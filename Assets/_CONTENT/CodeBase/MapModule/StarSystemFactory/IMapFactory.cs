using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.Services;
using _CONTENT.CodeBase.Infrastructure.Services.Progress;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetaryMap;
using _CONTENT.CodeBase.MapModule.StarSystem.Planets;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemFactory
{
    public interface IMapFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        StarSystemRoot CreateStarSystem();
        SystemCenter CreateSystemCenter();
        void CreatePlanet(PlanetData planetData);
        void CreatePlanetaryMap(PlanetaryMapData planetaryMapData);
        void CleanUp();
    }
}