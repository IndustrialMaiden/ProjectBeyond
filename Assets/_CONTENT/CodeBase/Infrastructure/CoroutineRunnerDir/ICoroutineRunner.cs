using System.Collections;
using UnityEngine;

namespace _CONTENT.CodeBase.Infrastructure.CoroutineRunnerDir
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);

        void StopAllCoroutines();
    }
}