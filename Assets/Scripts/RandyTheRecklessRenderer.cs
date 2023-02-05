using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
public class RandyTheRecklessRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer ren;
    [SerializeField] private GameObject circle;

    public float widthModifier = .005f;
    public float cummulativeLen = 0;

    private RootNode node;
    private IsRootTip tip;

    public float updateFrequencyTip = 0.5f;
    public float updateFrequencyMax = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<RootNode>();
        tip = GetComponent<IsRootTip>();
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(customUpdate());
    }

    IEnumerator customUpdate() {
        while (true) {
            //float waitTime = CalculateWidth(Mathf.Clamp(cummulativeLen, 0.4f, 40))+0.3f;
            yield return new WaitForSeconds(0.5f);


            updateLen();
            updateRendererPath();
            updateRendererWidht();
            //superUltraRootUpdate();
        }
    }

    void updateRendererWidht() {
        float parentLen = cummulativeLen;
        float myLen = childLen();
        ren.startWidth = CalculateWidth(parentLen);
        ren.endWidth = CalculateWidth(myLen);
        circle.transform.localScale = new Vector3(1, 1, 1) * ren.endWidth;
    }

    private float CalculateWidth(float length) {
        return -Mathf.Exp(-widthModifier * length) + 1;
    }

    void updateRendererPath() {
        ren.enabled = node.Parent != null;
        if (!node.Parent)
            return;
        ren.positionCount  = node.IntermediatePoints.Count+2;
        ren.SetPosition(node.IntermediatePoints.Count + 1, transform.position);
        for (int i = 0; i < node.IntermediatePoints.Count; i++)
            ren.SetPosition(i + 1, node.IntermediatePoints[i]);
        ren.SetPosition(0, node.Parent.transform.position);
    }

    List<Vector3> getMyVectors() {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < node.IntermediatePoints.Count; i++)
            result.Add(node.IntermediatePoints[i]);
        result.Add(node.transform.position);
        result.Reverse();
        return result;
    }

    List<Vector3> gatherPoints(RootNode node,ref RootNode deepest) {
        List<Vector3> result = node.GetComponent<RandyTheRecklessRenderer>().getMyVectors();
        deepest = node;
        var parent = node.Parent;
        if (!parent)
            return new List<Vector3>();
        if (((parent.Children.Count > 1 && parent.Children[0] == node) || parent.Children.Count == 1)) {
            result.AddRange(gatherPoints(parent, ref deepest));

        }
        return result;
    }

    void superUltraRootUpdate() {
        if(node.Children.Count == 0) {
            ren.enabled = true;
            RootNode deepest = null;
            List<Vector3> points = new List<Vector3>();
            points.Add(new Vector3());
            points.AddRange(gatherPoints(node, ref deepest));
            points[0] = transform.position;
            points.Add(deepest.transform.position);
            
            ren.startWidth = 0;
            ren.endWidth = CalculateWidth(deepest.GetComponent<RandyTheRecklessRenderer>().cummulativeLen);
            ren.SetPositions(points.ToArray());
            ren.positionCount = points.Count;
        }
        else {
            ren.enabled = false;
        }
    }

    float childLen() {
        float bestChildLen = 0;
        for (int i = 0; i < node.Children.Count; i++) {
            var ren = node.Children[i].GetComponent<RandyTheRecklessRenderer>();
            if (ren.cummulativeLen > bestChildLen)
                bestChildLen = ren.cummulativeLen;
        }
        return bestChildLen;
    }

    void updateLen() {
        float myLen = 0;
        if (node.Parent)
            myLen = (transform.position - node.Parent.transform.position).magnitude;
        float bestChildLen = 0;
        cummulativeLen = myLen + childLen();
    }
}
