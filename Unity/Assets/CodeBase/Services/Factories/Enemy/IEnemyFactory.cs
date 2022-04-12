using System.Threading.Tasks;
using CodeBase.Data.Static.Enemy;
using CodeBase.Services.Random;

namespace CodeBase.Services.Factories.Enemy
{
    public interface IEnemyFactory : IService
    {
        Task CreateEnemy(EnemyTypeId enemyType, EnemyDifficultyTypeId difficultyType, PointData point);
    }
}