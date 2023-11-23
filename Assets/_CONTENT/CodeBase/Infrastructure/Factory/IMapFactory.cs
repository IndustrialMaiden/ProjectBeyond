using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.Services;
using _CONTENT.CodeBase.Infrastructure.Services.Progress;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetFarObjects;
using UnityEngine;

namespace _CONTENT.CodeBase.Infrastructure.Factory
{
    public interface IMapFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        StarSystemComponent CreateStarSystem();
        SystemCenter CreateSystemCenter();
        PlanetFar CreatePlanetFar(Vector3 at);
        PlanetOrbitDrawer CreatePlanetOrbit(float distance);
        PlanetNear CreatePlanetNear();
        Region CreatePlanetRegion(Transform parent);

        void CleanUp();
    }
}