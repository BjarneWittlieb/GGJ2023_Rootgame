using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(vanish());
    }
    IEnumerator vanish() {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
