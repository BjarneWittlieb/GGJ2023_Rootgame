using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    private float GetNewWidthFromBefore(float widthBefore, float additionalLength)
    {
        return Mathf.Log(Mathf.Exp(widthBefore) / widthModifier + additionalLength) * widthModifier;
    }

    private float CalculateWidth(float length)
    {
        return -Mathf.Exp(-widthModifier * length) + 1;
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


        List<RootNode> nodes = GetBranch(startNode, endNode);
        List<Vector3> vecs = nodes.SelectMany(GetPositions).ToList();

        startNode.lengthFromTip = endNode.lengthFromTip + GetLengthOfPath(vecs);
        lineRenderer.startWidth = CalculateWidth(startNode.lengthFromTip);
        lineRenderer.endWidth = CalculateWidth(endNode.lengthFromTip);

        if (onlyAdjustWidth) return;

        int totalLength = vecs.Count;

        // Only add none added
        //nodes = nodes.Skip(lineRenderer.positionCount).ToList();
        //if (!nodes.Any()) return;

        if (totalLength == lineRenderer.positionCount)
            return;

        lineRenderer.positionCount = totalLength;
        for (int i = 0; i < vecs.Count; i++) {
            lineRenderer.SetPosition(i, vecs[i]);
        }
    }

    private float GetLengthOfPath(List<Vector3> path)
    {
        float length = 0;
        for (int i = 0; i < path.Count - 1; ++i)
        {
            length += (path[i] - path[i + 1]).magnitude;
        }
        return length;
    }

    private List<Vector3> GetPositions(RootNode rootNode)
    {
        var positions = rootNode.IntermediatePoints.Select(v => new Vector3(v.x, v.y, 0)).ToList();
        positions.Add(rootNode.transform.position);
        return positions;
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
