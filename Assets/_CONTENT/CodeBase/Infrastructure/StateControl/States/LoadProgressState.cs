using _CONTENT.CodeBase.Infrastructure.SceneControl;

namespace _CONTENT.CodeBase.Infrastructure.StateControl.States
{
    public class LoadProgressState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadProgressState(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _stateMachine.Enter<StarSystemViewState>();
        }

        public void Exit()
        {
            //
        }
    }
}