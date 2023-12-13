using _CONTENT.CodeBase.Infrastructure.Factory;
using _CONTENT.CodeBase.Infrastructure.MouseInteraction;
using _CONTENT.CodeBase.MapModule;
using _CONTENT.CodeBase.MapModule.CameraControl;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using _CONTENT.CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.ZenjectInstallers
{
    public class MapSceneInstaller : MonoInstaller
    {
        [SerializeField] private StarSystemGenerationParams _starSystemGenerationParams;
        [SerializeField] private MapSceneData _mapSceneData;
        [SerializeField] private CameraSwitchSystem _cameraSwitchSystem;
        
        public override void InstallBindings()
        {
            BindGenerationParameters();
            BindGenerators();
            BindMapFactory();
            BindMouseEventSystem();
            BindMouseEventHandler();
            BindMapSceneData();
            BindCameraSwitchSystem();
        }

        private void BindGenerationParameters()
        {
            Container
                .BindInstance(_starSystemGenerationParams)
                .AsSingle()
                .NonLazy();
        }

        private void BindGenerators()
        {
            Container.BindInterfacesAndSelfTo<PlanetNearGenerator>().AsTransient();
            Container.BindInterfacesAndSelfTo<StarSystemGenerator>().AsSingle().NonLazy();
        }

        private void BindMapFactory()
        {
            Container
                .Bind<IMapFactory>()
                .To<MapFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMouseEventSystem()
        {
            Container
                .BindInterfacesAndSelfTo<MouseEventSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMouseEventHandler()
        {
            Container
                .Bind<MouseEventHandler>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMapSceneData()
        {
            Container
                .Bind<MapSceneData>()
                .FromInstance(_mapSceneData)
                .AsSingle();
        }

        private void BindCameraSwitchSystem()
        {
            Container
                .Bind<CameraSwitchSystem>()
                .FromInstance(_cameraSwitchSystem)
                .AsSingle()
                .NonLazy();
        }
    }
}