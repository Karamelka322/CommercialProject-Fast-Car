using System;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.LoadScene
{
    public interface ISceneLoaderService : IService
    {
        void Load(string name, LoadSceneMode mode, Action onLoaded = null);
    }
}