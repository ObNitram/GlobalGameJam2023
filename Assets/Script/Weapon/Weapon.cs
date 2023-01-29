using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
     [SerializeField] private Transform _firePoint;
     [SerializeField] private GameObject _bulletPrefab;
     
     private bool _isShooting = false;
     
     //Handler for the input system
     public void OnShoot(InputAction.CallbackContext context)
     {
         _isShooting = context.ReadValueAsButton();
     }
     
     
     private void Update()
     {
           if (_isShooting)
           {
                Shoot();
           }
     }
     
     
     
     void Shoot()
     {
          Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
          
     }
     
     
}
