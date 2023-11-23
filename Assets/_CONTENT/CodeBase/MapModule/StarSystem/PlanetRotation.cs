using _CONTENT.CodeBase.MapModule.StarSystem.PlanetFarObjects;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    [RequireComponent(typeof(PlanetFar))]
    public class PlanetRotation : MonoBehaviour
    {
        private Vector3 _gravityCenterPosition;

        private float _speed;
        private float _speedScale;
        private float _distance;
        private DirectionType _directionType;

        public float HorizontalAxis => _horizontalAxis;
        private float _horizontalAxis;
        public float VerticalAxis => _verticalAxis;
        private float _verticalAxis;

        private float _angle;

        private void Start()
        {
            PlanetFar planet = GetComponent<PlanetFar>();
            
            _gravityCenterPosition = planet.GravityCenter.position;
            _distance = planet.Distance;
            _directionType = planet.DirectionType;
            _speedScale = planet.MovingSpeedScale;
            
            _speed = CalculateOrbitalSpeed();
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

        private float CalculateOrbitalSpeed()
        {
            if (_distance <= 0) return 0;
            
            _horizontalAxis = _distance;
            _verticalAxis = _distance / 2;
            
            float speedCoefficient = 5f;

            var direction = _directionType == DirectionType.Clockwise ? -1 : 1;
            
            return speedCoefficient / _distance * direction * _speedScale;
        }
    }
}