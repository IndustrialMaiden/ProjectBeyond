using System;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    public class PlanetRotation : MonoBehaviour
    {
        private Transform _gravityCenter;
        [SerializeField] private float _speed;
        private Vector3 axis;

        private void Start()
        {
            _gravityCenter = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity).transform;
            _speed = CalculateOrbitalSpeed(_gravityCenter.position);
            axis = new Vector3(0, 0, 1);
        }

        private float CalculateOrbitalSpeed(Vector3 gravityCenterPosition)
        {
            var distance = Vector3.Distance(gravityCenterPosition, transform.position);
            
            if (distance <= 0) return 0;
            
            float speedCoefficient = 200f;
            return speedCoefficient / distance;
        }

        private void Update()
        {
            transform.RotateAround(_gravityCenter.position, axis, _speed * Time.deltaTime);
        }
    }
}