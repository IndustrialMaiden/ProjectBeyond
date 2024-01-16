using System;
using _CONTENT.CodeBase.Infrastructure.SceneControl;

namespace _CONTENT.CodeBase.Infrastructure.StateControl.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _stateMachine.Enter<MainMenuState>();
        }

        private void OnSceneLoaded()
        {
            
        }

        public void Exit()
        {
            //
        }
    }
}