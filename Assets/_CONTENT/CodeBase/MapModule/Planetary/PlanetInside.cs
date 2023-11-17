using System;
using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.Planetary
{
    public class PlanetInside : MonoBehaviour
    {
        public int PlanetIndex;
        public List<Region> Regions;

        public PlanetInside()
        {
            Regions = new List<Region>();
        }
    }
}