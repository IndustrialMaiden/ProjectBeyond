using System;
using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.Planetary
{
    public class Planet : MonoBehaviour
    {
        public int PlanetIndex;
        public List<RegionData> Regions;

        public Planet()
        {
            Regions = new List<RegionData>();
        }
    }
}