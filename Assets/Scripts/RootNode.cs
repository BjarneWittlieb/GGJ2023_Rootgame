using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : MonoBehaviour
{
    public List<RootNode> Children;

    private void OnDrawGizmos() {
        foreach (var x in Children)
            if (x)
                Gizmos.DrawLine(transform.position, x.transform.position);
    }
}
