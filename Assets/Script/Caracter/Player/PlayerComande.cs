using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Player;
using UnityEngine;

public class PlayerComande : MonoBehaviour
{
    private InputManager _inputManager;
    
    [SerializeField] NormalBase _normalBase;
    
    private void Awake()
    {
        _inputManager = InputManagerSingleton.Instance;
        _inputManager.Player.SetupBase.performed += _ => SetupBase();
    }
    
    private void OnEnable()
    {
        _inputManager.Player.SetupBase.Enable();
    }
    
    private void OnDisable()
    {
        _inputManager.Player.SetupBase.Disable();
    }

   private void SetupBase()
   {
       Instantiate(_normalBase, transform.position, Quaternion.identity);
   }
}
