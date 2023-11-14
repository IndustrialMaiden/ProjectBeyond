using System;
using _CONTENT.CodeBase.MapModule.Planetary;
using _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration.Graph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        private PlanetaryMap _planetaryMap;

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

        [Space] [Header("Test Prefabs")]
        [SerializeField] public Planet PlanetPrefab;
        [SerializeField] public RegionData regionDataPrefab;

        [HideInInspector] public Planet planet;


        private void Start()
        {
            Random.InitState(Seed);
            
            if (planet != null) Destroy(planet.gameObject);
            
            CreatePlanet();
        }

        private void CreatePlanet()
        {
            _planetaryMap = new PlanetaryMap(_regionsCount, _width, _height, _pointSpacing, _noiseScale, _noiseResolution);
            
            planet = Instantiate(PlanetPrefab, Vector3.zero, Quaternion.identity);

            foreach (var center in _planetaryMap.Graph.centers)
            {
                var region = Instantiate(regionDataPrefab, Vector3.zero, Quaternion.identity, planet.transform);
                region.Construct(center);
                planet.Regions.Add(region);
            }
            
            foreach (var region in planet.Regions)
            {
                AssignNeighbors(region, _planetaryMap.Graph.centers[region.Index], planet);
            }
        }

        private void AssignNeighbors(RegionData regionData, Center center, Planet planet)
        {
            foreach (var index in center.neighborsIndexes)
            {
                regionData.AddNeighbour(planet.Regions[index]);
            }
        }
    }
}