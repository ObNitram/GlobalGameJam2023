using System;
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

    
   
    public class PlayerStatistics : MonoBehaviour
    {
        [SerializeField] private float aiming;
        public float currentAiming;
        public float targetAiming;
        public float currentAimingVelocity;
        
        [SerializeField]
        public PlayerState _playerState;
        
        [SerializeField] private Vise _viseUI;


        public WeaponSO weaponSo1;
        
        

        private void Start()
        {
            SetPlayerState(PlayerState.idle);
        }


        private void Update()
        {
            switch (_playerState)
            {
                case PlayerState.idle:
                {
                    targetAiming = weaponSo1.idlAiming;
                    break;
                }
                case PlayerState.walk:
                {
                    targetAiming = weaponSo1.walkAiming;
                    break;
                }
            }
            
            
            
            currentAiming = Mathf.SmoothDamp(currentAiming,targetAiming, ref currentAimingVelocity ,1f);
            _viseUI.UpdateTargetAngleWithFloat(currentAiming);
        }

        public void SetPlayerState(PlayerState playerState)
        {
            _playerState = playerState;
        }
        
        
    }
    
}        
