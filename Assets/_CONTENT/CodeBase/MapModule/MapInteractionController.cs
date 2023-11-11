using System;
using _CONTENT.CodeBase.Demo;
using _CONTENT.CodeBase.MapModule.Planetary;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule
{
    public class MapInteractionController : MonoBehaviour
    {
        [SerializeField] private UIDemoController _ui;
        [SerializeField] private LineRenderer _regionSelection;
        private RaycastHit2D _previousHit; 
        
        private void Update()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider == _previousHit.collider) return;
                _previousHit = hit;

                var region = hit.transform.GetComponent<PlanetaryRegion>();
                var path = region.SelectionPath;

                _regionSelection.positionCount = path.Length;
                _regionSelection.SetPositions(path);
                
                _regionSelection.gameObject.SetActive(true);
                
                _ui.SetTooltip(region.Faction);
                _ui.Tooltip.transform.position = new Vector3(region.Center.x, region.Center.y, 0);
                _ui.Tooltip.gameObject.SetActive(true);
            }
            else
            {
                _regionSelection.gameObject.SetActive(false);
                _previousHit = new RaycastHit2D();
                _ui.Tooltip.gameObject.SetActive(false);
            }
        }
    }
}