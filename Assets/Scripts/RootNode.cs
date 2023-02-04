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
    public VoidCall       OnSplit;
    public VoidCall       OnDead;//too much roots around, will not split anymore
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
        foreach (var x in Children)
            if (x)
                Gizmos.DrawLine(transform.position, x.transform.position);
    }
}
