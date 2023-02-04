using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public float minTime = 10f;
    public float maxTime = 20f;

    private float _timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        _timeLeft = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        _timeLeft -= Time.deltaTime;
        if (_timeLeft < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
