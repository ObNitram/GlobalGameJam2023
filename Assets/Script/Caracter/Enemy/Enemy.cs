using Script.Interface;
using SO;
using UnityEngine;
using UnityEngine.AI;
namespace Script.Caracter.Enemy
{
    public class Enemy : MonoBehaviour, IAttackable
    {
        public enum EnemyState
        {
            GoToPlayer,
            Attack,
        }


        [SerializeField] private EnemySO _enemyData;
        private int _life;

        public EnemyState _enemyState;
        private IA_Enemy _iaEnemy;

        private NavMeshAgent _agent;
        public Transform player;
        private Rigidbody2D _rigidbody2D;

        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider;

        private float timeIsSpoted;

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
            Init();
            _iaEnemy = new IA_Enemy(transform);
        }

        public void Init()
        {
            _life = _enemyData.maxLife;
            _spriteRenderer.sprite = _enemyData.sprite;
            _circleCollider.radius = _enemyData.spokeCollider;
            _agent.speed = _enemyData.speed;
            _agent.acceleration = _enemyData.acceleration;
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
                    Attack();
                    break;
            }


            _spriteRenderer.enabled = (timeIsSpoted > 0);
            if (timeIsSpoted < 0) return;
            timeIsSpoted -= Time.deltaTime;
        }

        private void UpdateEnemyState()
        {
            if ((player.position - transform.position).magnitude > 40f)
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
        }

        private void Attack()
        {
            _agent.enabled = false;

            Vector2 dir = _iaEnemy.CalculateDirection().normalized * _enemyData.speed;
            Debug.Log(dir);
            //_rigidbody2D.AddForce(dir);
            
            
            
        }


        public void HasBeenSpoted()
        {
            if (timeIsSpoted < 0.1)
            {
                timeIsSpoted += 0.1f;
            }
        }

        public void Damage(int damage)
        {
            _life -= damage;

            if (_life <= 0) Destroy(gameObject);
        }
    }
}