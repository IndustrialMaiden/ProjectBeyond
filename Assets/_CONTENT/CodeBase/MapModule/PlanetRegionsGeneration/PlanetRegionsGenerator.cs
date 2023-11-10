using System;
using _CONTENT.CodeBase.MapModule.Planetary;
using _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration.Graph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        private Map _map;

        [Header("Seed")]
        public int Seed;

        [Space][Header("Map")]
        [SerializeField] private int _regionsCount;
        [SerializeField] private float _width;
        [SerializeField] private float _height;

        [Space] [Header("Noise Generation")]
        [SerializeField] private float _pointSpacing;
        [SerializeField] private float _noiseScale;
        [SerializeField] private float _noiseResolution;

        [Space] [Header("Regenerate")]
        public bool Regenerate;

        [Space] [Header("Test Prefabs")]
        [SerializeField] public Planet PlanetPrefab;
        [SerializeField] public PlanetaryRegion RegionPrefab;

        public Planet planet;


        private void Start()
        {
            RegeneratePlanet();
        }

        void Update()
        {
            if (Regenerate)
            {
                Regenerate = false;
                RegeneratePlanet();
            }
        }

        private void RegeneratePlanet()
        {
            if (Seed < 0)
            {
                Seed = Environment.TickCount;
                Random.InitState(Seed);
            }

            else
            {
                Random.InitState(Seed);
            }

            _map = new Map(_regionsCount, _width, _height, _pointSpacing, _noiseScale, _noiseResolution);
            if (planet != null)
            {
                Destroy(planet.gameObject);
            }
            CreatePlanet();

            Seed = -10;
        }

        private void CreatePlanet()
        {
            planet = Instantiate(PlanetPrefab, Vector3.zero, Quaternion.identity);

            foreach (var center in _map.Graph.centers)
            {
                CreateRegion(center, planet);
            }
            
            foreach (var region in planet.Regions)
            {
                AssignNeighbors(region, _map.Graph.centers[region.Index], planet);
            }
        }

        private void CreateRegion(Center center, Planet planet)
        {
            var region = Instantiate(RegionPrefab, Vector3.zero, Quaternion.identity, planet.transform);
            region.Index = center.index;
            region.Center = center.point;
            region.name = $"Region {region.Index}";
            region.Faction = (Faction)Random.Range(0, 5);
            region.Collider.points = center.noisyPoints.ToArray();
            ApplyFactionMaterial(region);
            CreateBorder(region);
            planet.Regions.Add(region);
        }

        private void AssignNeighbors(PlanetaryRegion region, Center center, Planet planet)
        {
            foreach (var index in center.neighborsIndexes)
            {
                region.Neighbours.Add(planet.Regions[index]);
            }
        }

        private void ApplyFactionMaterial(PlanetaryRegion region)
        {
            switch (region.Faction)
            {
                case Faction.Insects:
                    region.MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Insects_Mat");
                    break;
                case Faction.Demons:
                    region.MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Demons_Mat");
                    break;
                case Faction.Mechanoids:
                    region.MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Mechanoids_Mat");
                    break;
                case Faction.Mages:
                    region.MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Mages_Mat");
                    break;
                case Faction.Necrons:
                    region.MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Necrons_Mat");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateBorder(PlanetaryRegion region)
        {
            Vector3[] linePositions = new Vector3[region.Collider.GetPath(0).Length];
            var colliderPositions = region.Collider.GetPath(0);
            for (int i = 0; i < colliderPositions.Length; i++)
            {
                linePositions[i] = new Vector3(colliderPositions[i].x, colliderPositions[i].y, -5);
            }

            var lineRenderer = region.GetComponent<LineRenderer>();
            lineRenderer.positionCount = linePositions.Length;
            lineRenderer.SetPositions(linePositions);

        }
    }
}