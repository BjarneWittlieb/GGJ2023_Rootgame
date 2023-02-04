using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAttack : MonoBehaviour
{
    public float speed = 0.3f;
    public float maxDistance = 7;
    bool charge = false;
    Vector2 startLocation;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (charge) {
            List<Collider2D> collider = new List<Collider2D>(Physics2D.OverlapPointAll(transform.position, LayerMask.GetMask("Wall")));
            float dist = (startLocation - (Vector2)transform.position).magnitude;
            if (collider.Count > 0 || dist > maxDistance) {
                charge = false;
                GetComponent<IsRootTip>().IsTip = false;
            }
            else
                transform.position += (Vector3)direction * speed;
        }

    }

    public void attack(Vector2 vector) {
        Debug.Log("Attack");
        GetComponent<Growing>().branch();
        GetComponent<RandomWalk>().enabled = false;
        startLocation = transform.position;
        direction = (vector - startLocation).normalized;
        GetComponent<IsRootTip>().IsTip = true;
        GetComponent<RootNode>().IsDead = true;
        charge = true;
    }
}
