using UnityEngine;

namespace EnemySystem
{
    public class FollowState : EnemyState
    {
        private Vector2 _target;

        public FollowState(Enemy enemy, Vector2 target) : base(enemy)
        {
            _target = target;
        }

        public override void UpdateLogic()
        {
            Vector2 dir = _target - (Vector2)_enemy.transform.position;

            if (dir.magnitude < 0.2f)
            {
                _enemy.ChangeState(new IdleState(_enemy));
                return;
            }

            _enemy.Movement.Move(dir.normalized);
        }
    }
}