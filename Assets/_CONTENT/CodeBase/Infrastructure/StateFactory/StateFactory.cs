using _CONTENT.CodeBase.Infrastructure.StateControl;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.StateFactory
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _projectContainer;
        private DiContainer _sceneContainer;

        public StateFactory(DiContainer projectContainer)
        {
            _projectContainer = projectContainer;
        }

        public void SetSceneContainer(DiContainer sceneContainer)
        {
            _sceneContainer = sceneContainer;
        }

        public TState CreateState<TState>() where TState : class, IState
        {
            if (_sceneContainer != null)
            {
                return _sceneContainer.Instantiate<TState>();
            }

            return _projectContainer.Instantiate<TState>();
        }

        public TState CreateState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            if (_sceneContainer != null)
            {
                return _sceneContainer.Instantiate<TState>(new[] { payload as object });
            }

            return _projectContainer.Instantiate<TState>(new[] { payload as object });
        }
    }

}