using CodeBase.Data.Static.Level;
using UnityEngine;

namespace CodeBase.Services.Random
{
    public interface IRandomService : IService
    {
        Vector3 CapsuleSpawnPoint();
        Vector3 PlayerSpawnPoint();
        Vector3 GeneratorSpawnPoint();
        Vector3 EnemySpawnPoint();
        void SetConfig(LevelStaticData levelCongig);
    }
}