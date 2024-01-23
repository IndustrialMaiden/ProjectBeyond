using System;
using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using _CONTENT.CodeBase.MapModule.StarSystemFactory;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using Random = System.Random;

namespace _CONTENT.CodeBase.Infrastructure.StateControl.States
{
    public class MapGenerationState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly IMapFactory _mapFactory;
        private readonly StarSystemGenerator _starSystemGenerator;
        private readonly PlanetaryMapGenerator _planetaryMapGenerator;

        private List<PlanetData> _planetGenData;
        private List<PlanetaryMapData> _planetaMapGenData;

        public MapGenerationState(StateMachine stateMachine, IMapFactory mapFactory,
            StarSystemGenerator starSystemGenerator, PlanetaryMapGenerator planetaryMapGenerator)
        {
            _stateMachine = stateMachine;
            _mapFactory = mapFactory;
            _starSystemGenerator = starSystemGenerator;
            _planetaryMapGenerator = planetaryMapGenerator;
        }

        public void Enter()
        {
            GenerateStarSystem(isNewGame: true);
            CreatePlanets();
            CreatePlanetaryMaps();
            OnMapGenerated();
        }

        private void GenerateStarSystem(bool isNewGame)
        {
            StarSystemRoot starSystem = _mapFactory.CreateStarSystem();
            _mapFactory.CreateSystemCenter();
            
            if (isNewGame)
            {
                Random random = new Random();
                var seed = random.Next(0, Int32.MaxValue);
                _planetGenData = _starSystemGenerator.GeneratePlanets(seed);
                _planetaMapGenData = _planetaryMapGenerator.GeneratePlanetaryMaps(seed);
            }
            
            starSystem.AssignNeighbours();
        }

        private void CreatePlanets()
        {
            foreach (var planetData in _planetGenData)
            {
                _mapFactory.CreatePlanet(planetData);
            }
        }

        private void CreatePlanetaryMaps()
        {
            foreach (var planetaryMapData in _planetaMapGenData)
            {
                _mapFactory.CreatePlanetaryMap(planetaryMapData);
            }
        }

        private void OnMapGenerated()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }
    }
}