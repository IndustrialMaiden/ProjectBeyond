using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class PlanetNear : MonoBehaviour
    {
        public int Index { get; private set; }
        
        private List<Region> _regions = new List<Region>();

        public void Activate() => 
            gameObject.SetActive(true);
        
        public void Deactivate() => 
            gameObject.SetActive(false);

        public void SetIndex(int planetIndex) => 
            Index = planetIndex;

        public void AddRegion(Region region) => 
            _regions.Add(region);

        public List<Region> GetRegions() => 
            _regions;
    }
}