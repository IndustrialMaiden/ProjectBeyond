namespace _CONTENT.CodeBase.Infrastructure.StateControl
{
    public class StateMachine
    {
        private readonly IStateFactory _stateFactory;
        private IExitableState _activeState;
        public IExitableState ActiveState => _activeState;

        public StateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState, TPayload>(payload);
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            TState state = _stateFactory.CreateState<TState>();
            _activeState = state;
            return state;
        }

        private TState ChangeState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            _activeState?.Exit();
            TState state = _stateFactory.CreateState<TState, TPayload>(payload);
            _activeState = state;
            return state;
        }
    }

}