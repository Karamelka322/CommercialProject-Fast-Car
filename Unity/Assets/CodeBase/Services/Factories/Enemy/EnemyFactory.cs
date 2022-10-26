using System.Threading.Tasks;
using CodeBase.Data.Static.Level;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Defeat;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.Victory;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories.Enemy
{
    [UsedImplicitly]
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;
        
        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async Task LoadEnemyAsync(int id, PointData spawnPoint) => 
            InstantiateRegister(await LoadResourcesEnemyAsync(id), spawnPoint);

        public async Task LoadAllResourcesEnemyAsync()
        {
            EnemiesSpawnConfig config = _diContainer
                .Resolve<IPersistentDataService>()
                .PlayerData.SessionData.LevelData.CurrentLevelConfig.Spawn.Enemy;

            foreach (EnemySpawnConfig enemyConfig in config.Enemies)
            {
                await _diContainer
                    .Resolve<IAssetManagementService>()
                    .LoadAsync<GameObject>(enemyConfig.PrefabReference);
            }
        }

        private async Task<GameObject> LoadResourcesEnemyAsync(int id)
        {
            EnemiesSpawnConfig config = _diContainer
                .Resolve<IPersistentDataService>()
                .PlayerData.SessionData.LevelData.CurrentLevelConfig.Spawn.Enemy;

            return await _diContainer
                .Resolve<IAssetManagementService>()
                .LoadAsync<GameObject>(config.Enemies[id].PrefabReference);
        }

        private void InstantiateRegister(Object prefab, PointData spawnPoint)
        {
            GameObject gameObject = Instantiate(prefab, spawnPoint);
            Register(gameObject);
        }

        private GameObject Instantiate(in Object prefab, in PointData spawnPoint) => 
            _diContainer.InstantiatePrefab(prefab, spawnPoint.Position, spawnPoint.Rotation, null);

        private void Register(in GameObject gameObject)
        {
            _diContainer.Resolve<IPauseService>().Register(gameObject);
            _diContainer.Resolve<IReplayService>().Register(gameObject);
            _diContainer.Resolve<IDefeatService>().Register(gameObject);
            _diContainer.Resolve<IVictoryService>().Register(gameObject);
        }
    }
}