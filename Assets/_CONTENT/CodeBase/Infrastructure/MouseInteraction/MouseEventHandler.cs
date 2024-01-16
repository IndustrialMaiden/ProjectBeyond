using System;
using _CONTENT.CodeBase.Infrastructure.StateControl;
using _CONTENT.CodeBase.Infrastructure.StateControl.States;
using _CONTENT.CodeBase.MapModule;
using _CONTENT.CodeBase.MapModule.CameraControl;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;

namespace _CONTENT.CodeBase.Infrastructure.MouseInteraction
{
    public class MouseEventHandler : IDisposable
    {
        private readonly MouseEventSystem _mouseEventSystem;
        private readonly StateMachine _stateMachine;
        private readonly MapSceneData _mapSceneData;

        public MouseEventHandler(MouseEventSystem mouseEventSystem, StateMachine stateMachine, CameraSwitchSystem cameraSwitchSystem, MapSceneData mapSceneData)
        {
            _mouseEventSystem = mouseEventSystem;
            _stateMachine = stateMachine;
            _mapSceneData = mapSceneData;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _mouseEventSystem.OnMouseOverEvent += OnMouseOver;
            _mouseEventSystem.OnMouseExitEvent += OnMouseExit;
            _mouseEventSystem.OnMouseLeftClickEvent += OnMouseLeftClick;
            _mouseEventSystem.OnMouseRightClickEvent += OnMouseRightClick;
            _mouseEventSystem.OnMouseRightClickNoHitEvent += OnMouseRightClickNoHit;
        }

        private void OnMouseOver(ISelectable selectable) => 
            selectable.OnMouseOverSelectable();

        private void OnMouseExit(ISelectable selectable) => 
            selectable.OnMouseExitSelectable();

        private void OnMouseLeftClick(IClickable clickable)
        {
            if (clickable is PlanetFar planetFar && _mapSceneData.ActivePlanetNearIndex == -1)
            {
                _stateMachine.Enter<PlanetViewState, int>(planetFar.Index);
                return;
            }
            
            clickable.OnLeftClick();
        }

        private void OnMouseRightClick(IClickable clickable)
        {
            clickable.OnRightClick();
        }

        private void OnMouseRightClickNoHit()
        {
            _stateMachine.Enter<StarSystemViewState>();
        }

        public void Dispose()
        {
            _mouseEventSystem.OnMouseOverEvent -= OnMouseOver;
            _mouseEventSystem.OnMouseExitEvent -= OnMouseExit;
            _mouseEventSystem.OnMouseLeftClickEvent -= OnMouseLeftClick;
            _mouseEventSystem.OnMouseRightClickEvent -= OnMouseRightClick;
            _mouseEventSystem.OnMouseRightClickNoHitEvent -= OnMouseRightClickNoHit;
        }
    }
}