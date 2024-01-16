using System;
using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration.Extensions;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration.NoiseGenerator;
using AnnulusGames.LucidTools.RandomKit;
using Plugins.Unity_delaunay.Delaunay;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration.GraphObjects
{
    public class Graph
    {
        List<KeyValuePair<int, Corner>> _cornerMap = new List<KeyValuePair<int, Corner>>();

        public int Width { get; }
        public int Height { get; }
        public List<Center> centers = new List<Center>();
        public List<Corner> corners = new List<Corner>();
        public List<Edge> edges = new List<Edge>();

        private NoisyEdgesGenerator _noiseGenerator;

        public Graph(IEnumerable<Vector2> points, Voronoi voronoi, int width, int height, RandomGenerator random, float pointSpacing,
            float noiseScale, float noiseResolution)
        {
            Width = width;
            Height = height;

            _noiseGenerator = new NoisyEdgesGenerator(random, pointSpacing, noiseScale, noiseResolution);

            BuildGraph(points, voronoi);

            RemovingWrongNeighbours();

            //SortAndRemapCenterIndexes(centers);

            _noiseGenerator = null;
        }

        private void BuildGraph(IEnumerable<Vector2> points, Voronoi voronoi)
        {
            var libedges = voronoi.Edges();

            var centerLookup = new Dictionary<Vector2?, Center>();

            foreach (var point in points)
            {
                var p = new Center {index = centers.Count, point = point};
                centers.Add(p);
                centerLookup[point] = p;
            }

            foreach (var p in centers)
            {
                voronoi.Region(p.point);
            }

            foreach (var libedge in libedges)
            {
                var dedge = libedge.DelaunayLine();
                var vedge = libedge.VoronoiEdge();

                var edge = new Edge
                {
                    index = edges.Count,

                    v0 = MakeCorner(vedge.p0),
                    v1 = MakeCorner(vedge.p1),
                    d0 = centerLookup[dedge.p0],
                    d1 = centerLookup[dedge.p1]
                };
                if (vedge.p0.HasValue && vedge.p1.HasValue)
                    edge.midpoint = Vector2Extensions.Interpolate(vedge.p0.Value, vedge.p1.Value, 0.5f);

                edges.Add(edge);

                if (edge.d0 != null)
                {
                    edge.d0.borders.Add(edge);
                }

                if (edge.d1 != null)
                {
                    edge.d1.borders.Add(edge);
                }

                if (edge.v0 != null)
                {
                    edge.v0.protrudes.Add(edge);
                }

                if (edge.v1 != null)
                {
                    edge.v1.protrudes.Add(edge);
                }

                // Centers point to centers.
                if (edge.d0 != null && edge.d1 != null)
                {
                    AddToCenterList(edge.d0.neighbors, edge.d1);
                    AddToCenterList(edge.d1.neighbors, edge.d0);
                }

                // Corners point to corners
                if (edge.v0 != null && edge.v1 != null)
                {
                    AddToCornerList(edge.v0.adjacent, edge.v1);
                    AddToCornerList(edge.v1.adjacent, edge.v0);
                }

                // Centers point to corners
                if (edge.d0 != null)
                {
                    AddToCornerList(edge.d0.corners, edge.v0);
                    AddToCornerList(edge.d0.corners, edge.v1);
                }

                if (edge.d1 != null)
                {
                    AddToCornerList(edge.d1.corners, edge.v0);
                    AddToCornerList(edge.d1.corners, edge.v1);
                }

                // Corners point to centers
                if (edge.v0 != null)
                {
                    AddToCenterList(edge.v0.touches, edge.d0);
                    AddToCenterList(edge.v0.touches, edge.d1);
                }

                if (edge.v1 != null)
                {
                    AddToCenterList(edge.v1.touches, edge.d0);
                    AddToCenterList(edge.v1.touches, edge.d1);
                }
            }

            // TODO: use edges to determine these
            var topLeft = centers.OrderBy(p => p.point.x + p.point.y).First();
            AddCorner(topLeft, 0, 0);

            var bottomRight = centers.OrderByDescending(p => p.point.x + p.point.y).First();
            AddCorner(bottomRight, Width, Height);

            var topRight = centers.OrderByDescending(p => Width - p.point.x + p.point.y).First();
            AddCorner(topRight, 0, Height);

            var bottomLeft = centers.OrderByDescending(p => p.point.x + Height - p.point.y).First();
            AddCorner(bottomLeft, Width, 0);

            AdjustCorners(centers, Width, Height, 11f);

            foreach (var center in centers)
            {
                center.corners.Sort(ClockwiseComparisonCorners(center));
                CreateNewEdges(center);
                UpdateEdgeVerticesClockwise(center);

                center.noisyEdgePoints = _noiseGenerator.GenerateNoisyEdges(center.newEdges);
                foreach (var vector2s in center.noisyEdgePoints.Values)
                {
                    center.noisyPoints.AddRange(vector2s);
                }
                
                var uniquePoints = RemoveNoisyPointsDuplicates(center.noisyPoints);
                center.noisyPoints = uniquePoints;
            }
        }

        private Corner MakeCorner(Vector2? nullablePoint)
        {
            if (nullablePoint == null)
                return null;

            var point = nullablePoint.Value;

            for (var i = (int) (point.x - 1); i <= (int) (point.x + 1); i++)
            {
                foreach (var kvp in _cornerMap.Where(p => p.Key == i))
                {
                    var dx = point.x - kvp.Value.point.x;
                    var dy = point.y - kvp.Value.point.y;
                    if (dx * dx + dy * dy < 1e-6)
                        return kvp.Value;
                }
            }

            var corner = new Corner {index = corners.Count, point = point};
            corners.Add(corner);
            corner.border = point.x == 0 || point.x == Width || point.y == 0 || point.y == Height;

            _cornerMap.Add(new KeyValuePair<int, Corner>((int) (point.x), corner));

            return corner;
        }

        private static void AddCorner(Center topLeft, int x, int y)
        {
            if (topLeft.point.x != x || topLeft.point.y != y)
                topLeft.corners.Add(new Corner {point = new Vector2(x, y)});
        }
        
        private void AddToCornerList(List<Corner> v, Corner x)
        {
            if (x != null && v.IndexOf(x) < 0)
                v.Add(x);
        }

        private void AddToCenterList(List<Center> v, Center x)
        {
            if (x != null && v.IndexOf(x) < 0)
            {
                v.Add(x);
            }
        }

        private Comparison<Corner> ClockwiseComparisonCorners(Center center)
        {
            return (cornerA, cornerB) =>
            {
                Vector2 centerPoint = center.point;
                Vector2 pointA = cornerA.point - centerPoint;
                Vector2 pointB = cornerB.point - centerPoint;

                // Рассчитать углы относительно центра
                float angleA = Mathf.Atan2(pointA.y, pointA.x);
                float angleB = Mathf.Atan2(pointB.y, pointB.x);

                // Нормализация углов от 0 до 2 * PI
                angleA = (angleA >= 0) ? angleA : (2 * Mathf.PI + angleA);
                angleB = (angleB >= 0) ? angleB : (2 * Mathf.PI + angleB);

                // Сравнить углы
                return angleA.CompareTo(angleB);
            };
        }

        private void UpdateEdgeVerticesClockwise(Center center)
        {
            // Затем проверяем и корректируем порядок вершин
            for (int i = 0; i < center.newEdges.Count; i++)
            {
                int nextIndex = (i + 1) % center.newEdges.Count;
                var currentEdge = center.newEdges[i];
                var nextEdge = center.newEdges[nextIndex];

                if (currentEdge.p1.position != nextEdge.p0.position)
                {
                    // Если p1 текущего ребра не совпадает с p0 следующего ребра, меняем местами p0 и p1 текущего ребра
                    var temp = currentEdge.p1.position;
                    currentEdge.p1.position = currentEdge.p0.position;
                    currentEdge.p0.position = temp;
                }
            }
        }

        public void AdjustCorners(List<Center> centers, float width, float height, float threshold)
        {
            // Код для определения уникальных углов карты...
            var mapCorners = new List<Vector2>
            {
                new (0, 0),
                new (width, 0),
                new (0, height),
                new (width, height)
            };

            // Поиск и удаление близких углов, а затем добавление углов карты
            foreach (var center in centers)
            {
                List<Corner> cornersToRemove = new List<Corner>();

                foreach (var corner in center.corners)
                {
                    foreach (var mapCorner in mapCorners)
                    {
                        if (Vector2.Distance(corner.point, mapCorner) < threshold)
                        {
                            cornersToRemove.Add(corner);
                            break;
                        }
                    }
                }

                // Здесь удаляем и заменяем углы
                foreach (var cornerToRemove in cornersToRemove)
                {
                    var matchingMapCorner =
                        mapCorners.First(mc => Vector2.Distance(cornerToRemove.point, mc) < threshold);

                    // Удаляем близкий угол во всех центрах и добавляем угол карты
                    foreach (var otherCenter in centers)
                    {
                        var removed = otherCenter.corners.RemoveAll(c =>
                            Vector2.Distance(c.point, cornerToRemove.point) < threshold);
                        if (removed > 0 && !otherCenter.corners.Any(c => c.point == matchingMapCorner))
                        {
                            otherCenter.corners.Add(new Corner {point = matchingMapCorner});
                        }
                    }
                }
            }
        }

        private void CreateNewEdges(Center center)
        {
            int cornerCount = center.corners.Count;

            for (int i = 0; i < cornerCount; i++)
            {
                var newEdge = new NewEdge();
                newEdge.p0.position = center.corners[i].point;

                int nextIndex = (i + 1) % cornerCount; // Получаем индекс следующего угла, с учетом цикличности

                newEdge.p1.position = center.corners[nextIndex].point;
                center.newEdges.Add(newEdge);
            }
        }

        private void RemovingWrongNeighbours()
        {
            for (var i = 0; i < centers.Count; i++)
            {
                var centerCorners = centers[i].corners;
                List<Center> validNeighbors = new List<Center>();
                List<int> neighborsIndexes = new List<int>();

                foreach (var neighbor in centers[i].neighbors)
                {
                    var neighborCorners = neighbor.corners;

                    if (centerCorners.Any(c => neighborCorners.Contains(c)))
                    {
                        validNeighbors.Add(neighbor);
                        neighborsIndexes.Add(neighbor.index);
                    }
                }

                centers[i].neighbors = validNeighbors;
                centers[i].neighborsIndexes = neighborsIndexes;
            }
        }

        public List<Vector2> RemoveNoisyPointsDuplicates(List<Vector2> points)
        {
            if (points == null || points.Count <= 1)
                return points;

            List<Vector2> uniquePoints = new List<Vector2> { points[0] };

            for (int i = 1; i < points.Count; i++)
            {
                var current = points[i];
                var lastUnique = uniquePoints.Last();

                // Сравниваем координаты до трёх знаков после запятой без округления
                if (Math.Truncate(current.x * 1000) != Math.Truncate(lastUnique.x * 1000) ||
                    Math.Truncate(current.y * 1000) != Math.Truncate(lastUnique.y * 1000))
                {
                    uniquePoints.Add(current);
                }
            }

            if (Math.Truncate(uniquePoints[0].x * 1000) == Math.Truncate(uniquePoints[^1].x * 1000) ||
                Math.Truncate(uniquePoints[0].y * 1000) != Math.Truncate(uniquePoints[^1].y * 1000))
            {
                uniquePoints.Remove(uniquePoints[^1]);
            }

            return uniquePoints;
        }


        // Нужен ли?
        public void SortAndRemapCenterIndexes(List<Center> centers)
        {
            const float rangeSize = 40f; // Размер диапазона для Y
            List<Center> sortedCenters = new List<Center>();

            // Определяем максимальное значение Y среди всех центров
            float maxY = centers.Max(c => c.point.y);

            // Проходим через каждый диапазон Y
            for (float currentY = 0; currentY <= maxY; currentY += rangeSize)
            {
                // Фильтруем центры, которые попадают в текущий диапазон
                var currentRangeCenters = centers
                    .Where(c => c.point.y >= currentY && c.point.y < currentY + rangeSize)
                    .OrderBy(c => c.point.x)
                    .ToList();
        
                // Добавляем отсортированные центры текущего диапазона к общему списку
                sortedCenters.AddRange(currentRangeCenters);
            }

            // Обновляем индексы в соответствии с новой сортировкой
            for (int i = 0; i < sortedCenters.Count; i++)
            {
                sortedCenters[i].index = i;
            }

            // Создаем словарь для быстрого поиска новых индексов
            var indexMap = sortedCenters.ToDictionary(c => c.point, c => c.index);

            // Обновляем индексы соседей каждого центра
            foreach (var center in centers)
            {
                foreach (var neighbor in center.neighbors)
                {
                    if (indexMap.TryGetValue(neighbor.point, out var newIndex))
                    {
                        neighbor.index = newIndex;
                    }
                }
            }

            // Копируем отсортированный список обратно в исходный список
            centers.Clear();
            centers.AddRange(sortedCenters);
        }

    }
}