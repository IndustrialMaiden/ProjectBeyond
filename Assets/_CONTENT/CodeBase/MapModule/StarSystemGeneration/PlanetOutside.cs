using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    public class PlanetOutside : MonoBehaviour
    {
        public int Index { get; private set; }
        public List<PlanetOutside> Neighbours => _neighbours;
        [SerializeField] private List<PlanetOutside> _neighbours = new List<PlanetOutside>();

        private float _size;
        private float _distance;
        private float _movingSpeed;
        private DirectionType _directionType;
        private Transform _gravityCenter;
        

        //PlanetType
        //Mass

        public void Construct(int index, float size, float distance, float movingSpeed, DirectionType directionType, Transform gravityCenter)
        {
            Index = index;
            _size = size;
            _distance = distance;
            _movingSpeed = movingSpeed;
            _directionType = directionType;
            _gravityCenter = gravityCenter;

            ApplyPlanetView();
        }

        public void AssignNeighbours(PlanetOutside planet)
        {
            _neighbours.Add(planet);
        }

        private void ApplyPlanetView()
        {
            transform.localScale = new Vector3(_size, _size, 1);
            RandomizeColor();
        }

        private void RandomizeColor()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);
            spriteRenderer.color = new Color(r, g, b);
        }
    }
}