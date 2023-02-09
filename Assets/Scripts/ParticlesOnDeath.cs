using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDeath : MonoBehaviour
{
    
    [SerializeField] private GameObject effect;

    [SerializeField] public GameObject deathSound;
    
    AudioSource _audioSource;

    bool isQuitting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDestroy() {
        if (isQuitting)
            return;
        var x = Instantiate(effect);
        if (deathSound)
            Instantiate(deathSound);
        x.transform.position = transform.position;
    }

    private void OnApplicationQuit() {
        isQuitting = true;
    }
}
