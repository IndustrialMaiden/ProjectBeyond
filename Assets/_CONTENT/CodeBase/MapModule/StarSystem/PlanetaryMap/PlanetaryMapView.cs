using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.StarSystem.Regions;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class PlanetaryMapView : MonoBehaviour
    {
        public PlanetaryMapData PlanetaryMapData { get; private set; } 
        
        [SerializeField] private List<RegionView> _regions = new List<RegionView>();
        [SerializeField] private RegionActivation _regionActivation;

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