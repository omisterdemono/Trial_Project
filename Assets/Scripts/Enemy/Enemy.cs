using Common;
using System;
using UnityEngine;

namespace EnemySystem
{
    [RequireComponent(typeof(MovementComponent), typeof(Rigidbody2D), typeof(AnimationController))]
    public class Enemy : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MovementComponent _movement;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private CustomColliderTrigger _viewCollider;

        [Header("Attack Settings")]
        [SerializeField] private float _attackDistance = 1.5f;
        [SerializeField] private float _attackCooldownTime = 2f;

        [Header("Patrol Settings")]
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private float _patrolRadius = 5f;

        private int _patrolIndex;
        private float _attackCooldown;
        private EnemyState _currentState;
        private Transform _detectedPlayer;
        private Vector2 _lastKnownPlayerPos;

        public MovementComponent Movement => _movement;
        public Transform[] PatrolPoints => _patrolPoints;
        public float PatrolRadius => _patrolRadius;
        public Vector2 LastKnownPlayerPos => _lastKnownPlayerPos;
        public int PatrolIndex { get => _patrolIndex; set => _patrolIndex = value; }
        public float AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animationController = GetComponent<AnimationController>();
        }

        private void OnEnable()
        {
            _viewCollider.OnTriggerEnter += SetPlayerPosition;
            _viewCollider.OnTriggerExit += RemovePlayerPosition;
        }

        private void OnDisable()
        {
            _viewCollider.OnTriggerEnter -= SetPlayerPosition;
            _viewCollider.OnTriggerExit -= RemovePlayerPosition;
        }

        private void Start()
        {
            ChangeState(new IdleState(this));
        }

        private void Update()
        {
            _attackCooldown += Time.deltaTime;
            _currentState?.UpdateLogic();

            if (_detectedPlayer != null)
            {
                float dist = Vector2.Distance(transform.position, _detectedPlayer.position);

                if (dist <= _attackDistance && _attackCooldown >= _attackCooldownTime)
                {
                    _attackCooldown = 0f;
                    ChangeState(new AttackState(this));
                    _animationController.PlayTrigger("Attack");
                    return;
                }
                else if (dist > _attackDistance && !_animationController.IsPlaying("Attack"))
                {
                    _lastKnownPlayerPos = _detectedPlayer.position;
                    ChangeState(new FollowState(this, _lastKnownPlayerPos));
                }
            }

            _animationController.SetMoveDirection(_rigidbody.linearVelocity);
        }

        private void FixedUpdate()
        {
            _currentState?.UpdatePhysics();
        }

        private void SetPlayerPosition(GameObject gameObject)
        {
            _detectedPlayer = gameObject.transform;
            _lastKnownPlayerPos = _detectedPlayer.position;
        }
        private void RemovePlayerPosition(GameObject @object)
        {
            _detectedPlayer = null;
            ChangeState(new IdleState(this));
        }

        public void ChangeState(EnemyState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}
