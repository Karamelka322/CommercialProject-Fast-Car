using CodeBase.Data.Static.Enemy;
using CodeBase.Services.Random;

namespace CodeBase.Services.Factories.Enemy
{
    public interface IEnemyFactory : IService
    {
        void CreateEnemy(EnemyTypeId enemyType, EnemyDifficultyTypeId difficultyType, PointData point);
    }
}