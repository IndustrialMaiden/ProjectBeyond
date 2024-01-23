using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration.GraphObjects;
using Plugins.Unity_delaunay.Delaunay;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration
{
    public class RegionsGenerator
    {
        private const int LloydRelaxationsNumber = 5;
        
        public Graph Graph { get; }

        public RegionsGenerator(int regionsCount, RandomGenerator random, float width, float height, float pointSpacing, float noiseScale, float noiseResolution)
        {
            var points = new List<Vector2>();

            for (int i = 0; i < regionsCount; i++)
            {
                points.Add(new Vector2(
                    random.Range(0, width),
                    random.Range(0, height))
                );
            }

            for (int i = 0; i < LloydRelaxationsNumber; i++)
                points = RelaxPoints(points, width, height).ToList();

            var voronoi = new Voronoi(points, null, new Rect(0, 0, width, height));
            
            Graph = new Graph(points, voronoi, (int)width, (int)height, random, pointSpacing, noiseScale, noiseResolution);


        }
        
        public IEnumerable<Vector2> RelaxPoints(IEnumerable<Vector2> startingPoints, float width, float height)
        {
            Voronoi v = new Voronoi(startingPoints.ToList(), null, new Rect(0, 0, width, height));
            foreach (var point in startingPoints)
            {
                var region = v.Region(point);
                point.Set(0, 0);
                foreach (var r in region)
                    point.Set(point.x + r.x, point.y + r.y);

                point.Set(point.x / region.Count, point.y / region.Count);
                yield return point;
            }
        }
    }
}
