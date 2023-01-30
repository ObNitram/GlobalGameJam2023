using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private WeaponSO _weaponSO;
    [SerializeField] private bool _isRapidFire = false;

    
    private InputManager _inputManager;
    
    private bool _mustShoot = false;
    private WaitForSeconds _fireRateWait;
    
    
    private void Awake()
    {
        _inputManager = new InputManager();
        _inputManager.Player.Shoot.started += StartShoot;
        _inputManager.Player.Shoot.canceled += StopShoot;
    }

    private void OnEnable()
    {
        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
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
        Quaternion rotation =
            Quaternion.Euler(0, 0, UnityEngine.Random.Range(-_weaponSO.precision, _weaponSO.precision));
        rotation *= _firePoint.rotation;

        Bullet bullet = Instantiate<Bullet>(_bulletPrefab, _firePoint.position, rotation);
        bullet.initBullet(_weaponSO);
    }
}