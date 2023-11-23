using System;
using UnityEngine;

namespace _CONTENT.CodeBase.Infrastructure.MouseInteraction
{
    public class MouseEventHandler : IDisposable
    {
        private readonly MouseEventSystem _mouseEventSystem;

        public MouseEventHandler(MouseEventSystem mouseEventSystem)
        {
            _mouseEventSystem = mouseEventSystem;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _mouseEventSystem.OnMouseOverEvent += OnMouseOver;
            _mouseEventSystem.OnMouseExitEvent += OnMouseExit;
            _mouseEventSystem.OnMouseLeftClickEvent += OnMouseLeftClick;
            _mouseEventSystem.OnMouseRightClickEvent += OnMouseRightClick;
        }

        private void OnMouseOver(ISelectable selectable) => 
            selectable.OnMouseOverSelectable();

        private void OnMouseExit(ISelectable selectable) => 
            selectable.OnMouseExitSelectable();

        private void OnMouseLeftClick(IClickable clickable)
        {
            Debug.Log("Left click on " + clickable);
        }

        private void OnMouseRightClick(IClickable clickable)
        {
            Debug.Log("Right click on " + clickable);
        }

        public void Dispose()
        {
            _mouseEventSystem.OnMouseOverEvent -= OnMouseOver;
            _mouseEventSystem.OnMouseExitEvent -= OnMouseExit;
            _mouseEventSystem.OnMouseLeftClickEvent -= OnMouseLeftClick;
            _mouseEventSystem.OnMouseRightClickEvent -= OnMouseRightClick;
        }
    }
}