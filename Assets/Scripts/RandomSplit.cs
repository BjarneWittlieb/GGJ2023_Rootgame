using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
[RequireComponent(typeof(IsRootTip))]
public class RandomSplit : MonoBehaviour
{
    [SerializeField] private float chancheBranch = 0.001f;

    private RootNode node;
    private IsRootTip tip;
    // Start is called before the first frame update
    void Start() {
        node = GetComponent<RootNode>();
        tip = GetComponent<IsRootTip>();
    }

    // Update is called once per frame
    void Update() {
        if (node.IsDead) {
            Destroy(this);
        }
        if (!tip.IsTip && !node.IsDead && Random.value < chancheBranch)
            tip.IsTip = true;
    }
}
