using System.Collections.Generic;
using AnnulusGames.LucidTools.RandomKit;
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
        private DirectionType _directionType;
        public DirectionType DirectionType => _directionType;
        private Transform _gravityCenter;
        public Transform GravityCenter => _gravityCenter;
        

        //PlanetType

        public void Construct(int index, float size, float distance, DirectionType directionType, Transform gravityCenter)
        {
            Index = index;
            _size = size;
            _distance = distance;
            _directionType = directionType;
            _gravityCenter = gravityCenter;

            ApplyPlanetView();

            GetComponent<PlanetRotation>().enabled = true;
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
            spriteRenderer.color = LucidRandom.ColorHSV();
        }
    }
}