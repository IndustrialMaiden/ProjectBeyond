using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.Unity_delaunay.Delaunay;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration
{
    public class Map
    {
        private const int LloydRelaxationsNumber = 5;
        
        public Graph.Graph Graph { get; }

        public Map(int regionsCount, float width, float height, float pointSpacing, float noiseScale, float noiseResolution)
        {
            var points = new List<Vector2>();

            for (int i = 0; i < regionsCount; i++)
            {
                points.Add(new Vector2(
                        Random.Range(0, width),
                        Random.Range(0, height))
                );
            }

            for (int i = 0; i < LloydRelaxationsNumber; i++)
                points = PlanetRegionsGeneration.Graph.Graph.RelaxPoints(points, width, height).ToList();

            var voronoi = new Voronoi(points, null, new Rect(0, 0, width, height));
            
            Graph = new Graph.Graph(points, voronoi, (int)width, (int)height, pointSpacing, noiseScale, noiseResolution);


        }
    }
}
