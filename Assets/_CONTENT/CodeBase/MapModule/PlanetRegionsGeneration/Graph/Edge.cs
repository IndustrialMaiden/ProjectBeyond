using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration.Graph
{
    public class Edge
    {
        public int index;
        
        public Center d0, d1;
        public Corner v0, v1;
        public Vector2 midpoint;
    }
}
