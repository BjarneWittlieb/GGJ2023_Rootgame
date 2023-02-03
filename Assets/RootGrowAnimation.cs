using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootGrowAnimation : MonoBehaviour
{
    public Transform start;

    public Transform end;

    /// <summary>
    /// Speed modification
    /// </summary>
    public double speed = 1;

    /// <summary>
    ///  Size of the sqaures to generate
    /// </summary>
    public float lengthOfLine = .03f;

    private float angleMin = 30;
    private float angleMax = 30;

    private int maxLengthOfBranch = 8;

    private List<Vector3> pixelPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        // TODO generate the root

        pixelPositions.Add(start.position);
        GeneratePathFromPosition(start.position, end.position - start.position, maxLengthOfBranch);

        return;
    }

    private Vector3 getDirectionVector(Vector3 currentPosition, Vector3 directionBefore)
    {
        var test = end.position - currentPosition;

        return (directionBefore + test) / 2.0f;
    }

    private void GeneratePathFromPosition(Vector3 currentPosition, Vector3 directionBefore, int lengthLeft)
    {
        if ((currentPosition - end.position).magnitude < lengthOfLine * 2)
        {
            pixelPositions.Add(end.position);
            return;
        }

        if (lengthLeft > 0)
        {
            // Generate next point of interest in the right direction with right space between
            Vector3 directionVector = getDirectionVector(currentPosition, directionBefore);
            Vector3 nextPosition = getNextPoint(currentPosition, directionBefore);
            pixelPositions.Add(nextPosition);
            GeneratePathFromPosition(nextPosition, directionVector, lengthLeft - 1);
            return;
        }

        // A knot so that 2 additional branches are created
        List<float> angles = new List<float> { angleMin / 2f , - angleMin / 2f };
        foreach (float angle in angles)
        {
            Vector3 newDirection = Quaternion.AngleAxis(angle, Vector3.forward) * directionBefore;
            Vector3 nextPosition = getNextPoint(currentPosition, newDirection);
            pixelPositions.Add(nextPosition);
            GeneratePathFromPosition(nextPosition, newDirection, maxLengthOfBranch);
        }
    }

    private Vector3 getNextPoint(Vector3 currentPosition, Vector3 directionBefore)
    {
        return currentPosition + directionBefore.normalized * lengthOfLine;
    }

    // Update is called once per frame
    void Update()
    {
        // animate along the root

        return;
    }

}

