using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressSIngleton : MonoBehaviour
{
    public static ButtonPressSIngleton instance;
    void Start()
    {
        instance = this;
    }

    public static void playButtonClick() {
        if (instance) {
            instance.play();
        }

    }
    private void play() {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
