using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RootGrowAnimation : MonoBehaviour
{
    public Transform start;

    public Transform end;

    /// <summary>
    /// Speed modification
    /// </summary>
    public float speed = 1f;

    /// <summary>
    ///  Size of the sqaures to generate
    /// </summary>
    public float lengthOfLine = .03f;

    public float lineWidth = 0.1f;

    public Color color = Color.black;

    public float angleMin = 30f;

    public float randomness = .5f;

    private float angleMax = 30;

    private float randomAngle = 10;


    private int _maxLengthOfBranch = 8;

    //private Tree<Vector3> _pixelPositions;

    //    // Start is called before the first frame update
    //void Start()
    //{
    //    // TODO generate the root
    //    UnityEngine.Random.InitState(1);

    //     _pixelPositions = new Tree<Vector3>(start.position);
    //    GeneratePathFromPosition(_pixelPositions, end.position - start.position, _maxLengthOfBranch, 0);

    //    DrawTree(_pixelPositions);

    //    return;
    //}

    ////private List<Line>> GetMainPath()
    ////{
    ////    throw new NotImplementedException();
    ////}

    //private List<Line> FormComplexerPath(List<Line> path, int depth)
    //{
    //    throw new NotImplementedException();
    //}

    //private List<Line> SplitLineInTwoLines(Line line)
    //{
    //    throw new NotImplementedException();

    //}

    //private void DrawTree(Tree<Vector3> tree)
    //{
    //    foreach(var child in tree.Children)
    //    {
    //        DrawLine(tree.Node, child.Node, lineWidth);
    //        DrawTree(child);
    //    }
    //}

    //private Vector3 getDirectionVector(Vector3 currentPosition, Vector3 directionBefore)
    //{
    //    float angle = UnityEngine.Random.Range(-randomAngle, randomAngle) * randomness;
    //    var directionVector = Quaternion.AngleAxis(angle, Vector3.forward) * (end.position - currentPosition);

    //    return directionVector + directionBefore * 3;
    //}

    //private void GeneratePathFromPosition(Tree<Vector3> treeSoFar, Vector3 directionBefore, int lengthLeft, int depth)
    //{
    //    if (depth > 5) {
    //        return;
    //    }

    //    if ((treeSoFar.Node - end.position).magnitude < lengthOfLine * 2)
    //    {
    //        _pixelPositions.Children.Add(new Tree<Vector3>(end.position));
    //        return;
    //    }

    //    if (lengthLeft > 0)
    //    {
    //        // Generate next point of interest in the right direction with right space between
    //        Vector3 directionVector = getDirectionVector(treeSoFar.Node, directionBefore);
    //        Vector3 nextPosition = getNextPoint(treeSoFar.Node, directionBefore);
    //        Tree<Vector3> child = new Tree<Vector3>(nextPosition);
    //        _pixelPositions.Children.Add(child);
    //        GeneratePathFromPosition(child, directionVector, lengthLeft - 1, depth);
    //        return;
    //    }

    //    // A knot so that 2 additional branches are created
    //    List<float> angles = new List<float> { angleMin / 2f, -angleMin / 2f };
    //    foreach (float angle in angles)
    //    {
    //        // Randomly kill nodes
    //        if (UnityEngine.Random.Range(0f, 1f) > .7f)
    //        {

    //            continue;
    //        }
    //        Vector3 newDirection = Quaternion.AngleAxis(angle, Vector3.forward) * directionBefore;
    //        Vector3 nextPosition = getNextPoint(treeSoFar.Node, newDirection);
    //        Tree<Vector3> child = new Tree<Vector3>(nextPosition);
    //        treeSoFar.Children.Add(child);
    //        GeneratePathFromPosition(child, newDirection, _maxLengthOfBranch, depth + 1);
    //    }
    //}

    //private Vector3 getNextPoint(Vector3 currentPosition, Vector3 directionBefore)
    //{
    //    return currentPosition + directionBefore.normalized * lengthOfLine;
    //}

    //private void DrawLine(Vector3 startPosition, Vector3 endPosition, float width)
    //{
    //    //For creating line renderer object
    //    var lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
    //    lineRenderer.startColor = color;
    //    lineRenderer.endColor = color;
    //    lineRenderer.startWidth = width;
    //    lineRenderer.endWidth = width;
    //    lineRenderer.positionCount = 2;
    //    lineRenderer.useWorldSpace = true;

    //    //For drawing line in the world space, provide the x,y,z values
    //    lineRenderer.SetPosition(0, startPosition); //x,y and z position of the starting point of the line
    //    lineRenderer.SetPosition(1, endPosition); //x,y and z position of the end point of the line
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // animate along the root

    //    return;
    //}

}

