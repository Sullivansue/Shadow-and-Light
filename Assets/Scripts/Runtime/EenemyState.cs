namespace Runtime
{
    public class EenemyState
    {
        protected Enemy _enemy;
        protected EnemyStateMachine _stateMachine;

        public EenemyState(Enemy enemy, EnemyStateMachine stateMachine)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
        }

        public virtual void EnterState(){}
        
        public virtual void UpdateState(){}
        
        public virtual void ExitState(){}
    }
}