using System.Collections.Generic;
using AnnulusGames.LucidTools.RandomKit;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class PlanetFar : MonoBehaviour
    {
        public int Index { get; private set; }
        public List<PlanetFar> Neighbours => _neighbours;
        [SerializeField] private List<PlanetFar> _neighbours = new List<PlanetFar>();

        private float _size;
        
        private float _movingSpeedScale;
        public float MovingSpeedScale => _movingSpeedScale;
        
        private DirectionType _directionType;
        public DirectionType DirectionType => _directionType;
        
        private Transform _gravityCenter;
        public Transform GravityCenter => _gravityCenter;
        

        //PlanetType

        public void Construct(int index, float size, float movingSpeedScale, DirectionType directionType, Transform gravityCenter)
        {
            Index = index;
            _size = size;
            _movingSpeedScale = movingSpeedScale;
            _directionType = directionType;
            _gravityCenter = gravityCenter;
            
            transform.localScale = new Vector3(_size, _size, 1);
            
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = LucidRandom.ColorHSV();

            GetComponent<PlanetRotation>().enabled = true;
        }

        public void AssignNeighbours(PlanetFar planet)
        {
            _neighbours.Add(planet);
        }
    }
}