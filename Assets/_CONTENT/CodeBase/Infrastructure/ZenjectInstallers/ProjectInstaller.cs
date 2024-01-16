using _CONTENT.CodeBase.Infrastructure.CoroutineRunner;
using _CONTENT.CodeBase.Infrastructure.SceneControl;
using _CONTENT.CodeBase.Infrastructure.StateControl;
using _CONTENT.CodeBase.Infrastructure.StateFactory;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.ZenjectInstallers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStateFactory();
            BindStateMachine();
            BindCoroutineRunner();
            BindSceneLoader();
        }

        private void BindStateFactory()
        {
            Container
                .Bind<IStateFactory>()
                .To<StateFactory.StateFactory>()
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
                .To<CoroutineRunner.CoroutineRunner>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .AsSingle();
        }
    }
}