using System;
using System.Collections;
using CodeBase.Infrastructure;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.LoadScene
{
    [UsedImplicitly]
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoaderService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, LoadSceneMode mode, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(name, mode, onLoaded));

        private static IEnumerator LoadScene(string name, LoadSceneMode mode, Action onLoaded = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name, mode);

            while (!asyncOperation.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}