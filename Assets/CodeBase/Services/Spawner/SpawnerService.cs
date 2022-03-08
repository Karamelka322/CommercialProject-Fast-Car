using CodeBase.Data.Static.Level;
using CodeBase.Infrastructure;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;

namespace CodeBase.Services.Spawner
{
    public class SpawnerService : ISpawnerService
    {
        private readonly ICapsuleSpawnerModule _capsuleSpawnerModule;
        private readonly IEnemySpawnerModule _enemySpawnerModule;

        public SpawnerService(
            IRandomService randomService,
            ILevelFactory levelFactory,
            IEnemyFactory enemyFactory,
            IPersistentDataService persistentDataService,
            ICorutineRunner corutineRunner)
        {
            _capsuleSpawnerModule = new CapsuleSpawnerModule(randomService, levelFactory);
            _enemySpawnerModule = new EnemySpawnerModule(randomService, enemyFactory, persistentDataService, corutineRunner);
        }

        public void SetConfig(LevelStaticData levelConfig)
        {
            _capsuleSpawnerModule.SetConfig(levelConfig);
            _enemySpawnerModule.SetConfig(levelConfig);
        }

        public void RealTimeSpawn()
        {
            _capsuleSpawnerModule.TrySpawnCapsule();
            _enemySpawnerModule.TrySpawnEnemy();
        }

        public void Clenup()
        {
            _capsuleSpawnerModule.ClenupModule();
            _enemySpawnerModule.ClenupModule();
        }
    }
}