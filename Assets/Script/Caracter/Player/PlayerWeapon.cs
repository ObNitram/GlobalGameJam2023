using System;
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


        public WeaponSO[] _weaponSO;
        public int _currentWeapon = 0;


        [SerializeField] private bool _isRapidFire = false;
        [SerializeField] private PlayerStatistics _statistics;
        [SerializeField] private AudioSource _audioSource;


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
            _inputManager.Player.ChangeWeapon.performed += ChangeWeapon;

            _rb = GetComponent<Rigidbody2D>();
            _light = _firePoint.GetComponent<Light2D>();
        }


        private void ChangeWeapon(InputAction.CallbackContext context)
        {
            Debug.Log("Weapon change");
            _delay = 0;
            StopAllCoroutines();

            _currentWeapon++;
            if (_currentWeapon >= _weaponSO.Length)
                _currentWeapon = 0;
        }

        private void OnEnable()
        {
            _inputManager.Player.Shoot.Enable();
            _inputManager.Player.ChangeWeapon.Enable();
        }

        private void OnDisable()
        {
            _inputManager.Player.Shoot.Disable();
            _inputManager.Player.ChangeWeapon.Disable();
        }

        private float lastShot;

        private float _delay;

        private void Update()
        {
            lastShot += Time.deltaTime;
            _delay += Time.deltaTime;
        }

        private void StartShoot(InputAction.CallbackContext context)
        {
            StopAllCoroutines();
            if (_delay < 0.5f) return;
            if (lastShot > _weaponSO[_currentWeapon].fireRate)
            {
                if (_isRapidFire && _mustShoot == false)
                {
                    _mustShoot = true;
                    _fireRateWait = new WaitForSeconds(_weaponSO[_currentWeapon].fireRate);
                    StartCoroutine(RapidFire());
                }
                else
                {
                    Shoot();
                }

                lastShot = 0;
            }

            StopCoroutine(RapidFire());
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
            //_light.intensity = 1;
            _audioSource.Play();
            for (int i = 0; i < _weaponSO[_currentWeapon].numberOfBullets; i++)
            {
                Quaternion rotation =
                    Quaternion.Euler(0, 0,
                        UnityEngine.Random.Range(-_statistics.currentAiming * 180, _statistics.currentAiming * 180) +
                        90f);
                rotation *= _firePoint.rotation;

                Bullet bullet = Instantiate<Bullet>(_bulletPrefab, _firePoint.position, rotation);
                bullet.Init(_weaponSO[_currentWeapon], _rb.velocity);
            }
        }
    }
}