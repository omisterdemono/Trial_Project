using UnityEngine;

namespace EnemySystem
{
    public class AttackState : EnemyState
    {
        private bool _hasAttacked;

        public AttackState(Enemy enemy) : base(enemy) { }

        public override void Enter()
        {
            _enemy.Movement.Move(Vector2.zero);
            Debug.Log("Attack!");
            _hasAttacked = true;
        }

        public override void UpdateLogic()
        {
            if (_hasAttacked)
            {
                _enemy.ChangeState(new IdleState(_enemy));
            }
        }
    }
}
