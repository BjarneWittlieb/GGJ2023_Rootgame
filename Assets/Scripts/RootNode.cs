using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void VoidCall();
public class RootNode : MonoBehaviour
{
    public List<RootNode> Children;
    public RootNode       Parent;
    public VoidCall       OnSplit;
    public VoidCall       OnDead;//too much roots around, will not split anymore
    public bool           IsDead = false;

    public LineRenderer lineRenderer;

    /// <summary>
    /// Depth till root
    /// </summary>
    public int Depth {
        get {
            if (Depth < 0)
            {
                _depth = Parent.Depth + 1;
                return _depth;
            }
            return _depth;
        }
    }

    private int _depth = -1;

    /// <summary>
    /// Length until root 
    /// </summary>
    public float Length {
        get {
            if (_length >= 0)
            {
                return _length;
            }

            if (Parent == null)
            {
                _length = 0f;
                return _length;
            }

            _length = Parent.Length + (this.transform.position - Parent.transform.position).magnitude;
            return _length;
        }
    }

    private float _length = -1f;

    private void OnDrawGizmos() {
        foreach (var x in Children)
            if (x)
                Gizmos.DrawLine(transform.position, x.transform.position);
    }
}
