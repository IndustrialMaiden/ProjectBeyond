using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.Services;
using _CONTENT.CodeBase.Infrastructure.Services.Progress;
using _CONTENT.CodeBase.MapModule.StarSystem;
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
        PlanetNear CreatePlanetNear();
        Region CreatePlanetRegion(Transform parent);

        void CleanUp();
    }
}