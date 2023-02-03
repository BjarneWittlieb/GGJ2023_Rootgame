using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
[RequireComponent(typeof(IsRootTip))]
public class Growing : MonoBehaviour
{
    [SerializeField] private float SplitDistance = 2;

    private RootNode node;
    private IsRootTip tip;

    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<RootNode>();
        tip = GetComponent<IsRootTip>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!node.Parent)
            return;
        if (tip.IsTip && node.Children.Count == 0)
            split();
        else if (tip.IsTip && node.Children.Count != 0)
            branch();
    }

    bool distanceToHigh(RootNode a, RootNode b) {
        return (a.transform.position - b.transform.position).magnitude > SplitDistance;
    }

    void split() {
        if (node.Parent == null)
            return;
        if (distanceToHigh(node.Parent, node)) {
            split(node);
        }
    }
    void branch() {
        var parent = node.Parent;
        parent.Children.Remove(node);
        var newNodeObj = Instantiate(transform.gameObject, null);
        var newNode = newNodeObj.GetComponent<RootNode>();
        newNode.Children = node.Children;
        foreach (var x in node.Children)
            x.Parent = newNode;
        newNode.Children.Add(node);
        node.Parent = newNode;
        node.Children.Clear();
    }

    void split(RootNode Child) {
        RootNode parent = Child.Parent;
        parent.Children.Remove(Child);        
        var newNodeObj = Instantiate(transform.gameObject, null);
        var newNode = newNodeObj.GetComponent<RootNode>();
        newNodeObj.GetComponent<IsRootTip>().IsTip = false;
        newNode.Children.Clear();        
        newNode.Parent = parent;
        parent.Children.Add(newNode);
        newNode.Children.Add(Child);
        Child.Parent = newNode;
        newNode.transform.position = (Child.transform.position + parent.transform.position) / 2;
    }
}
