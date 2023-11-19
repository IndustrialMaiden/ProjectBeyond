using System;
using System.Collections.Generic;
using System.Linq;
using AnnulusGames.LucidTools.RandomKit;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

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
        
        private List<int> _sectorsIndexes = new List<int>();

        private void Start()
        {
            Random random = new Random(); 
            
            LucidRandom.InitState(random.Next(0, Int32.MaxValue));
            
            CreateStarSystem();
        }

        public void CreateStarSystem()
        {
            var starSystem = new GameObject().AddComponent<StarSystem>(); // Наверное нужно поменять на префаб
            starSystem.name = "StarSystem";
            
            var systemCenter = Instantiate(_systemCenterPrefab, Vector3.zero, Quaternion.identity);
            
            starSystem.SetSystemCenter(systemCenter);
            
            InitializeSectors();

            float totalDistance = _systemCenterPrefab.transform.localScale.x / 1.5f;

            for (int i = 0; i < _planetsCount; i++)
            {
                float size = LucidRandom.Range(_possibleSize.x, _possibleSize.y);
                float distance = LucidRandom.Range(_possibleDistance.x, _possibleDistance.y) + size / 2f;
                DirectionType directionType = (DirectionType) LucidRandom.Range(0, 2);

                totalDistance += distance + size / 2f;

                PlanetOutside planet = Instantiate(_planetPrefab, RandomizePlanetPosition(totalDistance, i),
                    Quaternion.identity);
                planet.Construct(i, size, totalDistance, directionType, systemCenter.transform);
                
                starSystem.AddPlanet(planet);
            }
            
            starSystem.AssignNeighbours();

        }

        private void InitializeSectors()
        {
            for (int i = 0; i < _planetsCount; i++)
            {
                _sectorsIndexes.Add(i);
            }

            var array = LucidRandom.Shuffle(_sectorsIndexes).ToArray();
            _sectorsIndexes = array.ToList();
        }
        
        private Vector3 RandomizePlanetPosition(float distance, int planetIndex)
        {
            float sectorSize = 2 * Mathf.PI / _planetsCount;
            float sectorStart = sectorSize * _sectorsIndexes[planetIndex];
            float randomAngleInSector = sectorStart + LucidRandom.Range(0, sectorSize);

            return new Vector3(distance * Mathf.Cos(randomAngleInSector), distance * Mathf.Sin(randomAngleInSector), 0);
        }

    }
}