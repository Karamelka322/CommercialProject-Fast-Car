using UnityEngine;

namespace CodeBase.Services.Pause
{
    public interface IPauseService : IService
    {
        bool IsPause { get; }
        float PauseTime { get; }
        void SetPause(bool isPause);
        void Register(GameObject gameObject);
        void CleanUp();
    }
}