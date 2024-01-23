using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.MouseInteraction;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.Regions
{
    public class RegionView : MonoBehaviour, ISelectable
    {
        [SerializeField] private LineRenderer _border;
        [SerializeField] private LineRenderer _selection;
        public RegionData RegionData { get; private set; }
        
        public List<RegionView> Neighbours = new List<RegionView>();
        
        private bool _isSelectionActive;

        public void Construct(RegionData regionData, Vector3[] bordersPositions)
        {
            RegionData = regionData;

            _border.positionCount = bordersPositions.Length;
            _border.SetPositions(bordersPositions);
            
            _selection.positionCount = bordersPositions.Length;
            _selection.SetPositions(bordersPositions);
        }

        public void ActivateSelection(bool state)
        {
            _selection.gameObject.SetActive(state);
        }

        public void OnMouseOverSelectable()
        {
            if (_isSelectionActive) return;
            
            ActivateSelection(true);
            _isSelectionActive = true;
        }

        public void OnMouseExitSelectable()
        {
            if (!_isSelectionActive) return;
            
            ActivateSelection(false);
            _isSelectionActive = false;
        }
    }
}