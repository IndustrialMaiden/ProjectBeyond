using System.Collections;
using UnityEngine;

namespace _CONTENT.CodeBase.Infrastructure.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);

        void StopAllCoroutines();
    }
}