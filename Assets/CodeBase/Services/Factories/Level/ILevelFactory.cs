using CodeBase.Logic.Item;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Services.Factories.Level
{
    public interface ILevelFactory : IService
    {
        GameObject LoadGenerator(PointData spawnPoint);
        Capsule LoadCapsule(PointData spawnPoint);
    }
}