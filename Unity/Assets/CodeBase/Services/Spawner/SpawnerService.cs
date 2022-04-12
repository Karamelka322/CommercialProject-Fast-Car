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
        private readonly IGeneratorSpawnerModule _generatorSpawnerModule;

        public SpawnerService(
            IRandomService randomService,
            ILevelFactory levelFactory,
            IEnemyFactory enemyFactory,
            IPersistentDataService persistentDataService,
            ICorutineRunner corutineRunner)
        {
            _generatorSpawnerModule = new GeneratorSpawnerModule(levelFactory, randomService);
            _capsuleSpawnerModule = new CapsuleSpawnerModule(levelFactory, randomService);
            _enemySpawnerModule = new EnemySpawnerModule(randomService, enemyFactory, persistentDataService, corutineRunner);
        }

        public void SetConfig(LevelStaticData levelStaticData)
        {
            _generatorSpawnerModule.SetConfig(levelStaticData);
            _capsuleSpawnerModule.SetConfig(levelStaticData);
            _enemySpawnerModule.SetConfig(levelStaticData);
        }

        public void SpawnOnLoaded()
        {
            _generatorSpawnerModule.TrySpawnGenerator();
        }

        public void SpawnOnUpdate()
        {
            _capsuleSpawnerModule.TrySpawnCapsule();
            _enemySpawnerModule.TrySpawnEnemy();
        }

        public void CleanUp()
        {
            _generatorSpawnerModule.Clear();
            _capsuleSpawnerModule.Clear();
            _enemySpawnerModule.Clear();
        }

        public void Reset() => 
            _enemySpawnerModule.ResetModule();
    }
}