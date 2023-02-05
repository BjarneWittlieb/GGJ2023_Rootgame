using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSliderScript : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = slider.value;
    }
}
