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
    }

    bool distanceToHigh() {
        Vector2 a = getPreviousPos();
        Vector2 b = node.transform.position;

        if (node.IntermediatePoints.Count > 0)
            a = node.IntermediatePoints[node.IntermediatePoints.Count - 1];

        if (node.Parent) {
            RaycastHit2D mauerhit = Physics2D.Linecast(transform.position, node.Parent.transform.position, LayerMask.GetMask("Wall"));
            if (mauerhit)
                return false;
        }

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
    public void branch() {
        var parent = node.Parent;
        if (!parent)
            return;
        parent.Children.Remove(node);
        Transform basket = (GameObject.Find("RootBasket")? GameObject.Find("RootBasket").transform:null);
        var newNodeObj = Instantiate(transform.gameObject, basket);


        //cleanup line renderer
        var renderer = newNodeObj.GetComponentsInChildren<LineRenderer>();
        foreach (var x in renderer)
            if (!x.GetComponent<RootNode>())
                Destroy(x.gameObject);

        var newNode = newNodeObj.GetComponent<RootNode>();
        var newTip = newNodeObj.GetComponent<IsRootTip>();
        newTip.IsTip = false;
        newNode.Children = node.Children;
        newNode.lineRenderer = null;
        foreach (var x in node.Children) {
            x.rootCirlce.transform.localScale = new Vector3(0, 0, 0);
            x.Parent = newNode;
            x.lengthFromTip = getLen(x);
        }
        newNode.Parent = parent;
        newNode.IntermediatePoints = node.IntermediatePoints;
        newNode.rootCirlce.transform.localScale = new Vector3(0, 0, 0);
        parent.rootCirlce.transform.localScale = new Vector3(0, 0, 0);
        node.IntermediatePoints = new List<Vector2>();
        parent.Children.Add(newNode);
        newNode.Children.Add(node);
        node.Parent = newNode;
        node.Children = new List<RootNode>();
        if(node.rootCirlce)
            node.rootCirlce.transform.localScale = new Vector3(0, 0, 0);
        node.OnSplit();
        node.lengthFromTip = 0;


        if (newNode.lengthFromTip == 0) {
            newNode.lengthFromTip = getLen(newNode);
        }
    }

    void split(RootNode Me) {
        RootNode parent = Me.Parent;
        parent.Children.Remove(Me);
        Transform basket = (GameObject.Find("RootBasket") ? GameObject.Find("RootBasket").transform : null);
        var newNodeObj = Instantiate(transform.gameObject, basket);

        //cleanup line renderer
        var renderer = newNodeObj.GetComponentsInChildren<LineRenderer>();
        foreach (var x in renderer)
            if (!x.GetComponent<RootNode>())
              Destroy(x.gameObject);        

        var newNode = newNodeObj.GetComponent<RootNode>();        
        newNodeObj.GetComponent<IsRootTip>().IsTip = false;
        newNode.Children = new List<RootNode>();
        newNode.rootCirlce.transform.localScale = new Vector3(0, 0, 0);
        parent.rootCirlce.transform.localScale = new Vector3(0, 0, 0);
        newNode.lineRenderer = null;
        newNode.Parent = parent;
        parent.Children.Add(newNode);
        newNode.Children.Add(Me);
        Me.Parent = newNode;
        newNode.transform.position = (Me.transform.position + getPreviousPos()) / 2;
        newNode.IntermediatePoints = Me.IntermediatePoints;
        Me.IntermediatePoints = new List<Vector2>();
        Me.Children = new List<RootNode>();
        if (Me.rootCirlce) Me.rootCirlce.transform.localScale = new Vector3(0, 0, 0);
        Me.OnSplit();

    }

    void lazySplit() {        
        node.IntermediatePoints.Add((transform.position + getPreviousPos()) / 2);
        node.OnSplit();
    }

    float getLen(RootNode r) {
        float result = 0;
        foreach (var x in r.Children)
            result = Mathf.Max(result, getLen(x));
        if (r.Parent)
            result += (r.transform.position - r.Parent.transform.position).magnitude;
        return result;
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
