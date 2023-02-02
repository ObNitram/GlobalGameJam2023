using System.Collections;
using Script.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Script.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private WeaponSO _weaponSO;
        [SerializeField] private bool _isRapidFire = false;
        [SerializeField] private PlayerStatistics _statistics;

        private InputManager _inputManager;
        private Rigidbody2D _rb;
        private Light2D _light;

        private bool _mustShoot = false;
        private WaitForSeconds _fireRateWait;


        private void Awake()
        {
            _inputManager = InputManagerSingleton.Instance;
            _inputManager.Player.Shoot.started += StartShoot;
            _inputManager.Player.Shoot.canceled += StopShoot;

            _rb = GetComponent<Rigidbody2D>();
            _light = _firePoint.GetComponent<Light2D>();
        }

        private void OnEnable()
        {
            _inputManager.Player.Shoot.Enable();
        }

        private void OnDisable()
        {
            _inputManager.Player.Shoot.Disable();
        }

        private void StartShoot(InputAction.CallbackContext context)
        {
            if (_isRapidFire)
            {
                _mustShoot = true;
                _fireRateWait = new WaitForSeconds(_weaponSO.fireRate);
                StartCoroutine(RapidFire());
            }
            else
            {
                Shoot();
            }
        }

        private void StopShoot(InputAction.CallbackContext context)
        {
            _mustShoot = false;
        }

        private IEnumerator RapidFire()
        {
            while (_mustShoot)
            {
                Shoot();
                yield return _fireRateWait;
            }
        }

        void Shoot()
        {
            _light.intensity = 1;

            for (int i = 0; i < _weaponSO.numberOfBullets; i++)
            {
                Quaternion rotation =
                    Quaternion.Euler(0, 0,
                        UnityEngine.Random.Range(-_statistics.currentAiming / 2, _statistics.currentAiming / 2));
                rotation *= _firePoint.rotation;
                Bullet bullet = Instantiate<Bullet>(_bulletPrefab, _firePoint.position, rotation);
                bullet.Init(_weaponSO, _rb.velocity);
            }
        }
    }
}