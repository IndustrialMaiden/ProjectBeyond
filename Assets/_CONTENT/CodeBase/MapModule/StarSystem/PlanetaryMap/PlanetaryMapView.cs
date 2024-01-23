using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.PlanetaryMap
{
    public class PlanetaryMapView : MonoBehaviour
    {
        public PlanetaryMapData PlanetaryMapData { get; private set; } 
        
        [SerializeField] private List<RegionView> _regions = new List<RegionView>();
        [SerializeField] private RegionsActivation _regionActivation;

        public void Construct(PlanetaryMapData planetaryMapData, List<RegionView> regions)
        {
            PlanetaryMapData = planetaryMapData;
            _regions = regions;
        }

        public void Activate() =>
            _regionActivation.Show();
        
        public void Deactivate() => 
            _regionActivation.Hide();

        public List<RegionView> GetRegions() => 
            _regions;
    }
    
}