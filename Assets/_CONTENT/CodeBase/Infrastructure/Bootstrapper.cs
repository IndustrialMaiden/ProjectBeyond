using System;
using System.Collections;
using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.StateControl;
using _CONTENT.CodeBase.Infrastructure.StateControl.States;
using UnityEngine;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private StateMachine _stateMachine;

        [Inject]
        public void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _stateMachine.Enter<BootstrapState>();
        }
    }
}