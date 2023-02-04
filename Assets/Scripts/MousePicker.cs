using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePicker : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private Camera cam;
    [SerializeField] private float maxPickDist = 1;

    private GameObject target;

    private GameObject rootTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        marker.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        marker.transform.position = new Vector3(marker.transform.position.x, marker.transform.position.y,0);
        Vector3 mousePos = marker.transform.position;

        var objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Root"));
        float bestDist = float.PositiveInfinity;
        GameObject bestObj = null;
        foreach (var x in objects) {
            float dist = Vector3.Distance(x.transform.position, mousePos);
            if (dist < bestDist) {
                bestDist = dist;
                bestObj = x;                
            }
        }

        if (bestObj && bestDist < maxPickDist) {
            rootTarget = bestObj;
        }
        else
            rootTarget = null;

        grow();
    }

    private void grow() {
        if (rootTarget && Input.GetMouseButtonDown(0)) {
            if (rootTarget.GetComponent<IsRootTip>()) {
                rootTarget.GetComponent<IsRootTip>().IsTip = true;
                rootTarget.GetComponent<RootNode>().IsDead = false;
                rootTarget.GetComponent<RootInfluence>().TipDeadOnInfluence *= 2;
            }
        }
    }

    private void OnDrawGizmos() {
     if (rootTarget) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(marker.transform.position, rootTarget.transform.position);
        }
            
    }
}
