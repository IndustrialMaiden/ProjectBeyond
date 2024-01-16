using UnityEngine;

namespace _CONTENT.CodeBase.Infrastructure.CoroutineRunner
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}