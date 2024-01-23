using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.StarSystem.Regions;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class PlanetaryMapData
    {
        public int Index { get; }

        public List<RegionData> RegionsData = new List<RegionData>();

        public PlanetaryMapData(int index, List<RegionData> regionsData)
        {
            Index = index;
            RegionsData = regionsData;
        }
    }
}