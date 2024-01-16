using System;
using _CONTENT.CodeBase.MapModule.CameraControl;

namespace _CONTENT.CodeBase.Infrastructure.StateControl.States
{
    public class StarSystemViewState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly CameraSwitchSystem _cameraSwitchSystem;

        public StarSystemViewState(StateMachine stateMachine, CameraSwitchSystem cameraSwitchSystem)
        {
            _stateMachine = stateMachine;
            _cameraSwitchSystem = cameraSwitchSystem;
        }

        public void Enter()
        {
            _cameraSwitchSystem.ActivateStarSystemCamera();
            _cameraSwitchSystem.SetInteractionCamera(MapCamera.StarSystem);
        }

        public void Exit()
        {
            //
        }
    }
}