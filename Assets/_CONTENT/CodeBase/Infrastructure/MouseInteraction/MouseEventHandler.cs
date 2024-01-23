using System;
using _CONTENT.CodeBase.Infrastructure.StateControl;
using _CONTENT.CodeBase.Infrastructure.StateControl.States;

namespace _CONTENT.CodeBase.Infrastructure.MouseInteraction
{
    public class MouseEventHandler : IDisposable
    {
        private readonly MouseEventSystem _mouseEventSystem;
        private readonly StateMachine _stateMachine;

        public MouseEventHandler(MouseEventSystem mouseEventSystem, StateMachine stateMachine)
        {
            _mouseEventSystem = mouseEventSystem;
            _stateMachine = stateMachine;
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