using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsRootTip))]
[RequireComponent(typeof(RootNode))]
public class RandomWalk : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float rotationRange = Mathf.PI / 3;
    private IsRootTip tip;
    private RootNode node;

    private bool currentRotation = false;
    private float desiredAngle = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        tip  = GetComponent<IsRootTip>();
        node = GetComponent<RootNode>();
        currentRotation = Random.value < 0.5;
        if (!node.Parent)
            return;
        desiredAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tip.IsTip)
            walk();        
    }

    void walk() {
        //var dir = (transform.position - node.Parent.transform.position).normalized;
        //var currentAngle = Mathf.Deg2Rad * Vector2.Angle(new Vector2(0, 1), dir);
        //Vector2 movementDir = new Vector2(Mathf.Cos(desiredAngle), Mathf.Sin(desiredAngle)).normalized * tip.SplitDistance * 0.9f;
        //Vector2 movementTarget = (Vector2)node.Parent.transform.position + movementDir;
        //Vector2 movement = movementTarget - (Vector2)transform.position;
        //if (movement.magnitude > speed)
        //    movement = movement.normalized * speed;
        //
        //transform.position = transform.position + new Vector3(movement.x,movement.y,0);
        //
        //if (currentAngle < -rotationRange && !currentRotation)
        //    currentRotation = true;
        //if (currentAngle > rotationRange && currentRotation)
        //    currentRotation = false;
        //desiredAngle = desiredAngle + (currentRotation ? 1 : -1) * rotationRange * 0.1f;

    }

    double currentDistance() {
        return (transform.position - node.transform.position).magnitude;
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
