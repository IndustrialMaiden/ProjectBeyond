using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.Regions
{
    public class RegionActivation : MonoBehaviour
    {
        private Dictionary<Material, float> originalAlphas = new Dictionary<Material, float>();
        private List<Material> materials = new List<Material>();
    
        private float showDuration = 0.3f;  // Продолжительность анимации появления
        private float hideDuration = 0.01f;  // Продолжительность анимации исчезновения

        public Action Showed;
        public Action Hided;

        void Awake()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    materials.Add(material);
                    originalAlphas[material] = material.color.a;
                    SetMaterialAlpha(material, 0.0f);
                }
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(FadeToOriginalAlpha());
        }

        public void Hide()
        {
            StopAllCoroutines();
            StartCoroutine(FadeToZero());
        }

        private IEnumerator FadeToOriginalAlpha()
        {
            float elapsedTime = 0;
            while (elapsedTime < showDuration)
            {
                foreach (var material in materials)
                {
                    float targetAlpha = originalAlphas[material];
                    float newAlpha = Mathf.Lerp(0, targetAlpha, elapsedTime / showDuration);
                    SetMaterialAlpha(material, newAlpha);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            foreach (var material in materials)
            {
                SetMaterialAlpha(material, originalAlphas[material]);
            }
            Showed?.Invoke();
        }

        private IEnumerator FadeToZero()
        {
            float targetAlpha = 0;
            float elapsedTime = 0;
        
            while (elapsedTime < hideDuration)
            {
                foreach (var material in materials)
                {
                    float startAlpha = originalAlphas[material];
                    float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / hideDuration);
                    SetMaterialAlpha(material, newAlpha);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            foreach (var material in materials)
            {
                SetMaterialAlpha(material, targetAlpha);
            }
            Hided?.Invoke();
            gameObject.SetActive(false);
        }

        private void SetMaterialAlpha(Material material, float alpha)
        {
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }
}
