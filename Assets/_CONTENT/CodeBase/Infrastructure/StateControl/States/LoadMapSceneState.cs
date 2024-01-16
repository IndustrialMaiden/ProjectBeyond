using System;
using _CONTENT.CodeBase.Infrastructure.SceneControl;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration;
using UnityEngine;
using Random = System.Random;

namespace _CONTENT.CodeBase.Infrastructure.StateControl.States
{
    public class LoadMapSceneState : IState
    {
        private const string Map = "Map";
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        public LoadMapSceneState(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _sceneLoader.Load(Map, onLoaded: OnMapSceneLoaded);
        }

        private void OnMapSceneLoaded()
        {
            _stateMachine.Enter<MapGenerationState>();
        }

        public void Exit()
        {
            //
        }
    }
}