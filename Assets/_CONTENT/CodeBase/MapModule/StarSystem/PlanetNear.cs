using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class PlanetNear : MonoBehaviour
    {
        public int PlanetIndex { get; private set; }
        
        private List<Region> _regions = new List<Region>();

        public void SetIndex(int planetIndex)
        {
            PlanetIndex = planetIndex;
        }

        public void AddRegion(Region region)
        {
            _regions.Add(region);
        }

        public List<Region> GetRegions()
        {
            return _regions;
        }
    }
}