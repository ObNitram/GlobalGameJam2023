using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;

public class CaracterAnimator : MonoBehaviour
{
    public Vector2 direction;
    public bool isMoving;

    [SerializeField] private SC_SpriteAnimator spritePlayer;

    private SpriteRenderer spriteRenderer;

    private float timer;

    private int index;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spritePlayer.frameRate)
        {
            timer = 0;
            index++;

            if (isMoving)
            {
                if (direction.y > 0)
                {
                    if (index >= spritePlayer.spritesWalkBack.Length)
                    {
                        index = 0;
                    }
                    spriteRenderer.sprite = spritePlayer.spritesWalkBack[index];
                    spriteRenderer.sortingOrder = 3;
                }
                else
                {
                    if (index >= spritePlayer.spritesWalk.Length)
                    {
                        index = 0;
                    }
                    spriteRenderer.sprite = spritePlayer.spritesWalk[index];
                    spriteRenderer.sortingOrder = 1;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    //Debug.Log("fez");
                    if (index >= spritePlayer.spritesIdleBack.Length)
                    {
                        index = 0;
                    }
                    spriteRenderer.sprite = spritePlayer.spritesIdleBack[index];
                    spriteRenderer.sortingOrder = 3;
                }
                else
                {
                    if (index >= spritePlayer.spritesIdle.Length)
                    {
                        index = 0;
                    }
                    spriteRenderer.sprite = spritePlayer.spritesIdle[index];
                    spriteRenderer.sortingOrder = 1;
                }
                
                
     
            }
        }

        spriteRenderer.flipX = !(direction.x > 0);
    }
}