using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(MovementComponent))]
    public class PlayerControls : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] MovementComponent _movementComponent;
        [SerializeField] Animator _animator;

        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _animator = GetComponent<Animator>();            
        }

        void OnMove(InputValue value)
        {
            Vector2 direction = value.Get<Vector2>();
            _movementComponent.Move(direction);
        }
    }
}
