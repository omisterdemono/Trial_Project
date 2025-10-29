using UnityEngine;

namespace EnemySystem
{
    public class IdleState : EnemyState
    {
        private float _timer;
        private float _idleTime;

        public IdleState(Enemy enemy) : base(enemy) { }

        public override void Enter()
        {
            _enemy.Movement.Move(Vector2.zero);
            _idleTime = Random.Range(2f, 5f);
            _timer = 0f;
        }

        public override void UpdateLogic()
        {
            _timer += Time.deltaTime;
            if (_timer >= _idleTime)
            {
                _enemy.ChangeState(new PatrolState(_enemy));
            }
        }
    }
}