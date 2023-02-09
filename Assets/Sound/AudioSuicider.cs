using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSuicider : MonoBehaviour {

  public List<AudioClip> sounds;

    float initTime;
    AudioSource src;
	// Use this for initialization
	void Start () {
    initTime = Time.time;
    src = GetComponent<AudioSource>();
    src.clip = sounds[(int)(Random.value * (sounds.Count-1))];
    src.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Time.time - initTime) > src.timeSamples / 441000f + 9)
        {
            Destroy(this.gameObject);        
        }
	}
}
