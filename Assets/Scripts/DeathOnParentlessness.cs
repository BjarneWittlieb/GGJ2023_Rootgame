using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
public class DeathOnParentlessness : MonoBehaviour
{
    [SerializeField] private GameObject RootDeathObject;

    public float timeTillDestruction = 0.5f;
    RootNode node;
    bool dying = false;
    GameObject particleEffects = null;
    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<RootNode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (node.IsBossRoot)
            return;

        if (node.Parent == null && !dying) {
            dying = true;
            StartCoroutine(destruction());
        }

    }

    IEnumerator destruction() {
        if (RootDeathObject) {
            particleEffects = Instantiate(RootDeathObject);
            particleEffects.transform.position = transform.position;
        }

        yield return new WaitForSeconds(timeTillDestruction);

        if (particleEffects)
            Destroy(particleEffects);

        Destroy(gameObject);
    }
}
