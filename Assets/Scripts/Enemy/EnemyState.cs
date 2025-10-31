namespace EnemySystem
{
    public abstract class EnemyState
    {
        protected readonly Enemy _enemy;

        protected EnemyState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }
}