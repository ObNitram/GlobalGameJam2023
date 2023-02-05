using System.Collections;
using UnityEngine;

public class meleAttackAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] _sprites;
    private int _index;
    private float _timer;
    private float _frameRate = 0.1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        spriteRenderer.enabled = true;
        _index = 0;
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (_index < _sprites.Length)
        {
            spriteRenderer.sprite = _sprites[_index];
            _index++;
            yield return new WaitForSeconds(_frameRate);
        }

        spriteRenderer.enabled = false;
    }
}