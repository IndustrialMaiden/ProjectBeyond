using _CONTENT.CodeBase.Infrastructure.CoroutineRunnerDir;
using _CONTENT.CodeBase.Infrastructure.SceneControl;
using _CONTENT.CodeBase.Infrastructure.StateControl;
using _CONTENT.CodeBase.Infrastructure.StrategyControl;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.ZenjectInstallers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStateFactory();
            BindStateMachine();
            BindStrategyFactory();
            BindCoroutineRunner();
            BindSceneLoader();
        }

        private void BindStateFactory()
        {
            Container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle();
        }

        public void BindStateMachine()
        {
            Container
                .Bind<StateMachine>()
                .AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .AsSingle();
        }

        private void BindStrategyFactory()
        {
            Container
                .Bind<IStrategyFactory>()
                .To<StrategyFactory>()
                .AsSingle();
        }
    }
}