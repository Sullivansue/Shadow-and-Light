namespace Runtime
{
    public class State
    {
        protected Player _player;
        protected StateMachine _stateMachine;

        public State(Player player, StateMachine stateMachine)
        {
            _player = player;
            _stateMachine = stateMachine;
        }

        public virtual void EnterState(){}
        
        public virtual void UpdateState(){}
        
        public virtual void ExitState(){}
    }
}