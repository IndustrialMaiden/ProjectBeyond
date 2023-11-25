using System;
using UnityEngine;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.MouseInteraction
{
    public class MouseEventSystem : ITickable
    {
        public event Action<ISelectable> OnMouseOverEvent;
        public event Action<ISelectable> OnMouseExitEvent;
        public event Action<IClickable> OnMouseLeftClickEvent;
        public event Action<IClickable> OnMouseRightClickEvent;
        public event Action OnMouseRightClickNoHitEvent;


        private const string InteractableLayerName = "Interactable";

        private GameObject lastHitObject;
        public LayerMask interactionLayer;
        private float maxRaycastDistance = 10f;

        public MouseEventSystem()
        {
            interactionLayer = LayerMask.GetMask(InteractableLayerName);
        }

        public void Tick()
        {
            PerformRaycast();
        }

        private void PerformRaycast()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, maxRaycastDistance, interactionLayer);


            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                HandleRaycastHit(hitObject);
            }
            
            else if (hit.collider == null && Input.GetMouseButtonDown(1)) // Правая кнопка мыши
            {
                OnMouseRightClickNoHitEvent?.Invoke();
            }

            else if (lastHitObject != null)
            {
                HandleRaycastMiss();
            }
        }

        private void HandleRaycastHit(GameObject hitObject)
        {
            if (lastHitObject != hitObject)
            {
                if (lastHitObject != null)
                {
                    ISelectable lastSelectable = lastHitObject.GetComponent<ISelectable>();
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

                lastHitObject = hitObject;
            }

            ProcessMouseClick(hitObject, 0); // Левая кнопка мыши
            ProcessMouseClick(hitObject, 1); // Правая кнопка мыши
        }


        private void HandleRaycastMiss()
        {
            ISelectable lastSelectable = lastHitObject.GetComponent<ISelectable>();
            if (lastSelectable != null)
            {
                OnMouseExitEvent?.Invoke(lastSelectable);
            }

            lastHitObject = null;
        }

        private void ProcessMouseClick(GameObject hitObject, int mouseButton)
        {
            IClickable clickable = hitObject ? hitObject.GetComponent<IClickable>() : null;

            if (mouseButton == 0 && Input.GetMouseButtonDown(mouseButton)) // Левая кнопка мыши
            {
                if (clickable != null)
                {
                    OnMouseLeftClickEvent?.Invoke(clickable);
                }
            }
            else if (mouseButton == 1 && Input.GetMouseButtonDown(mouseButton)) // Правая кнопка мыши
            {
                if (clickable != null)
                {
                    OnMouseRightClickEvent?.Invoke(clickable);
                }
                else
                {
                    // Правая кнопка мыши нажата, но объект не реализует IClickable
                    OnMouseRightClickNoHitEvent?.Invoke();
                }
            }
        }

    }
}