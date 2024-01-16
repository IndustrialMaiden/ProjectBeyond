using System.Collections;
using _CONTENT.CodeBase.Infrastructure.CoroutineRunner;
using _CONTENT.CodeBase.Infrastructure.SceneControl;
using _CONTENT.CodeBase.MapModule;
using _CONTENT.CodeBase.MapModule.CameraControl;
using _CONTENT.CodeBase.MapModule.StarSystem;
using UnityEngine;

namespace _CONTENT.CodeBase.Infrastructure.StateControl.States
{
    public class PlanetViewState : IPayloadedState<int>
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly CameraSwitchSystem _cameraSwitchSystem;
        private readonly MapSceneData _mapSceneData;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly int _planetIndex;

        public PlanetViewState(StateMachine stateMachine, SceneLoader sceneLoader, CameraSwitchSystem cameraSwitchSystem, MapSceneData mapSceneData, ICoroutineRunner coroutineRunner, int planetIndex)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _cameraSwitchSystem = cameraSwitchSystem;
            _mapSceneData = mapSceneData;
            _coroutineRunner = coroutineRunner;
            _planetIndex = planetIndex;
        }

        public void Enter(int planetIndex)
        {
            _coroutineRunner.StartCoroutine(EnterState());
        }

        private IEnumerator EnterState()
        {
            _cameraSwitchSystem.ActivatePlanetaryCamera(_planetIndex);
            _mapSceneData.SetActivePlanetNear(_planetIndex);
            
            yield return new WaitForSeconds(0.5f);
            
            DisablePlanetsRenderer();
            _mapSceneData.GetPlanetNear(_planetIndex).Activate();
            _cameraSwitchSystem.SetInteractionCamera(MapCamera.Planetary);
        }

        public void Exit()
        {
            _coroutineRunner.StopAllCoroutines();
            PlanetNear planet = _mapSceneData.GetPlanetNear(_planetIndex);
            if (planet.gameObject.activeSelf) planet.Deactivate();
            _mapSceneData.SetActivePlanetNear(-1);
            EnablePlanetsRenderer();
        }

        private void EnablePlanetsRenderer()
        {
            foreach (var planetFar in _mapSceneData.PlanetsFar)
            {
                if (planetFar.Key == _planetIndex) continue;
                planetFar.Value.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        private void DisablePlanetsRenderer()
        {
            foreach (var planetFar in _mapSceneData.PlanetsFar)
            {
                if (planetFar.Key == _planetIndex) continue;
                planetFar.Value.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}