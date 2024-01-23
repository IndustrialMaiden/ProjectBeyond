using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.Services.Progress;
using _CONTENT.CodeBase.Infrastructure.StrategyControl;
using _CONTENT.CodeBase.Infrastructure.StrategyControl.Strategies;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using _CONTENT.CodeBase.MapModule.StarSystem.Regions;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using _CONTENT.CodeBase.StaticData;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _CONTENT.CodeBase.MapModule.StarSystemFactory
{
    public class MapFactory : IMapFactory
    {
        private readonly DiContainer _sceneContainer;
        private readonly IStrategyFactory _strategyFactory;
        private readonly WorldGenSettings _genSettings;
        private readonly MapSceneData _mapSceneData;


        private StarSystemRoot _starSystemRoot;
        private SystemCenter _systemCenter;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public MapFactory(DiContainer sceneContainer, IStrategyFactory strategyFactory, WorldGenSettings genSettings,
            MapSceneData mapSceneData)
        {
            _sceneContainer = sceneContainer;
            _strategyFactory = strategyFactory;
            _genSettings = genSettings;
            _mapSceneData = mapSceneData;
        }

        public StarSystemRoot CreateStarSystem()
        {
            _starSystemRoot = _sceneContainer.InstantiatePrefabForComponent<StarSystemRoot>
                (_genSettings.StarSystemRootPrefab, _genSettings.StarSystemCenter, Quaternion.identity, null);
            RegisterProgressWatchers(_starSystemRoot.gameObject);
            return _starSystemRoot;
        }

        public SystemCenter CreateSystemCenter()
        {
            _systemCenter = _sceneContainer.InstantiatePrefabForComponent<SystemCenter>
            (_genSettings.SystemCenterPrefab, _genSettings.StarSystemCenter, Quaternion.identity,
                _starSystemRoot.gameObject.transform);
            _starSystemRoot.SetSystemCenter(_systemCenter);
            RegisterProgressWatchers(_systemCenter.gameObject);
            return _systemCenter;
        }

        public void CreatePlanet(PlanetData planetData)
        {
            var planet = _sceneContainer.InstantiatePrefabForComponent<PlanetView>
            (_genSettings.PlanetViewPrefab, _genSettings.StarSystemCenter - planetData.StartPosition, Quaternion.identity,
                _starSystemRoot.gameObject.transform);

            planet.Construct(planetData);
            planet.transform.localScale = new Vector3(planetData.Size, planetData.Size, 1);
            planet.gameObject.name = $"Planet {planetData.Index}";
            planet.GetComponent<MeshFilter>().mesh = MeshGenerator.CreateCircleMesh();
            planet.GetComponent<MeshRenderer>().material = planetData.PlanetMaterial;
            planet.SetLeftClickAction(_strategyFactory.Get<ViewPlanetAction>(planetData.Index));


            var planetRotation = planet.GetComponent<PlanetRotation>();
            planetRotation.Construct(planet);
            planetRotation.enabled = true;

            _mapSceneData.AddPlanet(planet);
            _starSystemRoot.AddPlanet(planet);

            RegisterProgressWatchers(planet.gameObject);

            CreatePlanetOrbit(planet.PlanetData.Distance, planet.PlanetData.Index);
        }

        private void CreatePlanetOrbit(float distance, int index)
        {
            var planetOrbitDrawer = Object.Instantiate
                (_genSettings.OrbitPrefab, _genSettings.StarSystemCenter, Quaternion.identity, _starSystemRoot.transform);
            planetOrbitDrawer.Construct(distance);
            planetOrbitDrawer.gameObject.name = $"Orbit {index}";
        }

        public void CreatePlanetaryMap(PlanetaryMapData planetaryMapData)
        {
            PlanetaryMapView planetaryMap = _sceneContainer.InstantiatePrefabForComponent<PlanetaryMapView>(
                _genSettings.PlanetaryMapViewPrefab,
                Vector3.zero, Quaternion.identity, null);
            planetaryMap.gameObject.name = $"Planet Near {planetaryMapData.Index}";

            List<RegionView> regions = new List<RegionView>();

            foreach (var regionData in planetaryMapData.RegionsData)
            {
                RegionView region = CreatePlanetRegions(regionData, planetaryMap.transform);
                regions.Add(region);
            }

            foreach (var regionView in regions)
            {
                regionView.Neighbours = AssignRegionNeighbors(regions, regionView.RegionData.NeighborsIndexes);
            }

            planetaryMap.Construct(planetaryMapData, regions);

            RegisterProgressWatchers(planetaryMap.gameObject);

            _mapSceneData.AddPlanetaryMap(planetaryMap);
        }

        private RegionView CreatePlanetRegions(RegionData regionData, Transform planetaryMap)
        {
            RegionView region = _sceneContainer.InstantiatePrefabForComponent<RegionView>(
                _genSettings.PlanetRegionViewPrefab,
                Vector3.zero, Quaternion.identity, planetaryMap);
            region.gameObject.name = $"Region {regionData.Index}";
            region.GetComponent<PolygonCollider2D>().points = regionData.V2ColliderPositions;
            region.GetComponent<MeshFilter>().mesh = MeshGenerator.CreatePolygonMesh(regionData.V2ColliderPositions);
            region.GetComponent<MeshRenderer>().material = regionData.RegionMaterial;

            region.Construct(regionData, CreateBorder(regionData.V2ColliderPositions));

            RegisterProgressWatchers(region.gameObject);
            return region;
        }

        private List<RegionView> AssignRegionNeighbors(List<RegionView> regions, List<int> neighboursIndexes)
        {
            List<RegionView> neighbours = new List<RegionView>();

            foreach (var index in neighboursIndexes)
            {
                neighbours.Add(regions[index]);
            }

            return neighbours;
        }

        private Vector3[] CreateBorder(Vector2[] v2ColliderPositions)
        {
            Vector3[] v3ColliderPositions = new Vector3[v2ColliderPositions.Length];

            for (int i = 0; i < v2ColliderPositions.Length; i++)
            {
                v3ColliderPositions[i] = v2ColliderPositions[i];
            }

            return v3ColliderPositions;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponents<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}