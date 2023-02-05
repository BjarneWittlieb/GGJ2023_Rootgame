using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDeath : MonoBehaviour
{
    
    [SerializeField] private GameObject effect;

    [SerializeField] public AudioClip deathSound;
    
    AudioSource _audioSource;

    bool isQuitting = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDestroy() {
        if (isQuitting)
            return;
        var x = Instantiate(effect);
        effect.GetComponent<AudioSource>().Play();
        x.transform.position = transform.position;
        ButtonPressSIngleton.playButtonClick();
    }

    private void OnApplicationQuit() {
        isQuitting = true;
    }
}
