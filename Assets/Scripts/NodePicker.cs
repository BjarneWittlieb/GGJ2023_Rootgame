using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Timeline;

public class NodePicker : MonoBehaviour
{
    [SerializeField] public Light2D marker;
    [SerializeField] private Camera     cam;
    [SerializeField] private float      maxPickDist = 10;

    private float _oldRadius;
    private float _newRadius;

    public float transitionTime = .1f;

    /// <summary>
    /// Value between 0 and transtionTIme
    /// </summary>
    private float _inTransition;

    public void setNewRadius(float newRadius)
    {
        //_oldRadius = marker.pointLightOuterRadius;
        //_newRadius = newRadius;
    }

    public bool       draw;
    public GameObject target;

    private void Update()
    { 
        PickNodeInRange();

        TransitionMarkerRadius();
    }

    public void TransitionMarkerRadius()
    {
        if (_inTransition > transitionTime)
        {
            // marker.pointLightOuterRadius = _newRadius;
            return;
        }
        // marker.pointLightOuterRadius = _oldRadius + (_newRadius - _oldRadius) * (_inTransition / transitionTime);
        _inTransition -= Time.deltaTime;
        
    }

    private void OnDrawGizmos()
    {
        if (target && draw)
        {
            draw         = false;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(marker.transform.position, target.transform.position);
        }
    }

    private void PickNodeInRange()
    {
        var mousePos = Vector3.Scale(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(1, 1, 0)); 
        // Debug.Log(mousePos);
        marker.transform.position = mousePos;

        var        objects  = new List<GameObject>(GameObject.FindGameObjectsWithTag("Root"));
        var        bestDist = float.PositiveInfinity;
        target = null;
        GameObject bestObj  = null;

        foreach (var x in objects)
        {
            var dist = Vector3.Distance(x.transform.position, mousePos);

            if (dist < bestDist)
            {
                bestDist = dist;
                bestObj  = x;
            }
        }

        if (bestObj && bestDist < maxPickDist)
            target = bestObj;
    }

    // private void attack()
    // {
    //     if (rootTarget && Input.GetMouseButtonDown(2))
    //     {
    //         if (rootTarget.GetComponent<RootAttack>())
    //             rootTarget.GetComponent<RootAttack>().attack(marker.transform.position);
    //     }
    // }
    //
    // private void rootDestroy()
    // {
    //     if (rootTarget && Input.GetMouseButtonDown(1))
    //     {
    //         foreach (var x in rootTarget.GetComponent<RootNode>().Children)
    //             x.Parent = null;
    //         rootTarget.GetComponent<RootNode>().Parent.Children.Remove(rootTarget.GetComponent<RootNode>());
    //         Destroy(rootTarget);
    //         rootTarget = null;
    //     }
    // }
}