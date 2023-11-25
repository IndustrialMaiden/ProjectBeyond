using System;
using _CONTENT.CodeBase.MapModule.CameraControl;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetFarObjects;
using UnityEngine;

namespace _CONTENT.CodeBase.Infrastructure.MouseInteraction
{
    public class MouseEventHandler : IDisposable
    {
        private readonly MouseEventSystem _mouseEventSystem;
        private readonly CameraSwitchSystem _cameraSwitchSystem;

        public MouseEventHandler(MouseEventSystem mouseEventSystem, CameraSwitchSystem cameraSwitchSystem)
        {
            _mouseEventSystem = mouseEventSystem;
            _cameraSwitchSystem = cameraSwitchSystem;
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
            if (clickable is PlanetFar planetFar)
            {
                _cameraSwitchSystem.ActivatePlanetaryCamera(planetFar.Index);
            }
        }

        private void OnMouseRightClick(IClickable clickable)
        {
            Debug.Log("Right click on " + clickable);
        }

        private void OnMouseRightClickNoHit()
        {
            _cameraSwitchSystem.ActivateStarSystemCamera();
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