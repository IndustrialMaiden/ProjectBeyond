using System;
using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.StarSystem.Regions;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class PlanetNear : MonoBehaviour
    {
        public int Index { get; private set; }
        
        private List<Region> _regions = new List<Region>();
        [SerializeField] private RegionActivation _regionActivation;

        public void Activate() =>
            _regionActivation.Show();
        
        public void Deactivate() => 
            _regionActivation.Hide();

        public void SetIndex(int planetIndex) => 
            Index = planetIndex;

        public void AddRegion(Region region) => 
            _regions.Add(region);

        public List<Region> GetRegions() => 
            _regions;
    }
    
}