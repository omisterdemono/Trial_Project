using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(MovementComponent))]
    public class PlayerControls : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private GameObject _inventoryPanel;
        private Player _player;

        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _player = GetComponent<Player>();
        }

        private void OnMove(InputValue value)
        {
            Vector2 direction = value.Get<Vector2>();
            _player.Move(direction);
        }

        private void OnInventory()
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
        }

        private void OnInteract()
        {
            Debug.Log("interact");
            if (_player.Interactables.Count > 0)
            {
                IInteractable interactable = _player.Interactables[_player.Interactables.Count - 1];
                interactable.Interact();
                _player.Interactables.Remove(interactable);
            }
        }

        private void OnAttack()
        {
            _player.Attack();
        }
    }
}
