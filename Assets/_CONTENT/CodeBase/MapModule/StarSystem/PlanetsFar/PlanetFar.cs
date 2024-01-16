using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.MouseInteraction;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar
{
    public class PlanetFar : MonoBehaviour, IClickable
    {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private MeshRenderer _meshRenderer;
        public int Index { get; private set; }
        public List<PlanetFar> Neighbours => _neighbours;
        [SerializeField] private List<PlanetFar> _neighbours = new List<PlanetFar>();

        private float _size;
        private float _distance;

        public float Distance => _distance;
        
        private float _movingSpeedScale;
        public float MovingSpeedScale => _movingSpeedScale;
        
        private DirectionType _directionType;
        public DirectionType DirectionType => _directionType;
        
        private Transform _gravityCenter;
        public Transform GravityCenter => _gravityCenter;

        public void Construct(int index, float size, float movingSpeedScale, DirectionType directionType, Transform systemCenter, Material planetMaterial)
        {
            Index = index;
            _size = size;
            _distance = Vector2.Distance(systemCenter.position, transform.position);
            _movingSpeedScale = movingSpeedScale;
            _directionType = directionType;
            _gravityCenter = systemCenter;
            
            transform.localScale = new Vector3(_size, _size, 1);

            gameObject.name = $"Planet {index}";

            GetComponent<MeshFilter>().mesh = MeshGenerator.CreateCircleMesh();
            _meshRenderer.material = planetMaterial;

            GetComponent<PlanetRotation>().enabled = true;
        }

        public void AssignNeighbours(PlanetFar planet)
        {
            _neighbours.Add(planet);
        }

        public void OnLeftClick()
        {
            Debug.Log($"Left click on Planet {Index}");
        }

        public void OnRightClick()
        {
            Debug.Log($"Right click on Planet {Index}");
        }
    }
}