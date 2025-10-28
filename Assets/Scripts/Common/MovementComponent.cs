using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementComponent : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Settings")]
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _acceleration = 100f;
        [SerializeField] private float _deceleration = 100f;

        private Vector2 _moveDirection = Vector2.zero;
        private Vector2 _currentVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 targetVelocity = _moveDirection * _maxSpeed;

            _currentVelocity = Vector2.MoveTowards
            (
                _rigidbody.linearVelocity,
                targetVelocity,
                (_moveDirection.sqrMagnitude > 0 ? _acceleration : _deceleration) * Time.fixedDeltaTime
            );

            _rigidbody.linearVelocity = _currentVelocity;
        }

        public void Move(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
        }    
    }
}