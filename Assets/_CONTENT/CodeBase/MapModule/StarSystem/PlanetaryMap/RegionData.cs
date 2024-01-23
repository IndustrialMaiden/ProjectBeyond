using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.PlanetaryMap
{
    public class RegionData
    {
        public int Index { get; private set; }
        public Vector2 Center { get; private set; }
        public List<int> NeighborsIndexes = new List<int>();
        public Faction Faction { get; private set; }
        public Vector2[] V2ColliderPositions { get; private set; }

        public Material RegionMaterial;

        public RegionData(int index, Vector2 center, List<int> neighborsIndexes, Faction faction, Vector2[] v2ColliderPositions, Material regionMaterial)
        {
            Index = index;
            Center = center;
            NeighborsIndexes = neighborsIndexes;
            Faction = faction;
            V2ColliderPositions = v2ColliderPositions;
            RegionMaterial = regionMaterial;
        }
    }
}