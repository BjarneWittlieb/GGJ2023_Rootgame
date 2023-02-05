using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectSprite : MonoBehaviour
{
    public Sprite[] opponentSprites;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = opponentSprites[Random.Range(0, opponentSprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
