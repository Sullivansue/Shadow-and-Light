namespace Runtime
{
    public class EnemyStateMachine
    {
        public EenemyState currentState { get; set; }

        public void Initialize(EenemyState initialState)
        {
            currentState = initialState;
            currentState.EnterState();
        }

        public void ChangeState(EenemyState newState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }
    }
}