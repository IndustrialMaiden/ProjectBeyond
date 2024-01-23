using _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    [RequireComponent(typeof(PlanetView))]
    public class PlanetRotation : MonoBehaviour
    {
        private PlanetView _planetView;

        private Vector3 _gravityCenterPosition;

        private float _speed;
        private float _speedScale;
        private float _distance;
        private PlanetMoveDirection _planetMoveDirection;

        public float HorizontalAxis => _horizontalAxis;
        private float _horizontalAxis;
        public float VerticalAxis => _verticalAxis;
        private float _verticalAxis;

        private float _angle;


        public void Construct(PlanetView planetView)
        {
            _planetView = planetView;
        }

        private void Start()
        {
            _gravityCenterPosition = _planetView.PlanetData.GravityCenterPosition;
            _distance = _planetView.PlanetData.Distance;
            _planetMoveDirection = _planetView.PlanetData.PlanetMoveDirection;
            _speedScale = _planetView.PlanetData.SpeedScale;

            _speed = CalculateOrbitalSpeed();
            _angle = Mathf.Atan2(transform.position.y - _gravityCenterPosition.y,
                transform.position.x - _gravityCenterPosition.x);
        }

        private void Update()
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
            float distanceFactor = Mathf.Pow(_distance, 1.5f);

            var direction = _planetMoveDirection == PlanetMoveDirection.Clockwise ? -1 : 1;

            return speedCoefficient / distanceFactor * _speedScale * direction;
        }
    }
}