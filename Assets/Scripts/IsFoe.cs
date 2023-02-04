using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFoe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Root"))
        {
            foreach (var x in collision.gameObject.GetComponent<RootNode>().Children)
                x.Parent = null;
            collision.gameObject.GetComponent<RootNode>().Parent.Children.Remove(collision.gameObject.GetComponent<RootNode>());
            Destroy(collision.gameObject);
        }
    }
}