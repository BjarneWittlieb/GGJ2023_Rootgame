using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public delegate void VoidCall();
public class RootNode : MonoBehaviour
{
    public List<Vector2> IntermediatePoints; //Between Me and my Parent
    public List<RootNode> Children;
    public RootNode       Parent;
    public VoidCall       OnSplit = () => { };
    public VoidCall       OnDead = () => { };//too much roots around, will not split anymore
    public bool           IsDead = false;

    public LineRenderer lineRenderer;

    public float CurrentLength = 0;

    public void Start()
    {
        lineRenderer = null;
        CurrentLength = 0;
    }

    public void UpdateCurrentLength()
    {
        if (!Children.Any())
        {
            return;
        }
        CurrentLength = Children.Max(child => child.CurrentLength + (child.transform.position - transform.position).magnitude);
        foreach (var child in Children)
        {
            child.UpdateCurrentLength();
        }
    }

    private void OnDrawGizmos() {
        if (!Parent)
            return;
        if (IntermediatePoints.Count != 0) {
            Gizmos.DrawLine(Parent.transform.position, IntermediatePoints.First());
            for(int i = 1;i < IntermediatePoints.Count;i++)
                Gizmos.DrawLine(IntermediatePoints[i-1], IntermediatePoints[i]);
            Gizmos.DrawLine(transform.position, IntermediatePoints.Last());
        }
        else
            Gizmos.DrawLine(Parent.transform.position, transform.position);
    }
}
