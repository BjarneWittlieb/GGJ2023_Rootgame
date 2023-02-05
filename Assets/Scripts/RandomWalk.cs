using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsRootTip))]
[RequireComponent(typeof(RootNode))]
public class RandomWalk : MonoBehaviour {
    [SerializeField] private float RotationSpeed = 0.1f;
    [SerializeField] private float rotationRange = Mathf.PI / 3;
    [SerializeField] private float progressChance = 0.01f;
    public Vector2 gravity = new Vector2(0,-0.1f);

    [Range(0.01f,0.99f)]
    [SerializeField] private float walkSpeed = 0.05f;
    private IsRootTip tip;
    private RootNode node;

    public float probabilityOfDirectionModifier = .2f;

    private bool isRotatingRight = false;
    /// <summary>
    /// Is either -rotationSpeed or +rotationSpeed
    /// </summary>
    private float currentAngle = 0;
    private float currentDistance = 0;
    private bool isSplitting = false;
    
    /// <summary>
    /// The general direction in which the node will grow.
    /// If empty the node will grow completely random.
    /// </summary>
    public Vector2 generalDirection = Vector2.left;

    public bool growInGeneralDirection = true;

    float sinceWallIssue;
    bool hasWallIssue = false;

    // Start is called before the first frame update
    void Start()
    {
        tip  = GetComponent<IsRootTip>();
        node = GetComponent<RootNode>();
        isRotatingRight = Random.value < getProbabilityOfTurningRight();
        currentDistance = 0;
        node.OnSplit += splitting;
        if (!node.Parent)
            return; 
    }

    void splitting() {
        currentDistance = tip.SplitDistance / 2;
        isSplitting = false;
        isRotatingRight = Random.value < getProbabilityOfTurningRight();
        currentAngle = 0;
    }

    /// <summary>
    /// Returns the probabilty to turn right depending on the generalDirection vector
    /// </summary>
    /// <returns></returns>
    private float getProbabilityOfTurningRight()
    {
        if (!growInGeneralDirection)
        {
            return .5f;
        }

        float differenceOnLeftTurn = (getTargetDirection(-RotationSpeed).normalized - generalDirection.normalized).magnitude;
        float differenceOnRightTurn = (getTargetDirection(RotationSpeed).normalized - generalDirection.normalized).magnitude;
        return .5f + Mathf.Sign(differenceOnLeftTurn - differenceOnRightTurn) * probabilityOfDirectionModifier;
    }

    private Vector2 getParentDirection()
    {
        Vector2 parentDirection = new Vector2(0, 1);
        bool hasDir = node.Parent.Parent;
        Vector2 parentPosition = node.Parent.transform.position;
        Vector2 parentParentPosition = parentPosition;
        if (node.Parent.Parent)
            parentParentPosition = node.Parent.Parent.transform.position;
        if (node.IntermediatePoints.Count >= 1)
        {
            parentPosition = node.IntermediatePoints[node.IntermediatePoints.Count - 1];
            parentParentPosition = node.Parent.transform.position;
            hasDir = true;
        }
        if (node.IntermediatePoints.Count >= 2)
        {
            parentParentPosition = node.IntermediatePoints[node.IntermediatePoints.Count - 2];
            hasDir = true;
        }
        if (hasDir)
            parentDirection = (parentPosition - parentParentPosition).normalized;
        return parentDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tip.IsTip && node.Parent)
            walk();        
    }

    void walk() {
        if (node.IsDead)
            return;
        if (Random.value < progressChance) {
            List<Collider2D> collider = new List<Collider2D>(Physics2D.OverlapPointAll(transform.position,LayerMask.GetMask("Wall")));
            if (collider.Count == 0) {
                isSplitting = true;
                hasWallIssue = false;
            }
            else {
                rotationRange *= 1.01f;
                if (!hasWallIssue) {
                    hasWallIssue = true;
                    sinceWallIssue = Time.time;
                }
                else if ((Time.time-sinceWallIssue) > 5)
                    node.IsDead = true;
            }

        }
        transform.position = getTargetPosition(currentAngle);
        
        if (currentAngle < -rotationRange && !isRotatingRight)
            isRotatingRight = true;
        if (currentAngle > rotationRange && isRotatingRight)
            isRotatingRight = false;
        currentAngle += (isRotatingRight ? 1 : -1) * RotationSpeed;
        currentDistance = currentDistance * (1- walkSpeed) + tip.SplitDistance * (isSplitting?1.1f:0.9f) * walkSpeed;

    }

    private Vector2 getTargetDirection(float rotationAngle)
    {
        Vector2 parentDirection = getParentDirection();
        float parentAngleOfDirection = Mathf.Deg2Rad * Vector2.SignedAngle(new Vector2(1, 0), parentDirection);
        return new Vector2(Mathf.Cos(rotationAngle + parentAngleOfDirection), Mathf.Sin(rotationAngle + parentAngleOfDirection)).normalized * currentDistance + gravity;
    }

    Vector2 getTargetPosition(float rotationAngle) {
        Vector2 parentPosition = node.Parent.transform.position;
        Vector2 movementDir = getTargetDirection(rotationAngle);
        return parentPosition + movementDir;
    }

    private void OnDrawGizmos() {
        
    }
}
