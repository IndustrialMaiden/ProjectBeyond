using System;
using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using _CONTENT.CodeBase.MapModule.StarSystemFactory;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using _CONTENT.CodeBase.StaticData;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    public class StarSystemGenerator
    {
        private StarSystemGenerationParams _genParams;
        private IMapFactory _mapFactory;
        private PlanetNearGenerator _planetNearGen;
        private MapSceneData _mapSceneData;

        private RandomGenerator _starSystemRandom;
        
        private List<int> _sectorsIndexes = new List<int>();

        public StarSystemGenerator(StarSystemGenerationParams genParams, IMapFactory mapFactory, PlanetNearGenerator planetNearGen, MapSceneData mapSceneData)
        {
            _genParams = genParams;
            _mapFactory = mapFactory;
            _planetNearGen = planetNearGen;
            _mapSceneData = mapSceneData;
        }

        public void GenerateStarSystem(int seed, Action onGenerated)
        {
            _starSystemRandom = new RandomGenerator(seed);
            
            StarSystemComponent starSystemComponent = _mapFactory.CreateStarSystem();
            SystemCenter systemCenter = _mapFactory.CreateSystemCenter();
            starSystemComponent.SetSystemCenter(systemCenter);
            
            InitializeSectorsForPlanetsPosition();

            CreatePlanets(systemCenter, starSystemComponent);

            starSystemComponent.AssignNeighbours();
            
            starSystemComponent.transform.position = _genParams.StarSystemCenter;
            
            onGenerated?.Invoke();
        }

        private void CreatePlanets(SystemCenter systemCenter, StarSystemComponent starSystemComponent)
        {
            float totalDistance = systemCenter.transform.localScale.x;

            for (int i = 0; i < _genParams.PlanetsCount; i++)
            {
                float size = _starSystemRandom.Range(_genParams.PossiblePlanetSize.x, _genParams.PossiblePlanetSize.y);
                float distance = _starSystemRandom.Range(_genParams.PossiblePlanetDistance.x, _genParams.PossiblePlanetDistance.y);
                DirectionType directionType = (DirectionType) _starSystemRandom.Range(0, 2);

                totalDistance += distance + size;

                PlanetFar planet = _mapFactory.CreatePlanetFar(RandomizePlanetPosition(totalDistance, i));

                Material planetMaterial = PlanetMatRandomizer.GetMaterial(_genParams);

                planet.Construct(i, size, _genParams.MovingSpeedScale, directionType, systemCenter.transform, planetMaterial);
                
                _mapSceneData.AddPlanetFar(planet);
                starSystemComponent.AddPlanet(planet);

                _mapFactory.CreatePlanetOrbit(planet.Distance).gameObject.name = $"Orbit {i}";

                _planetNearGen.GenerateNearPlanet(i);
            }
        }

        private void InitializeSectorsForPlanetsPosition()
        {
            for (int i = 0; i < _genParams.PlanetsCount; i++)
            {
                _sectorsIndexes.Add(i);
            }

            var array = _starSystemRandom.Shuffle(_sectorsIndexes).ToArray();
            _sectorsIndexes = array.ToList();
        }

        private Vector3 RandomizePlanetPosition(float distance, int planetIndex)
        {
            float sectorSize = 2 * Mathf.PI / _genParams.PlanetsCount;
            float sectorStart = sectorSize * _sectorsIndexes[planetIndex];
            float randomAngleInSector = sectorStart + _starSystemRandom.Range(0, sectorSize);

            return new Vector3(distance * Mathf.Cos(randomAngleInSector), distance * Mathf.Sin(randomAngleInSector), 0);
        }
    }
}