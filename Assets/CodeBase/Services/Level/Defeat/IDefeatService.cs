using UnityEngine;

namespace CodeBase.Services.Defeat
{
    public interface IDefeatService : IService
    {
        void Register(GameObject obj);
        void CleanUp();
        void SetDefeat(bool isDefeat);
        bool IsDefeat { get; }
    }
}