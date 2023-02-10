using System;
using Script.Base;
using Script.Interface;
using Script.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Update = UnityEngine.PlayerLoop.Update;

namespace Script.Player
{
    public enum PlayerState
    {
        walk,
        idle
    }


    public class PlayerStatistics : MonoBehaviour, IAttackable
    {
        public static PlayerStatistics Instance;
        
        
        
        //[SerializeField] private float aiming;
        [HideInInspector] public float currentAiming;
        [SerializeField] private PlayerWeapon _playerWeapon;
        
        private float targetAiming;
        private float currentAimingVelocity;

        public float speed = 200.0f;
        public float rotationSpeed = 700.0f;
        public int currentLife;

        public PlayerState _playerState;

        [SerializeField] private Vise _viseUI;
        [SerializeField] PlayTime playTime;

        public WeaponSO weaponSo1;


        private void Start()
        {
            PlayerStatistics.Instance = this;
            SetPlayerState(PlayerState.idle);
            currentLife = 100;
        }


        private void Update()
        {
            switch (_playerState)
            {
                case PlayerState.idle:
                {
                    targetAiming = _playerWeapon._weaponSO[_playerWeapon._currentWeapon].idlAiming;
                    break;
                }
                case PlayerState.walk:
                {
                    targetAiming = _playerWeapon._weaponSO[_playerWeapon._currentWeapon].walkAiming;
                    break;
                }
            }
            
            UpdatePlayerAiming();
            
        }


        public void UpdatePlayerAiming()
        {
            
            currentAiming = Mathf.SmoothDamp(currentAiming, targetAiming, ref currentAimingVelocity, 1f);
            _viseUI.UpdateTargetAngleWithFloat(currentAiming);
        }

        public void SetPlayerState(PlayerState playerState)
        {
            _playerState = playerState;
        }

        public void Damage(int damage)
        {
            Debug.Log("Player take damage");
            currentLife -= damage;
            if (currentLife <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Player is dead");
            playTime.EndGameLose();
        }
    }
}