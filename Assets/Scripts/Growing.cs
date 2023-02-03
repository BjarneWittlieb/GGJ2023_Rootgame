using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
[RequireComponent(typeof(IsRootTip))]
public class Growing : MonoBehaviour
{
    [SerializeField] private float SplitDistance = 2;

    private RootNode node;

    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<RootNode>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkSplit();
    }

    void checkSplit() {
        if (node.Parent == null)
            return;
        var next = node.Parent.transform.position;
        var me = transform.position;
        if ((next-me).magnitude > SplitDistance) {
            split();
        }
    }

    void split() {
        RootNode parent = node.Parent;
        parent.Children.Remove(node);        
        var newNodeObj = Instantiate(transform.gameObject, null);
        var newNode = newNodeObj.GetComponent<RootNode>();
        newNode.Children.Clear();        
        newNode.Parent = parent;
        parent.Children.Add(newNode);
        newNode.Children.Add(node);
        node.Parent = newNode;

        newNode.transform.position = (node.transform.position + parent.transform.position) / 2;
    }
}
