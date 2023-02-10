using Script.Interface;
using Script.Player;
using SO;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Enemy
{
    public class ChampiEnemy : MonoBehaviour, IAttackable
    {
        public enum EnemyState
        {
            GoToPlayer,
            Attack,
        }


        [SerializeField] private EnemySO _enemyData;

        private int currentLife;
        private Vector2 _targetLook;
        public Vector2 _targetWay;
        public EnemyState _enemyState;

        public Transform player;

        private IA_Enemy _iaEnemy;
        private NavMeshAgent _agent;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private CaracterAnimator _caracterAnimator;
        [SerializeField] private Transform _rotation;
        [SerializeField] private meleAttackAnimator _meleAttackAnimator;
        [SerializeField] private AudioSource _audioSource;
        
        // Start is called before the first frame update
        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateUpAxis = false;
            _agent.updateRotation = false;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _circleCollider = GetComponent<CircleCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Init(PlayerStatistics.Instance.transform);
            _iaEnemy = new IA_Enemy(transform);
        }

        public void Init(Transform playerTransform)
        {
            player = playerTransform;
            currentLife = _enemyData.maxLife;
            _spriteRenderer.sprite = _enemyData.sprite;
            _circleCollider.radius = _enemyData.spokeCollider;
        }


        // Update is called once per frame
        void Update()
        {
            UpdateEnemyState();

            
            switch (_enemyState)
            {
                case EnemyState.GoToPlayer:
                    GoToPlayer();
                    break;
                case EnemyState.Attack:
                    AttackState();
                    break;
            }

            _rigidbody2D.AddForce(_targetWay);
            _rotation.rotation = Quaternion.LookRotation(Vector3.forward, _targetLook);
            
            _caracterAnimator.UpdateAnimator(_targetWay, _targetWay.magnitude > 0.1f);
        }

        private void UpdateEnemyState()
        {
            if ((player.position - transform.position).magnitude > 4f)
            {
                _enemyState = EnemyState.GoToPlayer;
            }
            else
            {
                _enemyState = EnemyState.Attack;
            }
        }


        private void GoToPlayer()
        {
            _agent.enabled = true;
            _agent.SetDestination(player.position);

            _targetWay = _agent.desiredVelocity.normalized;
        }

        private float _timeAttack;

        private void AttackState()
        {
            
            _agent.enabled = true;
            _agent.SetDestination(player.position);
            
            _timeAttack += Time.deltaTime;
            if (_timeAttack > _enemyData.cooldownAttack)
            {
                _timeAttack = 0;
                AttackPlayer();
            }

            _targetLook = (player.position - transform.position).normalized;
        }

        private void AttackPlayer()
        {
            Collider2D colliders = Physics2D.OverlapCircle(_attackPoint.position, _enemyData.rangeAttack,
                LayerMask.GetMask("Player"));

            if (colliders == null) return;
            _audioSource.Play();
            _meleAttackAnimator.Attack();
            colliders.GetComponent<IAttackable>().Damage(_enemyData.attackDamage);
        }
        

        public void Damage(int damage)
        {
            currentLife -= damage;

            if (currentLife <= 0)
            {
                Score.score += _enemyData.score;
                Destroy(gameObject);
            }
        }
    }
}