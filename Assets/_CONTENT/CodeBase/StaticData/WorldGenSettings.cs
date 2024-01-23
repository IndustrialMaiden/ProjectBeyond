using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using _CONTENT.CodeBase.MapModule.StarSystem.Regions;
using UnityEngine;

namespace _CONTENT.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "World Generation Settings", menuName = "Static Data/WorldGenSettings", order = 0)]
    public class WorldGenSettings : ScriptableObject
    {
        public Vector3 StarSystemCenter => 
            new Vector3(RegionsMapWidth / 2, RegionsMapHeight / 2, 1) + new Vector3(-300, 0, 0);
        public Vector3 PlanetaryRegionsCenter => new Vector3(RegionsMapWidth / 2, RegionsMapHeight / 2, 1);
        
        [field: Header("Star System"), Space]
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
        [field: SerializeField] public StarSystemRoot StarSystemRootPrefab { get; private set; }
        [field: SerializeField] public PlanetOrbitDrawer OrbitPrefab { get; private set; }
        [field: SerializeField] public SystemCenter SystemCenterPrefab { get; private set; }
        [field: SerializeField] public PlanetView PlanetViewPrefab { get; private set; }
        [field: SerializeField] public PlanetaryMapView PlanetaryMapViewPrefab { get; private set; }
        [field: SerializeField] public RegionView PlanetRegionViewPrefab { get; private set; }
        
        [field: Header("Materials"), Space]
        
        [field: SerializeField] public Material PlanetMaterialType1 { get; private set; }
        [field: SerializeField] public Material PlanetMaterialType2 { get; private set; }
        
        [field: Header("Planet Gradients"), Space]
        
        [field: SerializeField] public GradientsCollection PlanetGradients { get; private set; }

        
        
        
        
        
    }
}