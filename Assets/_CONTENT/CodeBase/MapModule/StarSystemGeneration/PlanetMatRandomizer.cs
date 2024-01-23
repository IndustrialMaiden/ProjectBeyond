using System;
using System.Collections.Generic;
using _CONTENT.CodeBase.StaticData;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    public static class PlanetMatRandomizer
    {
        private const string Color = "_Color";
        private const string Location = "_Location";
        private const string PlanetType = "_PLANETTYPE_";
        private const string SurfaceOffset = "_SurfaceOffset";
        private const string CloudsDirection = "_CLOUDSDIRECTION";
        private const string CloudsSpeed = "_CloudsSpeed";

        public static readonly Dictionary<int, string> PlanetMatPatterns = new Dictionary<int, string>()
        {
            {0, "BIGCIRCLES"},
            {1, "SMALLCIRCLES"},
            {2, "MOSAIC"},
            {3, "HEXISLANDS"},
            {4, "SIMPLECIRCULAR"},
            {5, "SIMPLE"},
            {6, "ISLANDSPOS"},
            {7, "ISLANDSNEG"},
            {8, "SPIRAL"},
            {9, "HEXAGON"},
            {10, "TRACERY"},
            {11, "STRIPES"},
            {12, "WAVES"},
        };

        public static Material GetMaterial(WorldGenSettings genParams, RandomGenerator starSystemRandom, int matNumber, int gradientNumber)
        {
            // Apply Material
            
            Material planetMat;

            switch (matNumber)
            {
                case >= 0 and <= 5:
                    planetMat = new Material(genParams.PlanetMaterialType1);
                    planetMat.EnableKeyword(PlanetType + PlanetMatPatterns[matNumber]);
                    break;
                case >= 6 and <= 12:
                    planetMat = new Material(genParams.PlanetMaterialType2);
                    planetMat.EnableKeyword(PlanetType + PlanetMatPatterns[matNumber]);
                    break;
                default:
                    planetMat = new Material(genParams.PlanetMaterialType1);
                    break;
            }

            // Apply Gradient

            GradientColorKey[] gradient = genParams.PlanetGradients.GetItem(gradientNumber).GetGradient();

            for (int i = 0; i < 8; i++)
            {
                planetMat.SetColor(Color + (i + 1), gradient[i].color);
                planetMat.SetFloat(Location + (i + 1), gradient[i].time);
            }
            
            // Randomize Surface Offset
            Vector3 surfaceOffset = new Vector3(
                starSystemRandom.Range(-10000, 10000), 
                starSystemRandom.Range(-10000, 10000),
                starSystemRandom.Range(-10000, 10000));
            
            planetMat.SetVector(SurfaceOffset, surfaceOffset);
            
            // Randomize Clouds
            
            CloudsDirection direction = (CloudsDirection) starSystemRandom.Range(0, Enum.GetNames(typeof(CloudsDirection)).Length);
            
            switch (direction)
            {
                case StarSystemGeneration.CloudsDirection.LEFT:
                    planetMat.EnableKeyword(CloudsDirection);
                    break;
                case StarSystemGeneration.CloudsDirection.RIGHT:
                    planetMat.DisableKeyword(CloudsDirection);
                    break;
            }
            
            float cloudsSpeed = starSystemRandom.Range(0.05f, 0.2f);
            planetMat.SetFloat(CloudsSpeed, cloudsSpeed);
            
            return planetMat;
        }
    }

    public enum CloudsDirection
    {
        LEFT,
        RIGHT
    }
}