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

    private int depthBefore = 0;

    private List<GameObject> oldLines= new List<GameObject>();

    private float _totalMaxLength;

    // Start is called before the first frame update
    void Start()
    {
        depthBefore = GetLongestPathDepth(theMostParentParent);

        DrawNewTree();
    }

    // Update is called once per frame
    void Update()
    {
        int newDepth = GetLongestPathDepth(theMostParentParent);

        if (newDepth != depthBefore)
        {
            depthBefore = newDepth;
            DesotroyOldTree();
            DrawNewTree();
        }
    }

    public void DesotroyOldTree()
    {
        foreach (GameObject line in oldLines) {
            Destroy(line);
        }
        oldLines.Clear();
    }

    public void DrawNewTree()
    {
        _totalMaxLength = GetLongestPathLength(theMostParentParent);
        DrawRootNode(theMostParentParent);
    }

    private int GetLongestPathDepth(RootNode rootNode)
    {
        if (!rootNode.Children.Any())
        {
            return 1;
        }   
        return rootNode.Children.Max(child => GetLongestPathDepth(child)) + 1;
    }

    private float GetLongestPathLength(RootNode rootNode)
    {
        if (!rootNode.Children.Any())
        {
            return 0f;
        }
        return rootNode.Children.Max(child => GetLongestPathLength(child) + (child.transform.position - rootNode.transform.position).magnitude);
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
        //For creating line renderer object
        GameObject gameObject = new GameObject("Line");
        oldLines.Add(gameObject);
        var lineRenderer = gameObject.AddComponent<LineRenderer>();
        rootNode.lineRenderer = lineRenderer;

        float endWidth = GetWidthOfRootNode(rootNode);
        float startWidth = GetWidthOfRootNode(rootNode.Parent);

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
