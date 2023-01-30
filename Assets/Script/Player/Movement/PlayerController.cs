using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player.Movement
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float _speed = 200.0f;
        [SerializeField] private float _rotationSpeed = 700.0f;
        
        private InputManager _inputManager;
        
        private Vector2 _move;
        private Vector2 _look;
        private bool _mustLook;

        // ReSharper disable once InconsistentNaming
        [SerializeField] private bool _isOnMoose = true;

        
        private Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _inputManager = new InputManager();
            _inputManager.Player.Move.performed += OnMove;
            _inputManager.Player.Move.canceled += OnMove;
            
            _inputManager.Player.Look.performed += OnLook;
            _inputManager.Player.Look.canceled += OnLook;
            
            _inputManager.Player.MustLook.performed += OnMustLook;
            _inputManager.Player.MustLook.canceled += OnMustLook;
        }
        
        private void OnEnable()
        {
            _inputManager.Enable();
        }
        private void OnDisable()
        {
            _inputManager.Disable();
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
            if (Camera.main != null) _look = Camera.main.ScreenToWorldPoint(_look);
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
                _look = _move;
            }
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, _look);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            _rb.MoveRotation(rotation);
        }
    }
}