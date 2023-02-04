using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
[RequireComponent(typeof(IsRootTip))]
public class Growing : MonoBehaviour
{
    [SerializeField] private int intemediateSplits = 5;
    private RootNode node;
    private IsRootTip tip;

    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<RootNode>();
        tip  = GetComponent<IsRootTip>();
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

        if (node.IsDead) {
            Destroy(this);
        }
    }

    bool distanceToHigh() {
        Vector2 a = getPreviousPos();
        Vector2 b = node.transform.position;

        if (node.IntermediatePoints.Count > 0)
            a = node.IntermediatePoints[node.IntermediatePoints.Count - 1];

        return (a - b).magnitude > tip.SplitDistance;
    }

    void split() {
        if (node.Parent == null)
            return;
        if (!childrenIsTip() && distanceToHigh()) {
            if (node.IntermediatePoints.Count < intemediateSplits)
                lazySplit();
            else
                split(node);
        }
    }
    void branch() {
        var parent = node.Parent;
        parent.Children.Remove(node);
        var newNodeObj = Instantiate(transform.gameObject, null);
        var newNode = newNodeObj.GetComponent<RootNode>();
        var newTip = newNodeObj.GetComponent<IsRootTip>();
        newTip.IsTip = false;
        newNode.Children = node.Children;
        foreach (var x in node.Children)
            x.Parent = newNode;
        newNode.Parent = parent;
        newNode.IntermediatePoints = node.IntermediatePoints;
        node.IntermediatePoints = new List<Vector2>();
        parent.Children.Add(newNode);
        newNode.Children.Add(node);
        node.Parent = newNode;
        node.Children = new List<RootNode>();
        node.OnSplit();
    }

    void split(RootNode Me) {
        RootNode parent = Me.Parent;
        parent.Children.Remove(Me);        
        var newNodeObj = Instantiate(transform.gameObject, null);
        var newNode = newNodeObj.GetComponent<RootNode>();
        newNodeObj.GetComponent<IsRootTip>().IsTip = false;
        newNode.Children = new List<RootNode>();       
        newNode.Parent = parent;
        parent.Children.Add(newNode);
        newNode.Children.Add(Me);
        newNode.lineRenderer = null;
        Me.Parent = newNode;
        newNode.transform.position = (Me.transform.position + getPreviousPos()) / 2;
        newNode.IntermediatePoints = Me.IntermediatePoints;
        Me.IntermediatePoints = new List<Vector2>();
        Me.Children = new List<RootNode>();
        Me.OnSplit();

    }

    void lazySplit() {        
        node.IntermediatePoints.Add((transform.position + getPreviousPos()) / 2);
        node.OnSplit();
    }

    Vector3 getPreviousPos() {
        if (node.IntermediatePoints.Count > 0)
            return node.IntermediatePoints[node.IntermediatePoints.Count - 1];
        return node.Parent.transform.position;
    }

    bool childrenIsTip() {
        foreach (var x in node.Children)
            if (x.GetComponent<IsRootTip>().IsTip)
                return true;
        return false;
    }
}
