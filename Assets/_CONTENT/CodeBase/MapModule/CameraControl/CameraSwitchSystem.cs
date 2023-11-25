using System;
using _CONTENT.CodeBase.MapModule.StarSystem;
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

        //ВЫРЕЗАТЬ
        private Color _mainCameraColor;
        

        [Inject]
        public void Construct(MapSceneData mapSceneData)
        {
            _mapSceneData = mapSceneData;
        }
        
        //ВЫРЕЗАТЬ
        private void Start()
        {
            _mainCameraColor = Camera.main.backgroundColor;
        }

        public bool IsPlanetaryCameraActive { get; private set; }
        private PlanetNear _activePlanet;

        public void ActivatePlanetaryCamera(int planetFarIndex)
        {
            if (IsPlanetaryCameraActive) return;

            _activePlanet = _mapSceneData.GetPlanetNear(planetFarIndex);
            
            //Изменение цвета для демо - потом вырезать
            Camera.main.backgroundColor =
                _mapSceneData.GetPlanetFar(planetFarIndex).GetComponent<MeshRenderer>().material.color;
            
            _activePlanet.Activate();
            _planetaryCamera.m_Priority = 15;
            IsPlanetaryCameraActive = true;
        }
        
        public void ActivateStarSystemCamera()
        {
            if (!IsPlanetaryCameraActive) return;
            //ЦВЕТ КАМЕРЫ ВЫРЕЗАТЬ
            Camera.main.backgroundColor = _mainCameraColor;
            
            _planetaryCamera.m_Priority = 5;

            _activePlanet.Deactivate();
            _activePlanet = null;

            IsPlanetaryCameraActive = false;
        }
    }
}