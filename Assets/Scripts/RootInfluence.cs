using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
public class RootInfluence : MonoBehaviour
{
    public int currentInfluence = 0;
    public int DeadOnInfluence = 50;
    public CircleCollider2D influenceField;

    private RootNode node;
    private IsRootTip tip;

    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<RootNode>();
        tip = GetComponent<IsRootTip>();
    }

    // Update is called once per frame
    void Update()
    {
        if (node.IsDead || !tip.IsTip)
            return;
        currentInfluence = Physics2D.OverlapCircleAll(transform.position, influenceField.radius).Length;
        if (currentInfluence > DeadOnInfluence)
            node.IsDead = true;
    }
}
