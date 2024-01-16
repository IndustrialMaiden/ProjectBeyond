using System;
using _CONTENT.CodeBase.MapModule.CameraControl;
using UnityEngine;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.MouseInteraction
{
    public class MouseEventSystem : ITickable
    {
        private CameraSwitchSystem _cameraSwitchSystem;
        
        public event Action<ISelectable> OnMouseOverEvent;
        public event Action<ISelectable> OnMouseExitEvent;
        public event Action<IClickable> OnMouseLeftClickEvent;
        public event Action<IClickable> OnMouseRightClickEvent;
        public event Action OnMouseRightClickNoHitEvent;


        private const string Interactable = "Interactable";

        private GameObject _lastHitObject;
        private LayerMask _interactionLayer;
        private float maxRaycastDistance = 15f;

        public MouseEventSystem(CameraSwitchSystem cameraSwitchSystem)
        {
            _cameraSwitchSystem = cameraSwitchSystem;
            _interactionLayer = LayerMask.GetMask(Interactable);
        }

        public void Tick()
        {
            PerformRaycast();
        }

        private void PerformRaycast()
        {
            Vector2 mousePos = _cameraSwitchSystem.InteractionCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, maxRaycastDistance, _interactionLayer);

            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                HandleRaycastHit(hitObject);
            }

            else if (hit.collider == null && Input.GetMouseButtonDown(1))
            {
                OnMouseRightClickNoHitEvent?.Invoke();
            }

            else if (_lastHitObject != null)
            {
                HandleRaycastMiss();
            }
        }

        private void HandleRaycastHit(GameObject hitObject)
        {
            if (_lastHitObject != hitObject)
            {
                if (_lastHitObject != null)
                {
                    ISelectable lastSelectable = _lastHitObject.GetComponent<ISelectable>();
                    if (lastSelectable != null)
                    {
                        OnMouseExitEvent?.Invoke(lastSelectable);
                    }
                }

                ISelectable selectable = hitObject.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    OnMouseOverEvent?.Invoke(selectable);
                }

                _lastHitObject = hitObject;
            }

            if (Input.GetMouseButtonDown(0))
                ProcessMouseClick(hitObject, 0);

            else if (Input.GetMouseButtonDown(1))
                ProcessMouseClick(hitObject, 1);
        }

        private void ProcessMouseClick(GameObject hitObject, int mouseButton)
        {
            IClickable clickable = hitObject ? hitObject.GetComponent<IClickable>() : null;

            if (clickable != null && mouseButton == 0)
            {
                OnMouseLeftClickEvent?.Invoke(clickable);
            }

            else if (clickable != null && mouseButton == 1)
            {
                OnMouseRightClickEvent?.Invoke(clickable);
            }

            else if (clickable == null && mouseButton == 1)
            {
                OnMouseRightClickNoHitEvent?.Invoke();
            }
        }

        private void HandleRaycastMiss()
        {
            ISelectable lastSelectable = _lastHitObject.GetComponent<ISelectable>();
            if (lastSelectable != null)
            {
                OnMouseExitEvent?.Invoke(lastSelectable);
            }

            _lastHitObject = null;
        }
    }
}