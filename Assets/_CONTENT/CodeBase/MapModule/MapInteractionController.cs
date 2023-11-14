using _CONTENT.CodeBase.MapModule.Planetary;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule
{
    public class MapInteractionController : MonoBehaviour
    {
        private RaycastHit2D _previousHit;
        private RegionData _regionData;
        
        private void Update()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider == _previousHit.collider) return;
                _previousHit = hit;

                var region = hit.transform.GetComponent<RegionData>();
                if (region != _regionData && _regionData != null) _regionData.ActivateSelection(false);
                _regionData = region;
                _regionData.ActivateSelection(true);
            }
            else
            {
                if (_regionData != null) _regionData.ActivateSelection(false);
                _previousHit = new RaycastHit2D();
            }
        }
    }
}