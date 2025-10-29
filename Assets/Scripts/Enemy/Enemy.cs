using Common;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemySystem
{
    [RequireComponent(typeof(MovementComponent))]
    public class Enemy : MonoBehaviour
    {
        private const float AttackCooldownTime = 1f;
        [Header("References")]
        [SerializeField] private MovementComponent _movement;
        [SerializeField] private Transform _player;

        [Header("Patrol Settings")]
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private float _patrolRadius = 5f;

        [Header("Detection Settings")]
        [SerializeField] private float _viewDistance = 7f;
        [SerializeField] private float _attackDistance = 1.5f;
        
        private int _patrolIndex = 0;
        private float _attackCooldown = 0;
        private EnemyState _currentState;
        private Vector2 _lastKnownPlayerPos;

        public MovementComponent Movement => _movement;
        public Transform Player => _player;
        public Transform[] PatrolPoints => _patrolPoints;
        public float PatrolRadius => _patrolRadius;
        public Vector2 LastKnownPlayerPos => _lastKnownPlayerPos;
        public int PatrolIndex { get => _patrolIndex; set => _patrolIndex = value; }
        public float AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
        }

        private void Start()
        {
            ChangeState(new IdleState(this));
        }

        private void Update()
        {
            _attackCooldown += Time.deltaTime;
            _currentState?.UpdateLogic();

            // Detection logic
            if (_player != null)
            {
                float dist = Vector2.Distance(transform.position, _player.position);
                if (dist < _attackDistance && _attackCooldown >= AttackCooldownTime)
                {
                    _attackCooldown = 0f;
                    ChangeState(new AttackState(this));
                    return;
                }
                else if (dist < _viewDistance && dist >= _attackDistance)
                {
                    _lastKnownPlayerPos = _player.position;
                    ChangeState(new FollowState(this, _lastKnownPlayerPos));
                }
            }
        }

        private void FixedUpdate()
        {
            _currentState?.UpdatePhysics();
        }

        public void ChangeState(EnemyState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}
