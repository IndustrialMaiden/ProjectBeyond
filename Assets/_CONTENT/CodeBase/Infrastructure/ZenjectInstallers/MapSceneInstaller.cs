using _CONTENT.CodeBase.Infrastructure.MouseInteraction;
using _CONTENT.CodeBase.Infrastructure.SceneControl;
using _CONTENT.CodeBase.MapModule;
using _CONTENT.CodeBase.MapModule.CameraControl;
using _CONTENT.CodeBase.MapModule.StarSystemFactory;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using _CONTENT.CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.ZenjectInstallers
{
    public class MapSceneInstaller : MonoInstaller
    {
        [SerializeField] private WorldGenSettings worldGenSettings;
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
            
            // Это нужно исправить
            ProjectContext.Instance.Container.Resolve<SceneLoader>().UpdateSceneContainer(Container);
        }

        private void BindGenerationParameters()
        {
            Container
                .BindInstance(worldGenSettings)
                .AsSingle();
        }

        private void BindGenerators()
        {
            Container.BindInterfacesAndSelfTo<PlanetaryMapGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<StarSystemGenerator>().AsSingle();
        }

        private void BindMapFactory()
        {
            Container
                .Bind<IMapFactory>()
                .To<MapFactory>()
                .AsSingle();
        }

        private void BindMouseEventSystem()
        {
            Container
                .BindInterfacesAndSelfTo<MouseEventSystem>()
                .AsSingle();
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
                .AsSingle();
        }
    }
}