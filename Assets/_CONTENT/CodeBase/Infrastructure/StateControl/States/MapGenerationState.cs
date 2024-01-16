using System;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration;
using UnityEngine;
using Random = System.Random;

namespace _CONTENT.CodeBase.Infrastructure.StateControl.States
{
    public class MapGenerationState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly StarSystemGenerator _starSystemGenerator;

        public MapGenerationState(StateMachine stateMachine, StarSystemGenerator starSystemGenerator)
        {
            _stateMachine = stateMachine;
            _starSystemGenerator = starSystemGenerator;
        }

        public void Enter()
        {
            Random random = new Random();
            var seed = random.Next(0, Int32.MaxValue);
            PlayerPrefs.SetInt("SEED", seed);
            _starSystemGenerator.GenerateStarSystem(seed, onGenerated: OnMapGenerated);
        }

        private void OnMapGenerated()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
            
        }
    }
}