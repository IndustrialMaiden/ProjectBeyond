using System;
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

        public static Material GetMaterial(StarSystemGenerationParams genParams)
        {
            // Randomize Material
            int materialVariantNumber = LucidRandom.Range(0, 2);

            Material planetMat;

            switch (materialVariantNumber)
            {
                case 0:
                    planetMat = new Material(genParams.PlanetMaterialType1);
                    break;
                case 1:
                    planetMat = new Material(genParams.PlanetMaterialType2);
                    break;
                default:
                    planetMat = new Material(genParams.PlanetMaterialType1);
                    break;
            }

            // Randomize Gradient
            int gradientNumber = LucidRandom.Range(0, genParams.PlanetGradients.GetCount());

            GradientColorKey[] gradient = genParams.PlanetGradients.GetItem(gradientNumber).GetGradient();

            for (int i = 0; i < 8; i++)
            {
                planetMat.SetColor(Color + (i + 1), gradient[i].color);
                planetMat.SetFloat(Location + (i + 1), gradient[i].time);
            }
            
            // Randomize Planet Type
            switch (materialVariantNumber)
            {
                case 0:
                    PlanetMatOneVariants variant1 = (PlanetMatOneVariants) LucidRandom.Range(0, Enum.GetNames(typeof(PlanetMatOneVariants)).Length);
                    planetMat.EnableKeyword(PlanetType + variant1);
                    break;
                case 1:
                    PlanetMatTwoVariants variant2 = (PlanetMatTwoVariants) LucidRandom.Range(0, Enum.GetNames(typeof(PlanetMatTwoVariants)).Length);
                    planetMat.EnableKeyword(PlanetType + variant2);
                    break;
            }
            
            // Randomize Surface Offset
            Vector3 surfaceOffset = new Vector3(
                LucidRandom.Range(-10000, 10000), 
                LucidRandom.Range(-10000, 10000),
                LucidRandom.Range(-10000, 10000));
            
            planetMat.SetVector(SurfaceOffset, surfaceOffset);
            
            // Randomize Clouds
            
            CloudsDirection direction = (CloudsDirection) LucidRandom.Range(0, Enum.GetNames(typeof(CloudsDirection)).Length);
            
            switch (direction)
            {
                case StarSystemGeneration.CloudsDirection.LEFT:
                    planetMat.EnableKeyword(CloudsDirection);
                    break;
                case StarSystemGeneration.CloudsDirection.RIGHT:
                    planetMat.DisableKeyword(CloudsDirection);
                    break;
            }
            
            float cloudsSpeed = LucidRandom.Range(0.05f, 0.2f);
            planetMat.SetFloat(CloudsSpeed, cloudsSpeed);
            
            return planetMat;
        }
    }

    public enum PlanetMatOneVariants
    {
        BIGCIRCLES,
        SMALLCIRCLES,
        MOSAIC,
        HEXISLANDS,
        SIMPLECIRCULAR,
        SIMPLE
    }
    
    public enum PlanetMatTwoVariants
    {
        ISLANDSPOS,
        ISLANDSNEG,
        SPIRAL,
        HEXAGON,
        TRACERY,
        STRIPES,
        WAVES
    }

    public enum CloudsDirection
    {
        LEFT,
        RIGHT
    }
}