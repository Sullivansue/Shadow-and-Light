namespace Runtime
{
    public class StateMachine
    {
        public State currentState { get; set; }

        public void Initialize(State initialState)
        {
            currentState = initialState;
            currentState.EnterState();
        }

        public void ChangeState(State newState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }
    }
}