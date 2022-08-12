using System.Threading.Tasks;
using CodeBase.Data.Static.Level;
using CodeBase.Infrastructure;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using JetBrains.Annotations;

namespace CodeBase.Services.Spawner
{
    [UsedImplicitly]
    public class SpawnerService : ISpawnerService
    {
        private readonly ICapsuleSpawnerModule _capsuleSpawnerModule;
        private readonly IEnemySpawnerModule _enemySpawnerModule;
        private readonly IGeneratorSpawnerModule _generatorSpawnerModule;

        private LevelStaticData _levelStaticData;

        private bool UsingGenerator => _levelStaticData.Spawn.Generator.UsingGenerator;
        private bool UsingCapsule => _levelStaticData.Spawn.Capsule.UsingCapsule;
        private bool UsingEnemy => _levelStaticData.Spawn.Enemy.UsingEnemy;

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
            _levelStaticData = levelStaticData;
            
            if(UsingGenerator)
                _generatorSpawnerModule.SetConfig(levelStaticData.Spawn.Generator);
            
            if(UsingCapsule)
                _capsuleSpawnerModule.SetConfig(levelStaticData.Spawn.Capsule);

            if(UsingEnemy)
                _enemySpawnerModule.SetConfig(levelStaticData.Spawn.Enemy);
        }

        public async Task SpawnOnLoaded()
        {
            if(UsingGenerator)
                await _generatorSpawnerModule.SpawnGenerator();
        }

        public async void SpawnOnUpdate()
        {
            if(UsingCapsule)
                await _capsuleSpawnerModule.TrySpawnCapsule();
            
            if(UsingEnemy)
                _enemySpawnerModule.TrySpawnEnemy();
        }

        public void CleanUp()
        {
            if(UsingGenerator)
                _generatorSpawnerModule.Clear();

            if(UsingCapsule)
                _capsuleSpawnerModule.Clear();

            if(UsingEnemy)
                _enemySpawnerModule.Clear();
            
            _levelStaticData = null;
        }

        public void Reset()
        {
            if(UsingEnemy)
                _enemySpawnerModule.ResetModule();
        }
    }
}