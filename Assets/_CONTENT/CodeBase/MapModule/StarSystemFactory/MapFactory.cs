using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.Services.Progress;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using _CONTENT.CodeBase.MapModule.StarSystem.Regions;
using _CONTENT.CodeBase.StaticData;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _CONTENT.CodeBase.MapModule.StarSystemFactory
{
    public class MapFactory : IMapFactory
    {
        private DiContainer _sceneContainer;

        private StarSystemGenerationParams _genParams;

        private StarSystemComponent _starSystemComponent;
        private SystemCenter _systemCenter;
        
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public MapFactory(DiContainer sceneContainer, StarSystemGenerationParams genParams)
        {
            _sceneContainer = sceneContainer;
            _genParams = genParams;
        }

        public StarSystemComponent CreateStarSystem()
        {
            _starSystemComponent = _sceneContainer.InstantiatePrefabForComponent<StarSystemComponent>
                (_genParams.StarSystemComponentPrefab, Vector3.zero, Quaternion.identity, null);
            RegisterProgressWatchers(_starSystemComponent.gameObject);
            return _starSystemComponent;
        }

        public SystemCenter CreateSystemCenter()
        {
            _systemCenter = _sceneContainer.InstantiatePrefabForComponent<SystemCenter>
            (_genParams.SystemCenterPrefab, Vector3.zero, Quaternion.identity, _starSystemComponent.gameObject.transform);
            RegisterProgressWatchers(_systemCenter.gameObject);
            return _systemCenter;
        }

        public PlanetFar CreatePlanetFar(Vector3 at)
        {
            var planetFar = _sceneContainer.InstantiatePrefabForComponent<PlanetFar>
            (_genParams.PlanetFarPrefab, at, Quaternion.identity, _starSystemComponent.gameObject.transform);
            RegisterProgressWatchers(planetFar.gameObject);
            return planetFar;
        }

        public PlanetOrbitDrawer CreatePlanetOrbit(float distance)
        {
            var planetOrbitDrawer = Object.Instantiate
            (_genParams.OrbitPrefab, Vector3.zero, Quaternion.identity, _starSystemComponent.transform);
            planetOrbitDrawer.Construct(distance);
            return planetOrbitDrawer;
        }

        public PlanetNear CreatePlanetNear()
        {
            var planetNear = _sceneContainer.InstantiatePrefabForComponent<PlanetNear>(_genParams.PlanetNearPrefab,
                Vector3.zero, Quaternion.identity, null);
            RegisterProgressWatchers(planetNear.gameObject);
            return planetNear;
        }

        public Region CreatePlanetRegion(Transform parent)
        {
            var region = _sceneContainer.InstantiatePrefabForComponent<Region>(_genParams.PlanetRegionPrefab,
                Vector3.zero, Quaternion.identity, parent);
            RegisterProgressWatchers(region.gameObject);
            return region;
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