using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "Gradients Collection", menuName = "Static Data/Gradients Collection", order = 2)]
    public class GradientsCollection : ScriptableObject
    {
        [field: SerializeField] private List<PlanetGradient> Collection { get; set; }

        public PlanetGradient GetItem(int index)
        {
            return Collection[index];
        }

        public int GetCount()
        {
            return Collection.Count;
        }
    }
}