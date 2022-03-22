using UnityEngine;

namespace CodeBase.Services.Victory
{
    public interface IVictoryService : IService
    {
        void Register(GameObject obj);
        void CleanUp();
        void SetVictory(bool isDefeat);
        bool IsVictory { get; }
    }
}