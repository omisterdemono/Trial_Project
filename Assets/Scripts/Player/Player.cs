using Common;
using CombatSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(MeleeCombatComponent), typeof(MovementComponent))]
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private MovementComponent _movement;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private MeleeCombatComponent _combat;
        [SerializeField] private CustomColliderTrigger _interactablesCollider;

        private List<IInteractable> _interactables = new();

        public List<IInteractable> Interactables => _interactables;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animationController = GetComponent<AnimationController>();
            _movement = GetComponent<MovementComponent>();
            _combat = GetComponent<MeleeCombatComponent>();
        }

        private void Update()
        {
            _animationController.SetMoveDirection(_rigidbody.linearVelocity);
            _combat.UpdateAttackDirection(_rigidbody.linearVelocity);
        }

        private void OnEnable()
        {
            _interactablesCollider.OnTriggerEnter += AddInteractable;
            _interactablesCollider.OnTriggerExit += RemoveInteractable;
        }

        private void OnDisable()
        {
            _interactablesCollider.OnTriggerEnter -= AddInteractable;
            _interactablesCollider.OnTriggerExit -= RemoveInteractable;
        }

        private void AddInteractable(GameObject obj)
        {
            if (obj.TryGetComponent(out IInteractable interactable) && !_interactables.Contains(interactable))
                _interactables.Add(interactable);
        }

        private void RemoveInteractable(GameObject obj)
        {
            if (obj.TryGetComponent(out IInteractable interactable))
                _interactables.Remove(interactable);
        }

        public void Move(Vector2 direction) => _movement.Move(direction);
        public void Attack() => _combat.TryAttack();
    }
}