using _CONTENT.CodeBase.MapModule.StarSystem;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule
{
    public class MapInteractionController : MonoBehaviour
    {
        private RaycastHit2D _previousHit;
        private Region _region;
        
        private void Update()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider == _previousHit.collider) return;
                _previousHit = hit;

                var region = hit.transform.GetComponent<Region>();
                if (region != _region && _region != null) _region.ActivateSelection(false);
                _region = region;
                _region.ActivateSelection(true);
            }
            else
            {
                if (_region != null) _region.ActivateSelection(false);
                _previousHit = new RaycastHit2D();
            }
        }
    }
}