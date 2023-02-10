using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleAnim : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float fps = 0.2f;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        int index = (int) (Time.time * fps);
        index = index % sprites.Length;
        spriteRenderer.sprite = sprites[index];
    }
}
