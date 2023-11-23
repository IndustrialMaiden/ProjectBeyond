using _CONTENT.CodeBase.Infrastructure.Factory;
using _CONTENT.CodeBase.Infrastructure.MouseInteraction;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using UnityEngine;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.ZenjectInstallers
{
    public class MapSceneInstaller : MonoInstaller
    {
        [SerializeField] private StarSystemGenerationParams starSystemGenerationParams;
        
        public override void InstallBindings()
        {
            BindGenerationParameters();
            BindGenerators();
            BindMapFactory();
            BindMouseEventSystem();
            BindMouseEventHandler();
        }

        private void BindGenerators()
        {
            Container.BindInterfacesAndSelfTo<PlanetNearGenerator>().AsTransient();
            Container.BindInterfacesAndSelfTo<StarSystemGenerator>().AsSingle().NonLazy();
        }

        private void BindGenerationParameters()
        {
            Container
                .BindInstance(starSystemGenerationParams)
                .AsSingle()
                .NonLazy();
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
            Container.BindInterfacesAndSelfTo<MouseEventSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMouseEventHandler()
        {
            Container.Bind<MouseEventHandler>().AsSingle().NonLazy();
        }
    }
}