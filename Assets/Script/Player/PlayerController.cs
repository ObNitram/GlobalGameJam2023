    using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player
{


    
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float _speed = 200.0f;
        [SerializeField] private float _rotationSpeed = 700.0f;
        [SerializeField] private PlayerStatistics _playerStatistics;
        
        private InputManager _inputManager;
        
        private Vector2 _move;
        private Vector2 _look;
        private bool _mustLook;

        // ReSharper disable once InconsistentNaming
        [SerializeField] private bool _isOnMoose = true;
        [SerializeField] private Camera _camera;
        
        private Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _inputManager = InputManagerSingleton.Instance;
            _inputManager.Player.Move.performed += OnMove;
            _inputManager.Player.Move.canceled += OnMove;

            _inputManager.Player.Move.started += _ => { _playerStatistics.SetPlayerState(PlayerState.walk); };
            _inputManager.Player.Move.canceled += _ => { _playerStatistics.SetPlayerState(PlayerState.idle); };

            
            _inputManager.Player.Look.performed += OnLook;
            //_inputManager.Player.Look.canceled += OnLook;
            
            _inputManager.Player.MustLook.performed += OnMustLook;
            _inputManager.Player.MustLook.canceled += OnMustLook;
        }
        
        private void OnEnable()
        {
            _inputManager.Player.Move.Enable();
            _inputManager.Player.Look.Enable();
            _inputManager.Player.MustLook.Enable();
        }
        private void OnDisable()
        {
            _inputManager.Player.Move.Disable();
            _inputManager.Player.Look.Disable();
            _inputManager.Player.MustLook.Disable();

        }


        //Handler for the input system
        public void OnMove(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            _look = context.ReadValue<Vector2>();
            
            if (!_isOnMoose) return;
            
            var position = transform.position;
            Vector2 objectPos = new Vector2(position.x, position.y);
            _look = _camera.ScreenToWorldPoint(_look);
            _look-= objectPos;
            _look.Normalize();
        }
        public void OnMustLook(InputAction.CallbackContext context)
        {
            _mustLook = context.ReadValueAsButton();
        }
        

        private void FixedUpdate()
        {
            MovePlayer();
        }
        
        private void MovePlayer()
        {
            _rb.AddForce(_move * (_speed)) ;
            
            if (!_mustLook)
            {
                if(_move == Vector2.zero)
                    return;
                _look = _move;
            }
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, _look);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            _rb.MoveRotation(rotation);
        }
    }
}