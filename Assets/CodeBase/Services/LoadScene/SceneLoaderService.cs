using System;
using System.Collections;
using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.LoadScene
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly ICorutineRunner _corutineRunner;

        public SceneLoaderService(ICorutineRunner corutineRunner)
        {
            _corutineRunner = corutineRunner;
        }

        public void Load(string name, LoadSceneMode mode, Action onLoaded = null) => 
            _corutineRunner.StartCoroutine(LoadScene(name, mode, onLoaded));

        private static IEnumerator LoadScene(string name, LoadSceneMode mode, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name) 
                onLoaded?.Invoke();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name, mode);

            while (!asyncOperation.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}