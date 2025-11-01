using Common;
using System;
using UnityEngine;

namespace CombatSystem
{
    [RequireComponent(typeof(AnimationController), typeof(AnimationEventsHandler))]
    public class MeleeCombatComponent : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private AnimationEventsHandler _animationEventsHandler;
        [SerializeField] private CircleCollider2D _damageCollider;
        [SerializeField] private CustomColliderTrigger _meleeAttackCollider;

        [Header("Attack Settings")]
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _attackDistanceModifier = 0.5f;
        [SerializeField] private int _damageAmount = 10;

        private float _cooldownTimer;
        private Vector2 _attackDirection = Vector2.zero;

        public event Action OnAttackStart;
        public event Action OnAttackEnd;

        private void Awake()
        {
            _animationController = GetComponent<AnimationController>();
            _animationEventsHandler = GetComponent<AnimationEventsHandler>();
        }

        private void OnEnable()
        {
            _animationEventsHandler.OnAttack += OnAttackAnimationEvent;
            _meleeAttackCollider.OnTriggerEnter += DealDamage;
        }

        private void OnDisable()
        {
            _animationEventsHandler.OnAttack -= OnAttackAnimationEvent;
            _meleeAttackCollider.OnTriggerEnter -= DealDamage;
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;
        }

        public void UpdateAttackDirection(Vector2 moveDir)
        {
            if (moveDir.sqrMagnitude > 0.01f)
                _attackDirection = moveDir.normalized;
        }

        public void TryAttack()
        {
            if (_cooldownTimer >= _attackCooldown)
            {
                _cooldownTimer = 0f;
                _animationController.PlayTrigger("Attack");
                OnAttackStart?.Invoke();
            }
        }

        private void OnAttackAnimationEvent()
        {
            _damageCollider.offset = _attackDirection * _attackDistanceModifier;
            _damageCollider.enabled = !_damageCollider.enabled;
            OnAttackEnd?.Invoke();
        }

        private void DealDamage(GameObject target)
        {
            if (target.TryGetComponent(out HealthComponent health))
                health.GetDamage(_damageAmount);
        }
    }
}