using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFoe : MonoBehaviour
{
    public AudioSource soundeffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision) {
        exterminate(collision);

    }
    void OnCollisionStay2D(Collision2D collision) {
        exterminate(collision);
    }


    void exterminate(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Root")) {

            if (collision.gameObject.GetComponent<RootAttack>().isCharging() || RootAttack.attacking)
                return;

            foreach (var x in collision.gameObject.GetComponent<RootNode>().Children)
                x.Parent = null;
            if (collision.gameObject.GetComponent<RootNode>().Parent != null)
                collision.gameObject.GetComponent<RootNode>().Parent.Children.Remove(collision.gameObject.GetComponent<RootNode>());
            Destroy(collision.gameObject);
            if (soundeffect)
                soundeffect.Play();
        }
    }
}