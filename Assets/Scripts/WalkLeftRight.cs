using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(char))]
public class WalkLeftRight : MonoBehaviour
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

    // Update is called once per frame
    void FixedUpdate()
    {
        float xSpeed = walkLeft ? -walkSpeed : walkSpeed;

        body.velocity = new Vector2(xSpeed, body.velocity.y);
        var bot = transform.position + new Vector3(0, col.bounds.extents[1], 0);

        var start = bot + new Vector3(CliffPeekDistance * Mathf.Sign(xSpeed), 0, 0);
        RaycastHit2D hit = Physics2D.Linecast(start, start + new Vector3(0, -2, 0), LayerMask.GetMask("Wall"));
        if (!hit)
            walkLeft = !walkLeft;

    }
}
