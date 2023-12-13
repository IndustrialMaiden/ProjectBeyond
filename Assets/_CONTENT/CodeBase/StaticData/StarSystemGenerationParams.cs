using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetFarObjects;
using UnityEngine;

namespace _CONTENT.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "Start System Generation Parameters", menuName = "Static Data/StarSystemGenerationParameters", order = 0)]
    public class StarSystemGenerationParams : ScriptableObject
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
        [field: SerializeField] public StarSystemComponent StarSystemComponentPrefab { get; private set; }
        [field: SerializeField] public PlanetOrbitDrawer OrbitPrefab { get; private set; }
        [field: SerializeField] public SystemCenter SystemCenterPrefab { get; private set; }
        [field: SerializeField] public PlanetFar PlanetFarPrefab { get; private set; }
        [field: SerializeField] public PlanetNear PlanetNearPrefab { get; private set; }
        [field: SerializeField] public Region PlanetRegionPrefab { get; private set; }
        
        [field: Header("Materials"), Space]
        
        [field: SerializeField] public Material PlanetMaterialType1 { get; private set; }
        [field: SerializeField] public Material PlanetMaterialType2 { get; private set; }
        
        [field: Header("Planet Gradients"), Space]
        
        [field: SerializeField] public GradientsCollection PlanetGradients { get; private set; }

        
        
        
        
        
    }
}