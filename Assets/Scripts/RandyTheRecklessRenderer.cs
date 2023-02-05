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
            float waitTime = CalculateWidth(Mathf.Clamp(cummulativeLen, 0.4f, 40))+0.3f;
            yield return new WaitForSeconds(waitTime);
            if (!node.Parent) {
                ren.enabled = false;
                circle.SetActive(false);
                continue;
            }
            else {
                ren.enabled = true;
                circle.SetActive(true);
            }

            updateLen();
            updateRendererPath();
            updateRendererWidht();
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
        ren.positionCount  = node.IntermediatePoints.Count+2;
        ren.SetPosition(node.IntermediatePoints.Count + 1, transform.position);
        for (int i = 0; i < node.IntermediatePoints.Count; i++)
            ren.SetPosition(i + 1, node.IntermediatePoints[i]);
        ren.SetPosition(0, node.Parent.transform.position);
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
        float myLen = (transform.position - node.Parent.transform.position).magnitude;
        float bestChildLen = 0;
        cummulativeLen = myLen + childLen();
    }
}
