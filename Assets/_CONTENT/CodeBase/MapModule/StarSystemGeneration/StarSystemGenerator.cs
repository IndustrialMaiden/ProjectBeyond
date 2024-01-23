using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using _CONTENT.CodeBase.StaticData;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    public class StarSystemGenerator
    {
        private readonly WorldGenSettings _genSettings;
        private RandomGenerator _starSystemRandom;

        private List<int> _sectorsIndexes;
        private List<int> _planetPatternsKeys;
        private List<int> _planetGradientKeys;
        private float _sectorSize;

        public StarSystemGenerator(WorldGenSettings genSettings)
        {
            _genSettings = genSettings;
            
            _sectorsIndexes = Enumerable.Range(0, _genSettings.PlanetsCount).ToList();
            _planetPatternsKeys = Enumerable.Range(0, PlanetMatRandomizer.PlanetMatPatterns.Count).ToList();
            _planetGradientKeys = Enumerable.Range(0, _genSettings.PlanetGradients.GetCount()).ToList();
            _sectorSize = 2 * Mathf.PI / _genSettings.PlanetsCount;
        }

        public List<PlanetData> GeneratePlanets(int seed)
        {
            _starSystemRandom = new RandomGenerator(seed);

            _sectorsIndexes = ShuffleList(_sectorsIndexes);
            _planetPatternsKeys = ShuffleList(_planetPatternsKeys);
            _planetGradientKeys = ShuffleList(_planetGradientKeys);

            List<PlanetData> planetGenData = new List<PlanetData>(_genSettings.PlanetsCount);
            
            float totalDistance = _genSettings.SystemCenterPrefab.transform.localScale.x;

            for (int i = 0; i < _genSettings.PlanetsCount; i++)
            {
                float size = _starSystemRandom.Range(_genSettings.PossiblePlanetSize.x, _genSettings.PossiblePlanetSize.y);
                float distance = _starSystemRandom.Range(_genSettings.PossiblePlanetDistance.x, _genSettings.PossiblePlanetDistance.y);

                totalDistance += distance + size;
                Vector2 startPosition = RandomizePlanetPosition(totalDistance, i);

                Material planetMaterial = PlanetMatRandomizer.GetMaterial(_genSettings, _starSystemRandom, _planetPatternsKeys[i], _planetGradientKeys[i]);

                PlanetMoveDirection planetMoveDirection = (PlanetMoveDirection)_starSystemRandom.Range(0, 2);
                
                PlanetData planetData = new PlanetData(i, startPosition, size, totalDistance, _genSettings.MovingSpeedScale,
                    planetMoveDirection, _genSettings.StarSystemCenter, planetMaterial);

                planetGenData.Add(planetData);
            }

            return planetGenData;
        }

        private List<T> ShuffleList<T>(List<T> list)
        {
            return _starSystemRandom.Shuffle(list).ToList();
        }

        private Vector3 RandomizePlanetPosition(float distance, int planetIndex)
        {
            float sectorStart = _sectorSize * _sectorsIndexes[planetIndex];
            float randomAngleInSector = sectorStart + _starSystemRandom.Range(0, _sectorSize);

            float x = distance * Mathf.Cos(randomAngleInSector);
            float y = distance / 2 * Mathf.Sin(randomAngleInSector);

            return new Vector3(x, y, 0);
        }
    }
}
