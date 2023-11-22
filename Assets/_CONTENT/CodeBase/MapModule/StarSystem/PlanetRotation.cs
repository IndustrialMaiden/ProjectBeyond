using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    [RequireComponent(typeof(PlanetFar))]
    public class PlanetRotation : MonoBehaviour
    {
        private Vector3 _gravityCenterPosition;

        private float _speed;
        private float _speedScale;
        private DirectionType _directionType;
        
        private float _horizontalAxis;
        private float _verticalAxis;
        
        private float _angle;

        private void Start()
        {
            PlanetFar planet = GetComponent<PlanetFar>();
            
            _gravityCenterPosition = planet.GravityCenter.position;
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
            var distance = Vector3.Distance(_gravityCenterPosition, transform.position);
            
            if (distance <= 0) return 0;
            
            _horizontalAxis = distance;
            _verticalAxis = distance / 2;
            
            float speedCoefficient = 5f;

            var direction = _directionType == DirectionType.Clockwise ? -1 : 1;
            
            return speedCoefficient / distance * direction * _speedScale;
        }
    }
}