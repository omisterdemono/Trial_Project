using Common;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerControls), typeof(AnimationController), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private AnimationController _animationController;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animationController = GetComponent<AnimationController>();
        }

        private void Update()
        {
            _animationController.SetMoveDirection(_rigidbody.linearVelocity);
        }
    }
}
