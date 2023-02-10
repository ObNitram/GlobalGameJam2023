using Script.Interface;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// lichess.org


namespace Script.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Light2D _light2D;
        private WeaponSO _weaponSo;

        private int currentPenetration = 0;

        // Start is called before the first frame update
        public void Init(WeaponSO weaponSO, Vector2 initialSpeed)
        {
            _rigidbody2D.AddForce(transform.right * weaponSO.speed, ForceMode2D.Impulse);
            _rigidbody2D.AddForce(initialSpeed, ForceMode2D.Impulse);
            Destroy(gameObject, weaponSO.range);
            _weaponSo = weaponSO;
            _spriteRenderer.sprite = weaponSO.sprite;
            _light2D.lightCookieSprite = weaponSO.sprite;
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(tag)) return;

            IAttackable enemy = col.GetComponent<IAttackable>();

            if (enemy == null)
            {
                Destroy(gameObject);
                return;
            }


            enemy.Damage(_weaponSo.damage);

            if (currentPenetration < _weaponSo.penetration)
                currentPenetration++;
            else
                Destroy(gameObject);
        }
    }
}