using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using _CONTENT.CodeBase.MapModule.Graph;
using _CONTENT.CodeBase.MapModule.Planetary;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CONTENT.CodeBase.MapModule
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

        [Space][Header("Select Next Region")]
        private int SelectedRegion = 0;

        [Space] [Header("Regenerate")]
        public bool Regenerate;

        [Space] [Header("Test Prefabs")]
        [SerializeField] public Planet PlanetPrefab;
        [SerializeField] public PlanetaryRegion RegionPrefab;

        private Planet planet;


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
            
            Debug.Log(Seed);
            

            _map = new Map(_regionsCount, _width, _height, _pointSpacing, _noiseScale, _noiseResolution);
            if (planet != null)
            {
                Destroy(planet.gameObject);
            }
            CreatePlanet();

            Seed = -10;
        }

        private void Start()
        {
            RegeneratePlanet();
        }
        
        private void CreatePlanet()
        {
            planet = Instantiate(PlanetPrefab, Vector3.zero, Quaternion.identity);

            // Создание регионов и назначение соседей
            foreach (var center in _map.Graph.centers)
            {
                CreateRegion(center, planet);

                // Назначение соседей региона
                
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
            region.Collider.points = center.NoisyPoints.ToArray();
            planet.Regions.Add(region);
        }

        private void AssignNeighbors(PlanetaryRegion region, Center center, Planet planet)
        {
            foreach (var index in center.neighborsIndexes)
            {
                // Теперь мы уверены, что все регионы были созданы, и индекс будет валиден
                region.Neighbours.Add(planet.Regions[index]);
            }
        }
        
        private void OnDrawGizmos()
        {
            if (_map != null && _map.Graph.centers != null && planet != null && planet.Regions != null)
            {
                Gizmos.color = Color.yellow;
                for (int i = 0; i < planet.Regions[SelectedRegion].Neighbours.Count; i++)
                {
                    Gizmos.DrawSphere(planet.Regions[SelectedRegion].Neighbours[i].Center, 0.3f);
                
                }

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(planet.Regions[SelectedRegion].Center, 0.4f);
            }
            
            if (_map != null && _map.Graph.edges != null) 
            {
                Gizmos.color = Color.white;
                for (int i = 0; i < _map.Graph.edges.Count; i++) 
                {
                    if (_map.Graph.edges[i].v0 != null && _map.Graph.edges[i].v1 != null)
                    {
                        Vector2 left = _map.Graph.edges[i].v0.point;
                        Vector2 right = _map.Graph.edges[i].v1.point;
                    
                        Gizmos.DrawLine (left, right);
                    }

                }
            }

            if (_map != null && _map.Graph.centers != null)
            {
                Gizmos.color = Color.blue;
                foreach (var center in _map.Graph.centers)
                {
                    foreach (var vector2s in center.NoisyEdgePoints.Values)
                    {
                        foreach (var vector2 in vector2s)
                        {
                            Gizmos.DrawSphere(vector2, 0.4f);
                        }
                    }
                }
            }

            /*if (_map != null && _map.Graph.corners != null)
            {
                Gizmos.color = Color.magenta;
                foreach (var corner in _map.Graph.corners)
                {
                    Gizmos.DrawSphere(corner.point, 0.6f);
                }
            }*/
            
            if (_map != null && _map.Graph.centers != null)
            {
                Gizmos.color = Color.magenta;
                foreach (var corner in _map.Graph.centers[0].corners)
                {
                    Gizmos.DrawSphere(corner.point, 0.6f);
                }
            }

            /*if (_map != null && _map.Graph.centers != null)
            {
                Gizmos.color = Color.yellow;
                foreach (var graphCenter in _map.Graph.centers)
                {
                    foreach (var graphCenterNewEdge in graphCenter.newEdges)
                    {
                        Gizmos.DrawSphere(graphCenterNewEdge.p0.position, 0.8f);
                        Gizmos.DrawSphere(graphCenterNewEdge.p1.position, 0.8f);
                        
                    }
                }
            }*/
            
            
        }

    }
}