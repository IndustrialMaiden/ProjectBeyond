using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace _CONTENT.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "Planet Gradient", menuName = "Static Data/Planet Gradient", order = 1)]
    public class PlanetGradient : ScriptableObject
    {
        [field: SerializeField] public Gradient OriginalGradient { get; private set; }
        [field: SerializeField, Space] public Gradient CreatedGradient { get; private set; }

        public GradientColorKey[] GetGradient()
        {
            return CreatedGradient.colorKeys;
        }

        [ContextMenu("Create Gradient")]
        private void CreateGradient()
        {
            if (OriginalGradient.colorKeys.Length != 4)
                throw new ArgumentException("Colors in Original Gradient != 4", name);

            List<GradientColorKey> colorList = new List<GradientColorKey>();

            float time = 0.25f;
            
            foreach (var gradientColorKey in OriginalGradient.colorKeys)
            {
                colorList.Add(new GradientColorKey(gradientColorKey.color, time));
                time += 0.25f;
            }
            
            Color darkestColor = new Color(colorList[0].color.r / 2f, colorList[0].color.g / 2f, colorList[0].color.b / 2f, 1);
            
            colorList.Insert(0, new GradientColorKey(darkestColor, 0));

            GradientColorKey[] newColors = new GradientColorKey[4];
            
            for (int i = 0; i < 4; i++)
            {
                var LerpColor = Vector4.Lerp(colorList[i].color, colorList[i + 1].color, 0.5f);
                var LerpTime = (colorList[i].time + colorList[i + 1].time) / 2;

                newColors[i] = new GradientColorKey(LerpColor, LerpTime);
            }

            colorList.Insert(1, newColors[0]);
            colorList.Insert(3, newColors[1]);
            colorList.Insert(5, newColors[2]);
            colorList.Insert(7, newColors[3]);
            colorList.RemoveAt(0);

            CreatedGradient.mode = GradientMode.Fixed;

            CreatedGradient.colorKeys = colorList.ToArray();
            
            if (CreatedGradient.colorKeys.Length != 8)
                throw new ArgumentException("Colors in Created Gradient != 8", name);
        }
        
    }
}