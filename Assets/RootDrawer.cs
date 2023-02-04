using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RootDrawer : MonoBehaviour
{
    public float startingWidth = .3f;

    public RootNode theMostParentParent;

    public Color color = Color.black;

    private float _stepSize;

    private int depthBefore = 0;

    // Start is called before the first frame update
    void Start()
    {
        depthBefore = GetLongestPath(theMostParentParent);
        _stepSize = startingWidth / depthBefore;

        DrawRootNode(theMostParentParent, startingWidth);
    }

    // Update is called once per frame
    void Update()
    {
        int newDepth = GetLongestPath(theMostParentParent);
        _stepSize = startingWidth / newDepth;

        if (newDepth != depthBefore)
        {
            DrawRootNode(theMostParentParent, startingWidth);
        }
    }

    private int GetLongestPath(RootNode rootNode)
    {
        if (!rootNode.Children.Any())
        {
            return 1;
        }   
        return rootNode.Children.Max(child => GetLongestPath(child)) + 1;
    }

    private void DrawRootNode(RootNode rootNode, float startWidth)
    {
        // Size is same es before, so no new drawing
        if (rootNode.lineRenderer != null && Mathf.Abs(rootNode.lineRenderer.startWidth - startWidth) < .05f)
        {
            return;
        }

        float endWidth = startWidth - _stepSize;
        DrawLine(rootNode, startWidth, endWidth);
        foreach (var child in rootNode.Children)
        {
            DrawRootNode(child, endWidth);
        }
    }

    private void DrawLine(RootNode rootNode, float startWidth, float endWidth)
    {
        if (rootNode.Parent == null)
        {
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
