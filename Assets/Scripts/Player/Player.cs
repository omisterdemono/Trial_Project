using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerControls), typeof(AnimationController), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private MovementComponent _movement;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private AnimationEventsHandler _eventsHandler;
        [SerializeField] private CircleCollider2D _damageCollider;
        [SerializeField] private CustomColliderTrigger _meleeAttackCollider;
        [SerializeField] private CustomColliderTrigger _interactablesCollider;

        [Header("Player settings")]
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _attackDistanceModifier = 0.75f;
        [SerializeField] private int _playerDaamage = 20;

        private float _attackCooldownTime = 0f;
        private Vector2 _attackDirection = Vector2.zero;


        private List<IInteractable> _interactables = new List<IInteractable>();

        public List<IInteractable> Interactables { get => _interactables;}

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animationController = GetComponent<AnimationController>();
            _movement = GetComponent<MovementComponent>();
            _eventsHandler = GetComponent<AnimationEventsHandler>();
        }

        private void Update()
        {
            _animationController.SetMoveDirection(_rigidbody.linearVelocity);

            _attackCooldownTime += Time.deltaTime;
        }

        private void OnEnable()
        {
            _eventsHandler.OnAttack += OnAttackComplete;
            _meleeAttackCollider.OnTriggerEnter += DealDamage;
            _interactablesCollider.OnTriggerEnter += AddInteractable;
            _interactablesCollider.OnTriggerExit += RemoveInteractable;
        }

        private void OnDisable()
        {
            _eventsHandler.OnAttack -= OnAttackComplete;
            _meleeAttackCollider.OnTriggerEnter -= DealDamage;
            _interactablesCollider.OnTriggerEnter -= AddInteractable;
            _interactablesCollider.OnTriggerExit -= RemoveInteractable;
        }

        private void AddInteractable(GameObject gameObject)
        {
            gameObject.TryGetComponent<IInteractable>(out IInteractable interactable);

            if (interactable != null && !_interactables.Contains(interactable))
            {
                _interactables.Add(interactable);
            }
        }

        private void RemoveInteractable(GameObject gameObject)
        {
            gameObject.TryGetComponent<IInteractable>(out IInteractable interactable);

            _interactables.Remove(interactable);
        }

        private void OnAttackComplete()
        {
            _damageCollider.offset = _attackDirection;
            _damageCollider.enabled = !_damageCollider.enabled;
        }

        public void Move(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                _attackDirection = direction * _attackDistanceModifier;
            }
            _movement.Move(direction);
        }

        public void Attack()
        {
            if (_attackCooldownTime > _attackCooldown)
            {
                _attackCooldownTime = 0;
                _animationController.PlayTrigger("Attack");
            }
        }

        public void DealDamage(GameObject gameObject)
        {
            gameObject.GetComponent<HealthComponent>().GetDamage(_playerDaamage);
        }
    }
}
