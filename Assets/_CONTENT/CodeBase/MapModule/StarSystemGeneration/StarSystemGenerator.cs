using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration
{
    public class StarSystemGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private PlanetOutside _planetPrefab;
        [SerializeField] private GameObject _systemCenterPrefab;
        
        [Space, Header("Generation Options")]
        [SerializeField] private int _planetsCount;
        [SerializeField, Range(0, 5f)] private int _movingSpeedScale;

        [Space]
        [SerializeField] private Vector2 _possibleSize;
        [SerializeField] private Vector2 _possibleDistance;
        [SerializeField] private Vector2 _possibleMovingSpeed;

        private List<PlanetOutside> _planets = new List<PlanetOutside>();

        private void Start()
        {
            System.Random random = new System.Random(); 
            
            Random.InitState(random.Next(0, Int32.MaxValue));
            
            CreateStarSystem();
        }

        public void CreateStarSystem()
        {
            var systemCenter = Instantiate(_systemCenterPrefab, Vector3.zero, Quaternion.identity);
            var starSystem = Instantiate(new GameObject().AddComponent<StarSystem>(), Vector3.zero, Quaternion.identity);
            starSystem.SetSystemCenter(systemCenter);

            float totalDistance = _systemCenterPrefab.transform.localScale.x / 1.5f;

            for (int i = 0; i < _planetsCount; i++)
            {
                float size = Random.Range(_possibleSize.x, _possibleSize.y);
                float distance = Random.Range(_possibleDistance.x, _possibleDistance.y) + size / 2f;
                float movingSpeed = Random.Range(_possibleMovingSpeed.x, _possibleMovingSpeed.y);
                DirectionType directionType = (DirectionType) Random.Range(0, 2);

                totalDistance += distance + size / 2f;

                PlanetOutside planet = Instantiate(_planetPrefab, RandomizePlatenPosition(totalDistance),
                    Quaternion.identity);
                planet.Construct(i, size, totalDistance, movingSpeed, directionType, systemCenter.transform);
                
                _planets.Add(planet);
                starSystem.AddPlanet(planet);
            }

            for (int i = 0; i < _planets.Count; i++)
            {
                if (i > 0)
                {
                    _planets[i].AssignNeighbours(_planets[i - 1]);
                }
                
                if (i + 1 < _planets.Count)
                {
                    _planets[i].AssignNeighbours(_planets[i + 1]);
                }
                
            }
            
        }

        private Vector3 RandomizePlatenPosition(float distance)
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            return new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);
        }

    }
}