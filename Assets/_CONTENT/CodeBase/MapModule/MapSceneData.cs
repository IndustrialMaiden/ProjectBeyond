using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetaryMap;
using _CONTENT.CodeBase.MapModule.StarSystem.Planets;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule
{
    public class MapSceneData : MonoBehaviour
    {
        public Dictionary<int, PlanetView> Planets = new Dictionary<int, PlanetView>();
        public Dictionary<int, PlanetaryMapView> PlanetaryMaps = new Dictionary<int, PlanetaryMapView>();

        public int ActivePlanetNearIndex { get; private set; } = -1;

        public void AddPlanet(PlanetView planetView) =>
            Planets.Add(planetView.PlanetData.Index, planetView);
        
        public void AddPlanetaryMap(PlanetaryMapView planetaryMapView) =>
            PlanetaryMaps.Add(planetaryMapView.PlanetaryMapData.Index, planetaryMapView);

        public PlanetView GetPlanet(int planetIndex) =>
            Planets.FirstOrDefault(p => p.Key == planetIndex).Value;

        public PlanetaryMapView GetPlanetaryMap(int planetaryMapIndex) => 
            PlanetaryMaps.FirstOrDefault(p => p.Key == planetaryMapIndex).Value;

        public void SetActivePlanetNear(int planetaryMapIndex) => ActivePlanetNearIndex = planetaryMapIndex;
    }
}