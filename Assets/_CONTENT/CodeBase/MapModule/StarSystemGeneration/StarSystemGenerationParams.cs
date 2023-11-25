using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetFarObjects;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    [CreateAssetMenu(fileName = "Start System Generation Parameters", menuName = "Generation/StarSystemGenerationParameters", order = 0)]
    public class StarSystemGenerationParams : ScriptableObject
    {
        [field: Header("Star System"), Space]
        [field: SerializeField] public Vector3 StarSystemCoordinates { get; private set; }
        [field: SerializeField] public int PlanetsCount { get; private set; } 
        [field: SerializeField, Range(0, 5f)] public float MovingSpeedScale { get; private set; } 
        [field: SerializeField] public Vector2 PossiblePlanetSize { get; private set; }
        [field: SerializeField] public Vector2 PossiblePlanetDistance { get; private set; }
        
        [field: Header("Planets"), Space]
        // Количество регионов должно браться из сохранения игрока
        [field: SerializeField] public int PlanetRegionsCount{ get; private set; }
        [field: SerializeField] public float RegionsMapHeight { get; private set; }
        [field: SerializeField] public float RegionsMapWidth { get; private set; }
        
        [field: Header("Regions Noise"), Space]
        [field: SerializeField] public float PointSpacing { get; private set; }
        [field: SerializeField] public float NoiseScale { get; private set; }
        [field: SerializeField] public float NoiseResolution { get; private set; }
        
        [field: Header("Prefabs"), Space]
        [field: SerializeField] public StarSystemComponent StarSystemComponentPrefab { get; private set; }
        [field: SerializeField] public PlanetOrbitDrawer OrbitPrefab { get; private set; }
        [field: SerializeField] public SystemCenter SystemCenterPrefab { get; private set; }
        [field: SerializeField] public PlanetFar PlanetFarPrefab { get; private set; }
        [field: SerializeField] public PlanetNear PlanetNearPrefab { get; private set; }
        [field: SerializeField] public Region PlanetRegionPrefab { get; private set; }
        
        
        
        
    }
}