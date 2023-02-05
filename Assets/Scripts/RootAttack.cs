using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAttack : MonoBehaviour {
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float impactDuration = 0.4f;

    public float speed = 0.3f;
    public float maxDistance = 7;
    bool charge = false;
    Vector2 startLocation;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool isCharging() {
        return charge;
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

                if (collider.Count != 0 && impactEffect) {
                    StartCoroutine(onImpactEffect());
                }
            }
            else {
                Vector2 movement = (Vector3)direction * speed;

                var potentialFoes = Physics2D.LinecastAll(transform.position, (Vector2)transform.position + movement);

                for(int i = 0;i < potentialFoes.Length; i++) {
                    var foe = potentialFoes[i];
                    if (foe.collider.gameObject.GetComponent<IsFoe>()) {
                        Destroy(foe.collider.gameObject);
                    }
                }

                transform.position += (Vector3)movement;
            }
        }

    }

    public void attack(Vector2 vector) {
        GetComponent<Growing>().branch();
        //GetComponent<RandomWalk>().enabled = false;
        startLocation = transform.position;
        direction = (vector - startLocation).normalized;
        GetComponent<IsRootTip>().IsTip = true;
        GetComponent<RootNode>().IsDead = true;
        charge = true;
    }

    IEnumerator onImpactEffect() {
        var x = Instantiate(impactEffect);
        x.transform.position = transform.position;

        yield return new WaitForSeconds(impactDuration);

        Destroy(x);
    }
}
