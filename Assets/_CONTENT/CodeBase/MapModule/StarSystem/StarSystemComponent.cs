using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class StarSystemComponent : MonoBehaviour
    {
        private SystemCenter _systemCenter;
        private List<PlanetFar> _planets = new List<PlanetFar>();
        
        public void SetSystemCenter(SystemCenter systemCenter)
        {
            _systemCenter = systemCenter;
        }

        public void AddPlanet(PlanetFar planet)
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