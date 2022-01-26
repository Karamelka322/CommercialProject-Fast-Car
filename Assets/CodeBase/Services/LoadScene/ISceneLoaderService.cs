using System;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.Input.LoadScene
{
    public interface ISceneLoaderService : IService
    {
        void Load(string name, LoadSceneMode mode, Action onLoaded = null);
    }
}