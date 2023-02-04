using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class RootDrawer : MonoBehaviour
{
    public float widthModifier = .1f;

    public RootNode theMostParentParent;

    public Color color = Color.black;

    //private float _totalMaxLength;

    // Start is called before the first frame update
    void Start()
    {
        DrawNewTree();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        theMostParentParent.UpdateCurrentLength();
        DrawNewTree();
    }

    public void DrawNewTree()
    {
        DrawRootNode(theMostParentParent);
    }

    private float GetLongestPathLength(RootNode rootNode)
    {
        return rootNode.CurrentLength;
    }

    private float GetWidthOfRootNode(RootNode rootNode)
    {
        return Mathf.Log(GetLongestPathLength(rootNode) + 1) * widthModifier;
    }

    private void DrawRootNode(RootNode rootNode)
    {
        DrawCone(rootNode);
        foreach (var child in rootNode.Children)
        {
            DrawRootNode(child);
        }
    }

    private void DrawCone(RootNode rootNode)
    {
        if (rootNode.Parent == null)
        {
            return;
        }

        float endWidth = GetWidthOfRootNode(rootNode);
        float startWidth = GetWidthOfRootNode(rootNode.Parent);

        if (rootNode.lineRenderer != null)
        {
            rootNode.lineRenderer.startWidth = startWidth;
            rootNode.lineRenderer.endWidth = endWidth;
            rootNode.lineRenderer.SetPosition(0, rootNode.Parent.transform.position); //x,y and z position of the starting point of the line
            rootNode.lineRenderer.SetPosition(1, rootNode.transform.position); //x,y and z position of the end point of the line
            return;
        }

        //For creating line renderer object
        var lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        rootNode.lineRenderer = lineRenderer;

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, rootNode.Parent.transform.position); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, rootNode.transform.position); //x,y and z position of the end point of the line
    }
}
