using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.StateControl
{
    public interface IStateFactory
    {
        void SetSceneContainer(DiContainer sceneContainer);
        TState CreateState<TState>() where TState : class, IState;
        TState CreateState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}