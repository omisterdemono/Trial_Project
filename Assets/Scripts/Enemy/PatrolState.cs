using UnityEngine;

namespace EnemySystem
{
    public class PatrolState : EnemyState
    {
        private Vector2 _target;
        private bool _useWaypoints;
        private float _stopDistance = 0.2f;

        public PatrolState(Enemy enemy) : base(enemy)
        {
            _useWaypoints = enemy.PatrolPoints != null && enemy.PatrolPoints.Length > 0;
        }

        public override void Enter()
        {
            SetNextTarget();
        }

        public override void UpdateLogic()
        {
            Vector2 dir = ((Vector2)_target - (Vector2)_enemy.transform.position);

            if (dir.magnitude <= _stopDistance)
            {
                _enemy.Movement.Move(Vector2.zero);
                _enemy.ChangeState(new IdleState(_enemy));
                return;
            }

            _enemy.Movement.Move(dir.normalized);
        }

        private void SetNextTarget()
        {
            if (_useWaypoints)
            {
                _target = _enemy.PatrolPoints[_enemy.PatrolIndex].position;

                _enemy.PatrolIndex++;

                if (_enemy.PatrolIndex >= _enemy.PatrolPoints.Length)
                {
                    _enemy.PatrolIndex = 0;
                }
            }
            else
            {
                // Random patrol within radius
                _target = (Vector2)_enemy.transform.position + Random.insideUnitCircle * _enemy.PatrolRadius;
            }
        }
    }
}
