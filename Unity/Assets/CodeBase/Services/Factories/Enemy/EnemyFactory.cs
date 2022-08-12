using System.Threading.Tasks;
using CodeBase.Data.Static.Enemy;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Defeat;
using CodeBase.Services.Pause;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.StaticData;
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

        public async Task CreateEnemy(EnemyTypeId enemyType, EnemyDifficultyTypeId difficultyType, PointData spawnPoint)
        {
            EnemyStaticData enemyData = _diContainer.Resolve<IStaticDataService>().ForEnemy(enemyType, difficultyType);

            GameObject prefab = await _diContainer
                .Resolve<IAssetManagementService>()
                .LoadAsync<GameObject>(enemyData.PrefabReference);
            
            InstantiateRegister(prefab, spawnPoint);
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