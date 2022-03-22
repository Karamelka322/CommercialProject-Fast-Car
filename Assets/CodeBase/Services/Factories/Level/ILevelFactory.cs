using CodeBase.Logic.Item;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Services.Factories.Level
{
    public interface ILevelFactory : IService
    {
        void LoadGenerator(PointData spawnPoint);
        Capsule LoadCapsule(PointData spawnPoint);
    }
}