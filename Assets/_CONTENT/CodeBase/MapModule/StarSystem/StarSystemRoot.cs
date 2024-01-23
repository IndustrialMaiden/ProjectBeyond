using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class StarSystemRoot : MonoBehaviour
    {
        private SystemCenter _systemCenter;
        private List<PlanetView> _planets = new List<PlanetView>();
        
        public void SetSystemCenter(SystemCenter systemCenter)
        {
            _systemCenter = systemCenter;
        }

        public void AddPlanet(PlanetView planetView)
        {
            _planets.Add(planetView);
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