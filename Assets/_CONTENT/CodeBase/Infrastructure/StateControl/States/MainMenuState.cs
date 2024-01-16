using System;
using _CONTENT.CodeBase.Infrastructure.SceneControl;

namespace _CONTENT.CodeBase.Infrastructure.StateControl.States
{
    public class MainMenuState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        public MainMenuState(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _stateMachine.Enter<LoadMapSceneState>();
        }

        public void Exit()
        {
            //
        }
    }
}