using UnityEngine;

namespace _CONTENT.CodeBase.Infrastructure.CoroutineRunnerDir
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}