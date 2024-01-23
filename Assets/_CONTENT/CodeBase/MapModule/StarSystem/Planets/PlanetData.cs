using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar
{
    public class PlanetData
    {
        public int Index { get; } 
        public Vector3 StartPosition { get; }
        public float Size { get; } 
        public float Distance { get; } 
        public float SpeedScale { get; } 
        public PlanetMoveDirection PlanetMoveDirection { get; }
        public Vector3 GravityCenterPosition { get; }
        
        public Material PlanetMaterial { get; }

        public PlanetData(int index, Vector3 startPosition, float size, float distance, float speedScale, PlanetMoveDirection planetMoveDirection, Vector3 gravityCenterPosition, Material planetMaterial)
        {
            Index = index;
            StartPosition = startPosition;
            Size = size;
            Distance = distance;
            SpeedScale = speedScale;
            PlanetMoveDirection = planetMoveDirection;
            GravityCenterPosition = gravityCenterPosition;
            PlanetMaterial = planetMaterial;
        }
    }
}