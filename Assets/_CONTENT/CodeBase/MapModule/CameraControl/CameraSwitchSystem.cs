using System;
using _CONTENT.CodeBase.MapModule.StarSystem;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration;
using _CONTENT.CodeBase.StaticData;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace _CONTENT.CodeBase.MapModule.CameraControl
{
    public class CameraSwitchSystem : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _starSystemCamera;
        [SerializeField] private CinemachineVirtualCamera _planetaryCamera;

        private MapSceneData _mapSceneData;
        private StarSystemGenerationParams _genParams;
        

        [Inject]
        public void Construct(MapSceneData mapSceneData, StarSystemGenerationParams genParams)
        {
            _mapSceneData = mapSceneData;
            _genParams = genParams;
        }

        private void Start()
        {
            _starSystemCamera.transform.position = new Vector3(_genParams.StarSystemCenter.x,
                _genParams.StarSystemCenter.y, _starSystemCamera.transform.position.z);
            
            _planetaryCamera.transform.position = new Vector3(_genParams.PlanetaryRegionsCenter.x,
                _genParams.PlanetaryRegionsCenter.y, _planetaryCamera.transform.position.z);
        }

        public bool IsPlanetaryCameraActive { get; private set; }
        private PlanetNear _activePlanet;

        public void ActivatePlanetaryCamera(int planetFarIndex)
        {
            if (IsPlanetaryCameraActive) return;

            _activePlanet = _mapSceneData.GetPlanetNear(planetFarIndex);

            _activePlanet.Activate();
            _planetaryCamera.m_Priority = 15;
            IsPlanetaryCameraActive = true;
        }
        
        public void ActivateStarSystemCamera()
        {
            if (!IsPlanetaryCameraActive) return;

            _planetaryCamera.m_Priority = 5;

            _activePlanet.Deactivate();
            _activePlanet = null;

            IsPlanetaryCameraActive = false;
        }
    }
}