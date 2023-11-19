using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration.Graph;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration.NoiseGenerator
{
    public class NoisyEdgesGenerator
    {
        private float _pointSpacing;
        private float _noiseScale;
        private float _noiseResolution;
        private Vector2 _noiseOffset;

        public NoisyEdgesGenerator(RandomGenerator random, float pointSpacing, float noiseScale, float noiseResolution)
        {
            _pointSpacing = pointSpacing;
            _noiseScale = noiseScale;
            _noiseResolution = noiseResolution;
            _noiseOffset = new Vector2(random.value * 1000, random.value * 1000);
        }
        
        public Dictionary<NewEdge, List<Vector2>> GenerateNoisyEdges(List<NewEdge> edges)
        {
            Dictionary<NewEdge, List<Vector2>> noisyEdgePoints = new Dictionary<NewEdge, List<Vector2>>();

            foreach (var edge in edges)
            {
                Vector2 start = edge.p0.position;
                Vector2 end = edge.p1.position;

                float segmentLength = (end - start).magnitude;
                int numPoints = Mathf.FloorToInt(segmentLength / _pointSpacing) - 1;

                List<Vector2> edgeNoisyPoints = new List<Vector2> { start };

                for (int j = 1; j <= numPoints; j++)
                {
                    float t = (float)j / (numPoints + 1);
                    Vector2 pointOnEdge = Vector2.Lerp(start, end, t);

                    // Модифицированная кривая веса
                    float weight = Mathf.Sin(t * Mathf.PI);
                    //float weight = 1 - Mathf.Pow((2 * t - 1), 2);

                    // Генерация шума с учетом весового коэффициента
                    Vector2 noise = PerlinNoiseAtPoint(pointOnEdge, _noiseResolution, 4, 0.3f) * weight;
                    Vector2 noisyPoint = pointOnEdge + noise * _noiseScale;

                    edgeNoisyPoints.Add(noisyPoint);
                }

                edgeNoisyPoints.Add(end);
                noisyEdgePoints[edge] = edgeNoisyPoints;
            }

            return noisyEdgePoints;
        }



        /*public Dictionary<NewEdge, List<Vector2>> GenerateNoisyEdges(List<NewEdge> edges)
        {
            Dictionary<NewEdge, List<Vector2>> noisyEdgePoints = new Dictionary<NewEdge, List<Vector2>>();

            foreach (var edge in edges)
            {
                Vector2 start = edge.p0.position;
                Vector2 end = edge.p1.position;

                Vector2 direction = (end - start).normalized;
                float segmentLength = (end - start).magnitude;
                int numPoints = Mathf.FloorToInt(segmentLength / _pointSpacing) - 1;

                List<Vector2> edgeNoisyPoints = new List<Vector2> {start};

                for (int j = 1; j <= numPoints; j++)
                {
                    float t = (float) j / (numPoints + 1);
                    Vector2 pointOnEdge = Vector2.Lerp(start, end, t);

                    // Квадратичная кривая для веса, с максимумом в середине и уменьшением к концам
                    float weight = 1 - Mathf.Pow((2 * t - 1), 2);

                    // Генерация шума с учетом весового коэффициента
                    Vector2 noise = PerlinNoiseAtPoint(pointOnEdge, _noiseResolution, 4, 0.5f) * weight;
                    Vector2 noisyPoint = pointOnEdge + noise * _noiseScale;

                    edgeNoisyPoints.Add(noisyPoint);
                }

                edgeNoisyPoints.Add(end);
                noisyEdgePoints[edge] = edgeNoisyPoints;
            }

            return noisyEdgePoints;
        }*/
        
        private Vector2 PerlinNoiseAtPoint(Vector2 point, float resolution, int octaves, float persistence)
        {
            Vector2 noiseResult = Vector2.zero;
            float maxAmplitude = 0f;
            float amplitude = 1f;
            float frequency = resolution;

            for (int i = 0; i < octaves; i++)
            {
                // Применяем смещение к координатам для генерации шума
                noiseResult.x += (Mathf.PerlinNoise((point.x + _noiseOffset.x) * frequency, (point.y + _noiseOffset.y) * frequency) - 0.5f) * amplitude;
                noiseResult.y += (Mathf.PerlinNoise((point.y + _noiseOffset.y) * frequency, (point.x + _noiseOffset.x) * frequency) - 0.5f) * amplitude;

                maxAmplitude += amplitude;
                amplitude *= persistence;
                frequency *= 2f;
            }

            // Нормализуем шум, чтобы он оставался в пределах [-1, 1]
            noiseResult /= maxAmplitude;

            return noiseResult;
        }
    }
}