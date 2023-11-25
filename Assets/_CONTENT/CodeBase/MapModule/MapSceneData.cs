using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetFarObjects;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule
{
    public class MapSceneData : MonoBehaviour
    {
        private Dictionary<int, PlanetFar> PlanetsFar = new Dictionary<int, PlanetFar>();
        private Dictionary<int, PlanetNear> PlanetsNear = new Dictionary<int, PlanetNear>();

        public void AddPlanetFar(PlanetFar planetFar)
        {
            PlanetsFar.Add(planetFar.Index, planetFar);
        }
        
        public void AddPlanetNear(PlanetNear planetNear)
        {
            PlanetsNear.Add(planetNear.Index, planetNear);
        }

        public PlanetFar GetPlanetFar(int planetFarIndex)
        {
            return PlanetsFar.FirstOrDefault(p => p.Key == planetFarIndex).Value;
        }

        public PlanetNear GetPlanetNear(int planetNearIndex)
        {
            return PlanetsNear.FirstOrDefault(p => p.Key == planetNearIndex).Value;
        }
    }
}