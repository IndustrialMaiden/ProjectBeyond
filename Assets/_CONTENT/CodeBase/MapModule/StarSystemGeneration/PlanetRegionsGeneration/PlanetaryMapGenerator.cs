using System;
using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystem.PlanetaryMap;
using _CONTENT.CodeBase.StaticData;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration
{
    public class PlanetaryMapGenerator
    {
        private WorldGenSettings _genSettings;

        private RegionsGenerator _regionsGenerator;

        public PlanetaryMapGenerator(WorldGenSettings genSettings)
        {
            _genSettings = genSettings;
        }


        public List<PlanetaryMapData> GeneratePlanetaryMaps(int seed)
        {
            List<PlanetaryMapData> planetaryMapGenData = new List<PlanetaryMapData>();
            
            for (int i = 0; i < _genSettings.PlanetsCount; i++)
            {
                RandomGenerator random = new RandomGenerator(seed + i);

                _regionsGenerator = new RegionsGenerator(_genSettings.PlanetRegionsCount, random, _genSettings.RegionsMapWidth,
                    _genSettings.RegionsMapHeight, _genSettings.PointSpacing, _genSettings.NoiseScale, _genSettings.NoiseResolution);

                List<RegionData> _regionsData = new List<RegionData>();

                foreach (var center in _regionsGenerator.Graph.centers)
                {
                    // Генерацию фракции отсюда нужно убрать
                    var faction = (Faction) random.Range(0, 5);
                    RegionData regionData = new RegionData(center.index, center.point, center.neighborsIndexes, faction,
                        center.noisyPoints.ToArray(), LoadFactionMaterial(faction));
                    _regionsData.Add(regionData);
                }

                PlanetaryMapData planetaryMapData = new PlanetaryMapData(i, _regionsData);
                planetaryMapGenData.Add(planetaryMapData);
            }

            return planetaryMapGenData;
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