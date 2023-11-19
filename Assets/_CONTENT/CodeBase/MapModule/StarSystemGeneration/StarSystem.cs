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

        public void AssignNeighbours()
        {
            for (int i = 0; i < _planets.Count; i++)
            {
                if (i > 0)
                {
                    _planets[i].AssignNeighbours(_planets[i - 1]);
                }
                
                if (i + 1 < _planets.Count)
                {
                    _planets[i].AssignNeighbours(_planets[i + 1]);
                }
                
            }
        }
    }
}