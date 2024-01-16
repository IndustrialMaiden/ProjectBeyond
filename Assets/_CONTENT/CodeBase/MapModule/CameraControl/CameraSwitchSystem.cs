using System;
using _CONTENT.CodeBase.StaticData;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace _CONTENT.CodeBase.MapModule.CameraControl
{
    public class CameraSwitchSystem : MonoBehaviour
    {
        
        [Header("Physical Cameras")]
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _regionCamera;
        
        [Space][Header("Virtual Cameras")]
        [SerializeField] private CinemachineVirtualCamera _starSystemCamera;
        [SerializeField] private CinemachineVirtualCamera _planetaryCamera;

        public Camera InteractionCamera { get; private set; }

        private MapSceneData _mapSceneData;
        private StarSystemGenerationParams _genParams;
        

        [Inject]
        public void Construct(MapSceneData mapSceneData, StarSystemGenerationParams genParams)
        {
            InteractionCamera = _mainCamera;
            _mapSceneData = mapSceneData;
            _genParams = genParams;
        }

        private void Start()
        {
            _starSystemCamera.transform.position = new Vector3(_genParams.StarSystemCenter.x,
                _genParams.StarSystemCenter.y, _starSystemCamera.transform.position.z);
        }

        public bool IsPlanetaryCameraActive { get; private set; }

        public void ActivatePlanetaryCamera(int planetIndex)
        {
            if (IsPlanetaryCameraActive) return;
            _planetaryCamera.Follow = _mapSceneData.GetPlanetFar(planetIndex).transform;
            _planetaryCamera.m_Priority = 15;
            IsPlanetaryCameraActive = true;
        }
        
        public void ActivateStarSystemCamera()
        {
            if (!IsPlanetaryCameraActive) return;
            _planetaryCamera.m_Priority = 5;
            IsPlanetaryCameraActive = false;
        }

        public void SetInteractionCamera(MapCamera cameraType)
        {
            switch (cameraType)
            {
                case MapCamera.StarSystem:
                    InteractionCamera = _mainCamera;
                    break;
                case MapCamera.Planetary:
                    InteractionCamera = _regionCamera;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cameraType), cameraType, "Wrong camera type");
            }
        }
    }

    public enum MapCamera
    {
        StarSystem,
        Planetary
    }
}