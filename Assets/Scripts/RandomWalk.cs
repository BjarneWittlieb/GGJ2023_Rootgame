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

    private bool currentRotation = false;
    private float currentAngle = 0;
    private float currentDistance = 0;
    private bool isSplitting = false;


    float sinceWallIssue;
    bool hasWallIssue = false;

    // Start is called before the first frame update
    void Start()
    {
        tip  = GetComponent<IsRootTip>();
        node = GetComponent<RootNode>();
        currentRotation = Random.value < 0.5;
        currentDistance = 0;
        node.OnSplit += splitting;
        if (!node.Parent)
            return;
    }

    void splitting() {
        currentDistance = tip.SplitDistance / 2;
        isSplitting = false;
        currentRotation = Random.value < 0.5;
        currentAngle = 0;
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
        transform.position = getTargetPosition();
        
        if (currentAngle < -rotationRange && !currentRotation)
            currentRotation = true;
        if (currentAngle > rotationRange && currentRotation)
            currentRotation = false;
        currentAngle += (currentRotation ? 1 : -1) * RotationSpeed;
        currentDistance = currentDistance * (1- walkSpeed) + tip.SplitDistance * (isSplitting?1.1f:0.9f) * walkSpeed;

    }

    Vector2 getTargetPosition() {
        Vector2 parentDir = new Vector2(0, 1);
        bool hasDir = node.Parent.Parent;
        Vector2 parentPosition = node.Parent.transform.position;
        Vector2 parentParentPosition = parentPosition;
        if (node.Parent.Parent)
            parentParentPosition = node.Parent.Parent.transform.position;
        if (node.IntermediatePoints.Count >= 1) {
            parentPosition = node.IntermediatePoints[node.IntermediatePoints.Count - 1];
            parentParentPosition = node.Parent.transform.position;
            hasDir = true;
        }
        if (node.IntermediatePoints.Count >= 2) {
            parentParentPosition = node.IntermediatePoints[node.IntermediatePoints.Count - 2];
            hasDir = true;
        }
        if (hasDir)
            parentDir = (parentPosition - parentParentPosition).normalized;
        var v = Mathf.Deg2Rad * Vector2.SignedAngle(new Vector2(1,0), parentDir);
        Vector2 movementDir = new Vector2(Mathf.Cos(currentAngle + v), Mathf.Sin(currentAngle + v)).normalized * currentDistance + gravity;
        return parentPosition + movementDir;
    }

    private void OnDrawGizmos() {
        
    }
}
