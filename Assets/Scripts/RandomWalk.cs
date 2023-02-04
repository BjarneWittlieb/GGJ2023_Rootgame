using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsRootTip))]
[RequireComponent(typeof(RootNode))]
public class RandomWalk : MonoBehaviour {
    [SerializeField] private float RotationSpeed = 0.1f;
    [SerializeField] private float rotationRange = Mathf.PI / 3;
    [SerializeField] private float progressChance = 0.01f;
    [SerializeField] private Vector2 gravity = new Vector2(0,-0.1f);
    private IsRootTip tip;
    private RootNode node;

    private bool currentRotation = false;
    private float currentAngle = 0;
    private float currentDistance = 0;
    private bool isSplitting = false;

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
    void Update()
    {
        if (tip.IsTip && node.Parent)
            walk();        
        if (node.IsDead) {
            Destroy(this);
        }
    }

    void walk() {
        if (node.IsDead)
            return;
        if (Random.value < progressChance)
            isSplitting = true;
        transform.position = getTargetPosition();
        
        if (currentAngle < -rotationRange && !currentRotation)
            currentRotation = true;
        if (currentAngle > rotationRange && currentRotation)
            currentRotation = false;
        currentAngle += (currentRotation ? 1 : -1) * RotationSpeed;
        currentDistance = currentDistance * 0.95f + tip.SplitDistance * (isSplitting?1.1f:0.9f) * 0.05f;

    }

    Vector2 getTargetPosition() {
        Vector2 parentDir = new Vector2(0, 1);
        if (node.Parent.Parent)
            parentDir = (node.Parent.transform.position - node.Parent.Parent.transform.position).normalized;
        var v = Mathf.Deg2Rad * Vector2.SignedAngle(new Vector2(1,0), parentDir);
        Vector2 movementDir = new Vector2(Mathf.Cos(currentAngle + v), Mathf.Sin(currentAngle + v)).normalized * currentDistance + gravity;
        return (Vector2)node.Parent.transform.position + movementDir;
    }

    double distanceRight() {
        Vector2 normal = (node.Parent.transform.position - node.transform.position).normalized;
        Vector2 right = new Vector2(-normal.x, normal.y);
        var cast = Physics2D.Raycast(transform.position, right);
        if (cast.collider == null)
            return float.PositiveInfinity;
        return cast.distance;
    }

    double distanceLeft() {
        Vector2 normal = (node.Parent.transform.position - node.transform.position).normalized;
        Vector2 left = new Vector2(normal.x, -normal.y);
        var cast = Physics2D.Raycast(transform.position, left);
        if (cast.collider == null)
            return float.PositiveInfinity;
        return cast.distance;
    }


    private void OnDrawGizmos() {
        
    }
}
