using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    public Sprite[] _sprite;
    public float frameTime;



    private float acumulateur;

    private int index = 0;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        acumulateur += Time.deltaTime;

        if (acumulateur > frameTime)
        {
            acumulateur = 0;

            index++;
            
            if (index >= _sprite.Length)
            {
                index = 0;
            }

            _spriteRenderer.sprite = _sprite[index];


        }
        
        
    }
}
