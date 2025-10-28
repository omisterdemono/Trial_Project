using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Player),typeof(MovementComponent))]
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
            

            //Test Variant for animation
            if (direction.magnitude > 0)
            {
                _animator.SetFloat("Horizontal", direction.x);
                _animator.SetFloat("Vertical", direction.y);
                _animator.SetBool("Walk", true);
            }
            else
            {
                _animator.SetBool("Walk", false);
            }
        }
    }
}
