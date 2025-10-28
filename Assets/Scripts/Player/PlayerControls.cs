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

        void OnMove(InputValue value)
        {
            Vector2 direction = value.Get<Vector2>();
            _movementComponent.Move(direction);
        }
    }
}
