using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SearchForRoot : MonoBehaviour
{
    public float walkSpeed = 1;
    public float CliffPeekDistance = 1;

    Rigidbody2D body;
    Collider2D col;

    bool walkLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        walkLeft = Random.value < 0.5f;
    }

    bool findRoot(Vector3 dir) {
        var root = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, LayerMask.GetMask("Root"));
        var wall = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, LayerMask.GetMask("Wall"));
        if (wall && root && wall.distance < root.distance)
            return false;
        return root.collider;
    }

    // Update is called once per frame
    void Update()
    {
        if (findRoot(Vector3.right))
        {
            WalkRight();
        }
        else if (findRoot(Vector3.left))
        {
            WalkLeft();
        }
        else if (findRoot(Vector3.up))
        {
            body.velocity = new Vector2(0, 10);
        }
        else
        {
            float xSpeed = walkLeft ? -walkSpeed : walkSpeed;

            body.velocity = new Vector2(xSpeed, body.velocity.y);
            var bot = transform.position + new Vector3(0, col.bounds.extents[1], 0);

            var start = bot + new Vector3(CliffPeekDistance * Mathf.Sign(xSpeed), 0, 0);
            RaycastHit2D hit = Physics2D.Linecast(start, start + new Vector3(0, -2, 0), LayerMask.GetMask("Wall"));
            RaycastHit2D mauerhit = Physics2D.Linecast(transform.position, transform.position + new Vector3(0.5f * Mathf.Sign(xSpeed), 0, 0), LayerMask.GetMask("Wall"));
            if (!hit || mauerhit)
                walkLeft = !walkLeft;
        }


        void WalkLeft()
        {
            walkLeft = true;
            float xSpeed = walkLeft ? -walkSpeed : walkSpeed;
            body.velocity = new Vector2(xSpeed, body.velocity.y);
        }

        void WalkRight()
        {
            walkLeft = false;
            float xSpeed = walkLeft ? -walkSpeed : walkSpeed;
            body.velocity = new Vector2(xSpeed, body.velocity.y);
        }

    }
}
