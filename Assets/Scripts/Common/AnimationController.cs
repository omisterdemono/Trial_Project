using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        private Animator _animator;

        private readonly int _horizontalHash = Animator.StringToHash("Horizontal");
        private readonly int _verticalHash = Animator.StringToHash("Vertical");
        private readonly int _walkHash = Animator.StringToHash("Walk");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMoveDirection(Vector2 direction)
        {
            if (direction.sqrMagnitude > 0)
            {
                _animator.SetFloat(_horizontalHash, direction.x);
                _animator.SetFloat(_verticalHash, direction.y);
                _animator.SetBool(_walkHash, true);
            }
            else
            {
                _animator.SetBool(_walkHash, false);
            }
        }

        public void PlayTrigger(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public bool IsPlaying(string stateName)
        {
            return _animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }
    }
}
