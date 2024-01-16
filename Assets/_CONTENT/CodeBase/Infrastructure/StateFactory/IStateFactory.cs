using _CONTENT.CodeBase.Infrastructure.StateControl;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.StateFactory
{
    public interface IStateFactory
    {
        void SetSceneContainer(DiContainer sceneContainer);
        TState CreateState<TState>() where TState : class, IState;
        TState CreateState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}