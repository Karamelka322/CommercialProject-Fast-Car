using CodeBase.Data.Static.Level;
using UnityEngine;

namespace CodeBase.Services.Random
{
    public interface IRandomService : IService
    {
        PointData EnergySpawnPoint();
        Vector3 PlayerSpawnPoint();
        PointData GeneratorSpawnPoint();
        PointData EnemySpawnPoint();
        void SetConfig(LevelStaticData levelCongig);
        void BindObjectToSpawnPoint(Object obj, PointData point);
        int GetNumberUnlockedCapsuleSpawnPoints();
        int GetNumberUnlockedEnemySpawnPoints();
        void BindTimeToSpawnPoint(float time, PointData point);
        void CleanUp();
    }
}