using CodeBase.Data.Static.Level;
using CodeBase.Infrastructure;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Update;

namespace CodeBase.Services.Spawner
{
    public class SpawnerService : ISpawnerService
    {
        private readonly IUpdateService _updateService;

        private readonly ICapsuleSpawnerModule _capsuleSpawnerModule;
        private readonly IEnemySpawnerModule _enemySpawnerModule;

        public SpawnerService(
            IUpdateService updateService,
            IRandomService randomService,
            ILevelFactory levelFactory,
            IEnemyFactory enemyFactory,
            IPersistentDataService persistentDataService,
            ICorutineRunner corutineRunner)
        {
            _updateService = updateService;
            
            _capsuleSpawnerModule = new CapsuleSpawnerModule(randomService, levelFactory);
            _enemySpawnerModule = new EnemySpawnerModule(randomService, enemyFactory, persistentDataService, corutineRunner);
        }

        public void SetConfig(LevelStaticData levelConfig)
        {
            _capsuleSpawnerModule.SetConfig(levelConfig);
            _enemySpawnerModule.SetConfig(levelConfig);
        }

        public void Enable() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnUpdate()
        {
            _capsuleSpawnerModule.TrySpawnCapsule();
            _enemySpawnerModule.TrySpawnEnemy();
        }

        public void Disable() => 
            _updateService.OnUpdate -= OnUpdate;

        public void Clenup()
        {
            _capsuleSpawnerModule.ClenupModule();
            _enemySpawnerModule.ClenupModule();
        }
    }
}