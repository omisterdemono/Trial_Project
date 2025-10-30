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
        [SerializeField] private AnimationController _animationController;

        private List<IInteractable> _interactables = new List<IInteractable>();

        public List<IInteractable> Interactables { get => _interactables;}

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animationController = GetComponent<AnimationController>();
        }

        private void Update()
        {
            _animationController.SetMoveDirection(_rigidbody.linearVelocity);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.TryGetComponent<IInteractable>(out IInteractable interactable);

            if (interactable != null && !_interactables.Contains(interactable))
            {
                _interactables.Add(interactable);
                Debug.Log("aeqweqweqwe");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            collision.TryGetComponent<IInteractable>(out IInteractable interactable);

            _interactables.Remove(interactable);
        }
    }
}
