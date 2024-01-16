using System;
using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule
{
    public class MapSceneData : MonoBehaviour
    {
        public Dictionary<int, PlanetFar> PlanetsFar = new Dictionary<int, PlanetFar>();
        public Dictionary<int, PlanetNear> PlanetsNear = new Dictionary<int, PlanetNear>();

        public int ActivePlanetNearIndex { get; private set; } = -1;

        public void AddPlanetFar(PlanetFar planetFar) =>
            PlanetsFar.Add(planetFar.Index, planetFar);
        
        public void AddPlanetNear(PlanetNear planetNear) =>
            PlanetsNear.Add(planetNear.Index, planetNear);

        public PlanetFar GetPlanetFar(int planetFarIndex) =>
            PlanetsFar.FirstOrDefault(p => p.Key == planetFarIndex).Value;

        public PlanetNear GetPlanetNear(int planetNearIndex) => 
            PlanetsNear.FirstOrDefault(p => p.Key == planetNearIndex).Value;

        public void SetActivePlanetNear(int planetNearIndex) => ActivePlanetNearIndex = planetNearIndex;
    }
}