using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidCall();
public class RootNode : MonoBehaviour
{
    public List<RootNode> Children;
    public RootNode       Parent;
    public VoidCall       OnSplit;
    public VoidCall       OnDead;//too much roots around, will not split anymore
    public bool           IsDead = false;

    private void OnDrawGizmos() {
        foreach (var x in Children)
            if (x)
                Gizmos.DrawLine(transform.position, x.transform.position);
    }
}
