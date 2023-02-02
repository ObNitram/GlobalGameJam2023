using Script.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Enemy
{
    public class Enemy : MonoBehaviour, IAttackable
    {
        [SerializeField]
        private EnemySO _enemyData;
        private int _life;
        
        private NavMeshAgent _agent;
        public Transform player;
        
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
        }

        private void Start()
        {
            Init();
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
            _agent.SetDestination(player.position);
            
            _spriteRenderer.enabled = (timeIsSpoted > 0);
            if (timeIsSpoted < 0) return;
            timeIsSpoted -= Time.deltaTime; 

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