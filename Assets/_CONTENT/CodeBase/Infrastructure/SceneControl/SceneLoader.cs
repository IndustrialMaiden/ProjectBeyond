using System;
using System.Collections;
using _CONTENT.CodeBase.Infrastructure.CoroutineRunnerDir;
using _CONTENT.CodeBase.Infrastructure.StateControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _CONTENT.CodeBase.Infrastructure.SceneControl
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStateFactory _stateFactory;
        private readonly DiContainer _projectContainer;

        private DiContainer _currentSceneContainer;

        public SceneLoader(ICoroutineRunner coroutineRunner, IStateFactory stateFactory, DiContainer projectContainer)
        {
            _coroutineRunner = coroutineRunner;
            _stateFactory = stateFactory;
            _projectContainer = projectContainer;
        }

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    
        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }
        
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
                yield return null;
            
            UpdateStateFactoryWithSceneContainer();

            onLoaded?.Invoke();
        }

        public void UpdateStateFactoryWithSceneContainer()
        {
            if (_currentSceneContainer != null)
            {
                _stateFactory.SetSceneContainer(_currentSceneContainer);
            }
            else
            {
                _stateFactory.SetSceneContainer(_projectContainer);
            }
        }

        public void UpdateSceneContainer(DiContainer sceneContainer)
        {
            _currentSceneContainer = sceneContainer;
        }
    }
}