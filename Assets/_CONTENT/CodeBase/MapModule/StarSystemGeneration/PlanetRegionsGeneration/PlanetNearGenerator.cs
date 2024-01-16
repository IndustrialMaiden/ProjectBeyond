using System;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.Regions;
using _CONTENT.CodeBase.MapModule.StarSystemFactory;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration.GraphObjects;
using _CONTENT.CodeBase.StaticData;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration
{
    public class PlanetNearGenerator
    {
        private StarSystemGenerationParams _genParams;
        private IMapFactory _mapFactory;
        private MapSceneData _mapSceneData;
        
        
        private PlanetaryMap _planetaryMap;

        public PlanetNearGenerator(StarSystemGenerationParams genParams, IMapFactory mapFactory, MapSceneData mapSceneData)
        {
            _genParams = genParams;
            _mapFactory = mapFactory;
            _mapSceneData = mapSceneData;
        }


        public void GenerateNearPlanet(int planetIndex)
        {
            RandomGenerator random = new RandomGenerator(PlayerPrefs.GetInt("SEED") + planetIndex);
            
            CreatePlanet(random, planetIndex);
        }

        private void CreatePlanet(RandomGenerator random, int planetIndex)
        {
            _planetaryMap = new PlanetaryMap(_genParams.PlanetRegionsCount, random, _genParams.RegionsMapWidth, _genParams.RegionsMapHeight, _genParams.PointSpacing, _genParams.NoiseScale, _genParams.NoiseResolution);

            PlanetNear planetNear = _mapFactory.CreatePlanetNear();
            planetNear.SetIndex(planetIndex);
            planetNear.gameObject.name = $"Planet Near {planetIndex}";

            foreach (var center in _planetaryMap.Graph.centers)
            {
                Region region = _mapFactory.CreatePlanetRegion(planetNear.transform);
                // Генерацию фракции отсюда нужно убрать
                var faction = (Faction) random.Range(0, 5);
                region.Construct(center, faction, LoadFactionMaterial(faction));
                planetNear.AddRegion(region);
            }
            
            foreach (var region in planetNear.GetRegions())
            {
                AssignNeighbors(region, _planetaryMap.Graph.centers[region.Index], planetNear);
            }
            
            _mapSceneData.AddPlanetNear(planetNear);
        }

        private void AssignNeighbors(Region region, Center center, PlanetNear planetNear)
        {
            foreach (var index in center.neighborsIndexes)
            {
                region.AddNeighbour(planetNear.GetRegions()[index]);
            }
        }
        
        // Asset Provider?
        private Material LoadFactionMaterial(Faction faction)
        {
            switch (faction)
            {
                case Faction.Insects:
                    return Resources.Load<Material>("Z_DemoMaterials/Insects_Mat");
                case Faction.Demons:
                    return Resources.Load<Material>("Z_DemoMaterials/Demons_Mat");
                case Faction.Mechanoids:
                    return Resources.Load<Material>("Z_DemoMaterials/Mechanoids_Mat");
                case Faction.Mages:
                    return Resources.Load<Material>("Z_DemoMaterials/Mages_Mat");
                case Faction.Necrons:
                    return Resources.Load<Material>("Z_DemoMaterials/Necrons_Mat");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}