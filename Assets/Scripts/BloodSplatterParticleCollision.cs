using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterParticleCollision : MonoBehaviour
{
    ParticleSystem particle;

    public List<GameObject> splatPrefabs;
    public Transform splatHolder;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

        int count = collisionEvents.Count;
        for (int i = 0; i < count; i++)
        {
            Instantiate(
                original: splatPrefabs[Random.Range(0, splatPrefabs.Count)], 
                position: collisionEvents[i].intersection, 
                rotation: Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), 
                parent: splatHolder);   
        }
    }

}
