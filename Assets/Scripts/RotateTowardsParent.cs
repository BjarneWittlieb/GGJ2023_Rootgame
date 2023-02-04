using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RootNode))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class RotateTowardsParent : MonoBehaviour
{
    RootNode node;
    CapsuleCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<RootNode>();
        col = GetComponent<CapsuleCollider2D>();
        StartCoroutine(customUpdate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator customUpdate() {
        while (true) {
            if (node && node.Parent) {
                float distance = (node.Parent.transform.position - node.transform.position).magnitude;
                float angle = Vector2.SignedAngle(new Vector2(0, 1), (node.Parent.transform.position - node.transform.position).normalized);
                transform.eulerAngles = new Vector3(0, 0, angle);
                if (col) {
                    col.offset = new Vector2(0, distance / 2);

                    float scale = CalculateWidth(node.lengthFromTip);
                    if (scale <= 0.1f)
                        scale = 0.1f;
                    col.size = new Vector2(scale, distance);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private float CalculateWidth(float length) {
        return -Mathf.Exp(-RootDrawer.widthModifierStatic * length) + 1;
    }
}
