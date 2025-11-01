using Common;
using CombatSystem;
using UnityEngine;

namespace EnemySystem
{
    [RequireComponent(typeof(MovementComponent), typeof(MeleeCombatComponent))]
    public class Enemy : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MovementComponent _movement;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private CustomColliderTrigger _viewCollider;
        [SerializeField] private MeleeCombatComponent _combat;

        [Header("AI Settings")]
        [SerializeField] private float _attackDistance = 1.5f;
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private float _patrolRadius = 5f;

        private Transform _detectedPlayer;
        private Vector2 _lastKnownPlayerPos;
        private EnemyState _currentState;
        private int _patrolIndex;

        public MovementComponent Movement => _movement;
        public Transform[] PatrolPoints => _patrolPoints;
        public float PatrolRadius => _patrolRadius;
        public Vector2 LastKnownPlayerPos => _lastKnownPlayerPos;
        public int PatrolIndex { get => _patrolIndex; set => _patrolIndex = value; }

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animationController = GetComponent<AnimationController>();
            _combat = GetComponent<MeleeCombatComponent>();
        }

        private void OnEnable()
        {
            _viewCollider.OnTriggerEnter += OnPlayerEnter;
            _viewCollider.OnTriggerExit += OnPlayerExit;
        }

        private void OnDisable()
        {
            _viewCollider.OnTriggerEnter -= OnPlayerEnter;
            _viewCollider.OnTriggerExit -= OnPlayerExit;
        }

        private void Start() => ChangeState(new IdleState(this));

        private void Update()
        {
            _currentState?.UpdateLogic();
            _combat.UpdateAttackDirection(_rigidbody.linearVelocity);
            _animationController.SetMoveDirection(_rigidbody.linearVelocity);

            if (_detectedPlayer != null)
            {
                float dist = Vector2.Distance(transform.position, _detectedPlayer.position);

                if (dist <= _attackDistance)
                {
                    _combat.TryAttack();
                    ChangeState(new AttackState(this));
                }
                else if (dist > _attackDistance && !_animationController.IsPlaying("Attack"))
                {
                    _lastKnownPlayerPos = _detectedPlayer.position;
                    ChangeState(new FollowState(this, _lastKnownPlayerPos));
                }
            }
        }

        private void FixedUpdate() => _currentState?.UpdatePhysics();

        private void OnPlayerEnter(GameObject player)
        {
            _detectedPlayer = player.transform;
            _lastKnownPlayerPos = _detectedPlayer.position;
        }

        private void OnPlayerExit(GameObject player)
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