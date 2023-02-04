using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
[RequireComponent(typeof(IsRootTip))]
public class RandomSplit : MonoBehaviour
{
    public  float chancheBranch = 0.001f;
    public float TimeBeforeNewSplit = 5;

    private RootNode node;
    private IsRootTip tip;
    private float sinceExistence;
    // Start is called before the first frame update
    void Start() {
        node = GetComponent<RootNode>();
        tip = GetComponent<IsRootTip>();
        sinceExistence = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (sinceExistence + TimeBeforeNewSplit > Time.time)
            return;
        if (!tip.IsTip && !node.IsDead && Random.value < chancheBranch)
            tip.IsTip = true;
    }
}
