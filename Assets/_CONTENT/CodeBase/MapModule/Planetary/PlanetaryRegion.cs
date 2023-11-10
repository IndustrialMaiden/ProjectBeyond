using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.Planetary
{
    public class PlanetaryRegion : MonoBehaviour
    {
        public Faction Faction;
        
        [HideInInspector] public int Index;
        [HideInInspector] public Vector2 Center;

        public List<PlanetaryRegion> Neighbours = new List<PlanetaryRegion>();

        public PolygonCollider2D Collider;
        public MeshRenderer MeshRenderer;
    }
}