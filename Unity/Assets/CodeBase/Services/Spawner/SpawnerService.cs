using System.Threading.Tasks;
using CodeBase.Data.Static.Level;
using CodeBase.Infrastructure;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Services.Spawner
{
    [UsedImplicitly]
    public class SpawnerService : ISpawnerService
    {
        public EnergySpawnerModule EnergySpawnerModule { get; }
        
        private readonly IEnemySpawnerModule _enemySpawnerModule;
        private readonly IGeneratorSpawnerModule _generatorSpawnerModule;

        private LevelStaticData _levelStaticData;

        private bool UsingGenerator => _levelStaticData.Spawn.Generator.UsingGenerator;
        private bool UsingCapsule => _levelStaticData.Spawn.energy.UsingCapsule;
        private bool UsingEnemy => _levelStaticData.Spawn.Enemy.UsingEnemy;

        public SpawnerService(
            IRandomService randomService,
            ILevelFactory levelFactory,
            IEnemyFactory enemyFactory,
            IPersistentDataService persistentDataService,
            ICoroutineRunner coroutineRunner)
        {
            _generatorSpawnerModule = new GeneratorSpawnerModule(levelFactory, randomService);
            EnergySpawnerModule = new EnergySpawnerModule(levelFactory, randomService);
            _enemySpawnerModule = new EnemySpawnerModule(randomService, enemyFactory, persistentDataService, coroutineRunner);
        }

        public void SetConfig(LevelStaticData levelStaticData)
        {
            _levelStaticData = levelStaticData;
            
            if(UsingGenerator)
                _generatorSpawnerModule.SetConfig(levelStaticData.Spawn.Generator);
            
            if(UsingCapsule)
                EnergySpawnerModule.SetConfig(levelStaticData.Spawn.energy);

            if(UsingEnemy)
                _enemySpawnerModule.SetConfig(levelStaticData.Spawn.Enemy);
        }

        public async Task LoadResourcesAsync()
        {
            if (UsingGenerator)
                await _generatorSpawnerModule.LoadResourcesAsync();
            
            if (UsingCapsule)
                await EnergySpawnerModule.LoadResourcesAsync();

            if (UsingEnemy)
                await _enemySpawnerModule.LoadResourcesAsync();
        }

        public async Task SpawnOnLoaded()
        {
            if(UsingGenerator)
                await _generatorSpawnerModule.SpawnGeneratorAsync();
        }

        public async void SpawnOnUpdate()
        {
            if(UsingCapsule)
                await EnergySpawnerModule.TrySpawnEnergy();
            
            if(UsingEnemy)
                _enemySpawnerModule.TrySpawnEnemy();
        }

        public void CleanUp()
        {
            if(UsingGenerator)
                _generatorSpawnerModule.Clear();

            if(UsingCapsule)
                EnergySpawnerModule.Clear();

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