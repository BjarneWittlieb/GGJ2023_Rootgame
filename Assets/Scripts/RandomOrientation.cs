using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0,0,Random.value * 360);
        GetComponent<SpriteRenderer>().enabled = Random.value < 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
