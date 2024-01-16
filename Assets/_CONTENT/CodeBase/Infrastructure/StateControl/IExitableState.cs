namespace _CONTENT.CodeBase.Infrastructure.StateControl
{
    public interface IState : IExitableState
    {
        public void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }

    public interface IExitableState
    {
        public void Exit();
    }
}