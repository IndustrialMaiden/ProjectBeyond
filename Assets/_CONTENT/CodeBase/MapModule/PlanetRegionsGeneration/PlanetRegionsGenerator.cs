using System;
using _CONTENT.CodeBase.MapModule.Planetary;
using _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration.Graph;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        private PlanetaryMap _planetaryMap;
        public int PlanetIndex;

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
        [SerializeField] public PlanetInside planetInsidePrefab;
        [SerializeField] public Region regionPrefab;

        [HideInInspector] public PlanetInside planetInside;

        private RandomGenerator _random;


        private void Start()
        {
            _random = new RandomGenerator(Seed + PlanetIndex);
            
            if (planetInside != null) Destroy(planetInside.gameObject);
            
            CreatePlanet();
        }

        private void CreatePlanet()
        {
            _planetaryMap = new PlanetaryMap(_regionsCount, _random, _width, _height, _pointSpacing, _noiseScale, _noiseResolution);
            
            planetInside = Instantiate(planetInsidePrefab, Vector3.zero, Quaternion.identity);

            foreach (var center in _planetaryMap.Graph.centers)
            {
                var region = Instantiate(regionPrefab, Vector3.zero, Quaternion.identity, planetInside.transform);
                var faction = (Faction) _random.Range(0, 5);
                region.Construct(center, faction);
                planetInside.Regions.Add(region);
            }
            
            foreach (var region in planetInside.Regions)
            {
                AssignNeighbors(region, _planetaryMap.Graph.centers[region.Index], planetInside);
            }
        }

        private void AssignNeighbors(Region region, Center center, PlanetInside planetInside)
        {
            foreach (var index in center.neighborsIndexes)
            {
                region.AddNeighbour(planetInside.Regions[index]);
            }
        }
    }
}