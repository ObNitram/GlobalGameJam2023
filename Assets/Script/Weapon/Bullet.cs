using Script.Interface;
using UnityEngine;

// lichess.org


namespace Script.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private WeaponSO _weaponSo;


        // Start is called before the first frame update
        public void Init(WeaponSO weaponSO, Vector2 initialSpeed)
        {
            _rigidbody2D.AddForce(transform.right * weaponSO.speed, ForceMode2D.Impulse);
            _rigidbody2D.AddForce(initialSpeed, ForceMode2D.Impulse);
            Destroy(gameObject, weaponSO.range);
            _weaponSo = weaponSO;
            _spriteRenderer.sprite = weaponSO.sprite;
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(tag)) return;

            IAttackable enemy = col.GetComponent<IAttackable>();
            enemy?.Damage(_weaponSo.damage);

            Destroy(gameObject);
        }
    }
}