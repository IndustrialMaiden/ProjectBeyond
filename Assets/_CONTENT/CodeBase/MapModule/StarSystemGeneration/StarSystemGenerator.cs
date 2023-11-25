using System;
using System.Collections.Generic;
using System.Linq;
using _CONTENT.CodeBase.Infrastructure.Factory;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetFarObjects;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    public class StarSystemGenerator : IInitializable
    {
        private StarSystemGenerationParams _genParams;
        private IMapFactory _mapFactory;
        private PlanetNearGenerator _planetNearGen;
        private MapSceneData _mapSceneData;
        
        private List<int> _sectorsIndexes = new List<int>();

        public StarSystemGenerator(StarSystemGenerationParams genParams, IMapFactory mapFactory, PlanetNearGenerator planetNearGen, MapSceneData mapSceneData)
        {
            _genParams = genParams;
            _mapFactory = mapFactory;
            _planetNearGen = planetNearGen;
            _mapSceneData = mapSceneData;
        }

        public void Initialize()
        {
            //Это надо поубирать
            
            Random random = new Random();

            var seed = random.Next(0, Int32.MaxValue);
            
            PlayerPrefs.SetInt("SEED", seed);
            
            LucidRandom.InitState(seed);
            
            GenerateStarSystem();
        }

        public void GenerateStarSystem()
        {
            StarSystemComponent starSystemComponent = _mapFactory.CreateStarSystem();
            SystemCenter systemCenter = _mapFactory.CreateSystemCenter();
            
            starSystemComponent.SetSystemCenter(systemCenter);
            
            InitializeSectors();


            CreatePlanets(systemCenter, starSystemComponent);

            starSystemComponent.AssignNeighbours();
            
            starSystemComponent.transform.position = _genParams.StarSystemCoordinates;


        }

        private void CreatePlanets(SystemCenter systemCenter, StarSystemComponent starSystemComponent)
        {
            float totalDistance = systemCenter.transform.localScale.x;

            for (int i = 0; i < _genParams.PlanetsCount; i++)
            {
                float size = LucidRandom.Range(_genParams.PossiblePlanetSize.x, _genParams.PossiblePlanetSize.y);
                float distance = LucidRandom.Range(_genParams.PossiblePlanetDistance.x, _genParams.PossiblePlanetDistance.y);
                DirectionType directionType = (DirectionType) LucidRandom.Range(0, 2);

                totalDistance += distance + size;

                PlanetFar planet = _mapFactory.CreatePlanetFar(RandomizePlanetPosition(totalDistance, i));


                planet.Construct(i, size, _genParams.MovingSpeedScale, directionType, systemCenter.transform);
                
                _mapSceneData.AddPlanetFar(planet);
                starSystemComponent.AddPlanet(planet);

                _mapFactory.CreatePlanetOrbit(planet.Distance).gameObject.name = $"Orbit {i}";

                _planetNearGen.GenerateNearPlanet(i);
            }
        }

        private void InitializeSectors()
        {
            for (int i = 0; i < _genParams.PlanetsCount; i++)
            {
                _sectorsIndexes.Add(i);
            }

            var array = LucidRandom.Shuffle(_sectorsIndexes).ToArray();
            _sectorsIndexes = array.ToList();
        }

        private Vector3 RandomizePlanetPosition(float distance, int planetIndex)
        {
            float sectorSize = 2 * Mathf.PI / _genParams.PlanetsCount;
            float sectorStart = sectorSize * _sectorsIndexes[planetIndex];
            float randomAngleInSector = sectorStart + LucidRandom.Range(0, sectorSize);

            return new Vector3(distance * Mathf.Cos(randomAngleInSector), distance * Mathf.Sin(randomAngleInSector), 0);
        }
    }
}