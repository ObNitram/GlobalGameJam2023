using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Vector3 = System.Numerics.Vector3;

namespace Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStatistics _playerStatistics;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private CaracterAnimator _caracterAnimator;
        private InputManager _inputManager;

        private Vector2 _move;
        public Vector2 _targetLook;
        private bool _mustLook;


        private Vector2 currentAngle;
        private Vector2 angleSpeed;        

        

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

            _inputManager.Player.Move.started += _ =>
            {
                _playerStatistics.SetPlayerState(PlayerState.walk); 
                _caracterAnimator.isMoving = true;
            };
            _inputManager.Player.Move.canceled += _ =>
            {
                _playerStatistics.SetPlayerState(PlayerState.idle);
                _caracterAnimator.isMoving = false;
            };


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
            _targetLook = context.ReadValue<Vector2>();

            if (!_isOnMoose) return;

            var position = transform.position;
            Vector2 objectPos = new Vector2(position.x, position.y);
            _targetLook = _camera.ScreenToWorldPoint(_targetLook);
            _targetLook -= objectPos;
            _targetLook.Normalize();
        }

        public void OnMustLook(InputAction.CallbackContext context)
        {
            _mustLook = context.ReadValueAsButton();
        }


        private void FixedUpdate()
        {
            MovePlayer();
            RotatePlayer();
        }

        private void MovePlayer()
        {
            _rb.AddForce(_move * (_playerStatistics.speed));
        }

        private void RotatePlayer()
        {
            _caracterAnimator.direction = currentAngle;

            if (!_mustLook)
            {
                if (_move == Vector2.zero)
                    return;
                _targetLook = _move;
            }

            currentAngle = Vector2.SmoothDamp(currentAngle, _targetLook, ref angleSpeed, 0.1f);

            _firePoint.rotation = Quaternion.LookRotation(UnityEngine.Vector3.forward, currentAngle);
        }
    }
}