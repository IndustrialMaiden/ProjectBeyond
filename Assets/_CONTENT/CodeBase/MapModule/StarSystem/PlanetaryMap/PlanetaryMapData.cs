using System.Collections.Generic;

namespace _CONTENT.CodeBase.MapModule.StarSystem.PlanetaryMap
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