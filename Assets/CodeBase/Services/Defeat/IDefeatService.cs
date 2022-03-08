using UnityEngine;

namespace CodeBase.Services.Defeat
{
    public interface IDefeatService : IService
    {
        void Register(GameObject obj);
        void Clenup();
        void SetDefeat(bool isDefeat);
        bool IsDefeat { get; }
    }
}