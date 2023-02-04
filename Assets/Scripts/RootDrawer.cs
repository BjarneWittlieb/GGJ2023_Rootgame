using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;
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
        return Mathf.Log(GetLongestPathLength(rootNode) + 1f) * widthModifier;
    }

    private void DrawRootNode(RootNode rootNode)
    {
        RootNode endNode = FindNextSplitterOrEnd(rootNode);
        DrawBranch(rootNode, endNode, false);

        foreach (var child in endNode.Children)
        {
            DrawRootNode(child);
        }
    }

    private RootNode FindNextSplitterOrEnd(RootNode rootNode)
    {
        if (rootNode.Children.Count == 1)
        {
            return FindNextSplitterOrEnd(rootNode.Children.Single());
        }
        else
        { 
            return rootNode; 
        }
    }

    private void DrawBranch(RootNode startNode, RootNode endNode, bool onlyAdjustWidth)
    {
        LineRenderer lineRenderer = endNode.lineRenderer;
        if (lineRenderer == null)
        {
            lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
            endNode.lineRenderer = lineRenderer;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = 0;
        }

        float endWidth = GetWidthOfRootNode(startNode);
        float startWidth = GetWidthOfRootNode(endNode);
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;

        Debug.Log($"Start: {startWidth}");
        Debug.Log($"End: {endWidth}");
        Debug.Log($"");
        Debug.Log($"");

        if (onlyAdjustWidth) return;

        List<RootNode> nodes = GetBranch(startNode, endNode);
        int totalLength = nodes.Count;

        // Only add none added
        nodes = nodes.Skip(lineRenderer.positionCount).ToList();
        if (!nodes.Any()) return;

        lineRenderer.positionCount = totalLength;
        for (int i = 0; i < nodes.Count; i++) {
            lineRenderer.SetPosition(totalLength - nodes.Count + i, nodes[i].transform.position);
        }
    }

    /// <summary>
    /// Gets list of rootnotes for two connected nodes. 
    /// Will stack overflow if roots are not connected.
    /// </summary>
    /// <param name="startNode"></param>
    /// <param name="endNode"></param>
    /// <returns></returns>
    private List<RootNode> GetBranch(RootNode startNode, RootNode endNode)
    {
        if (startNode == endNode) 
            return new List<RootNode>() { startNode };

        return GetBranch(startNode, endNode.Parent).Append(endNode).ToList();
    }

    //private void DrawCone(RootNode rootNode)
    //{
    //    if (rootNode.Parent == null)
    //    {
    //        return;
    //    }

    //    float endWidth = GetWidthOfRootNode(rootNode);
    //    float startWidth = GetWidthOfRootNode(rootNode.Parent);

    //    if (rootNode.lineRenderer != null)
    //    {
    //        rootNode.lineRenderer.startWidth = startWidth;
    //        rootNode.lineRenderer.endWidth = endWidth;
    //        rootNode.lineRenderer.SetPosition(0, rootNode.Parent.transform.position); //x,y and z position of the starting point of the line
    //        rootNode.lineRenderer.SetPosition(1, rootNode.transform.position); //x,y and z position of the end point of the line
    //        return;
    //    }

    //    //For creating line renderer object
    //    var lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
    //    rootNode.lineRenderer = lineRenderer;

    //    lineRenderer.startColor = color;
    //    lineRenderer.endColor = color;
    //    lineRenderer.startWidth = startWidth;
    //    lineRenderer.endWidth = endWidth;
    //    lineRenderer.positionCount = 2;
    //    lineRenderer.useWorldSpace = true;

    //    //For drawing line in the world space, provide the x,y,z values
    //    lineRenderer.SetPosition(0, rootNode.Parent.transform.position); //x,y and z position of the starting point of the line
    //    lineRenderer.SetPosition(1, rootNode.transform.position); //x,y and z position of the end point of the line
    //}
}
