using UnityEngine;

namespace CodeBase.Services.Replay
{
    public interface IReplayService : IService
    {
        void Register(GameObject gameObject);
        void InformHandlers();
        void CleanUp();
    }
}