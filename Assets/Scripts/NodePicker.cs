using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class NodePicker : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private Camera     cam;
    [SerializeField] private float      maxPickDist = 1;
    public                   GameObject rootTarget;
    private                  bool       aim;

    private void Update()
    {
        if(!aim)
            return;
        
        var target = PickTarget();
        marker.transform.position = target.transform.position;
        OnDrawGizmos();
    }
    
    private void OnDrawGizmos()
    {
        if (rootTarget)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(marker.transform.position, rootTarget.transform.position);
        }
    }

    public GameObject PickTarget()
    {
        var mousePos = Vector3.Scale(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(1, 1, 0)); 

        var        objects  = new List<GameObject>(GameObject.FindGameObjectsWithTag("Root"));
        var        bestDist = float.PositiveInfinity;
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
            return bestObj;

        return null;
    }

    private void attack()
    {
        if (rootTarget && Input.GetMouseButtonDown(2))
        {
            if (rootTarget.GetComponent<RootAttack>())
                rootTarget.GetComponent<RootAttack>().attack(marker.transform.position);
        }
    }

    private void rootDestroy()
    {
        if (rootTarget && Input.GetMouseButtonDown(1))
        {
            foreach (var x in rootTarget.GetComponent<RootNode>().Children)
                x.Parent = null;
            rootTarget.GetComponent<RootNode>().Parent.Children.Remove(rootTarget.GetComponent<RootNode>());
            Destroy(rootTarget);
            rootTarget = null;
        }
    }
}