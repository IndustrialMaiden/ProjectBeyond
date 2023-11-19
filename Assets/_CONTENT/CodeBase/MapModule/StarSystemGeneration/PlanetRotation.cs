using System;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    [RequireComponent(typeof(PlanetOutside))]
    public class PlanetRotation : MonoBehaviour
    {
        private Vector3 _gravityCenterPosition;

        private float _speed;
        private DirectionType _directionType;
        
        private float _horizontalAxis;
        private float _verticalAxis;
        
        private float _angle;

        private void Start()
        {
            PlanetOutside planet = GetComponent<PlanetOutside>();
            
            _gravityCenterPosition = planet.GravityCenter.position;
            _directionType = planet.DirectionType;
            
            _speed = CalculateOrbitalSpeed(_gravityCenterPosition);
            _angle = Mathf.Atan2(transform.position.y - _gravityCenterPosition.y, transform.position.x - _gravityCenterPosition.x);
        }

        void FixedUpdate()
        {
            MovePlanet();
        }

        private void MovePlanet()
        {
            _angle += _speed * Time.deltaTime;

            float x = Mathf.Cos(_angle) * _horizontalAxis + _gravityCenterPosition.x;
            float y = Mathf.Sin(_angle) * _verticalAxis + _gravityCenterPosition.y;

            transform.position = new Vector3(x, y, transform.position.z);
        }

        private float CalculateOrbitalSpeed(Vector3 gravityCenterPosition)
        {
            var distance = Vector3.Distance(gravityCenterPosition, transform.position);
            
            if (distance <= 0) return 0;
            
            _horizontalAxis = distance;
            _verticalAxis = distance / 2;
            
            float speedCoefficient = 5f;

            var direction = _directionType == DirectionType.Clockwise ? -1 : 1;
            
            return speedCoefficient / distance * direction;
        }
    }
}