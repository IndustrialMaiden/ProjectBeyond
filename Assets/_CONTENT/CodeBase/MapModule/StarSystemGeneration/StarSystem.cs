using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    public class StarSystem : MonoBehaviour
    {
        private GameObject _systemCenter;
        private List<PlanetOutside> _planets = new List<PlanetOutside>();

        public void SetSystemCenter(GameObject systemCenter)
        {
            _systemCenter = systemCenter;
        }

        public void AddPlanet(PlanetOutside planet)
        {
            _planets.Add(planet);
        }
    }
}