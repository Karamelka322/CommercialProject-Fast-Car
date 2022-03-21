using CodeBase.Data.Static.Enemy;
using CodeBase.Services.Defeat;
using CodeBase.Services.Pause;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.StaticData;
using CodeBase.Services.Victory;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories.Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;
        
        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void CreateEnemy(EnemyTypeId enemyType, EnemyDifficultyTypeId difficultyType, PointData spawnPoint)
        {
            GameObject prefab = _diContainer.Resolve<IStaticDataService>().ForEnemy(enemyType, difficultyType).Prefab.gameObject;
            InstantiateRegister(prefab, spawnPoint);
        }

        private void InstantiateRegister(Object prefab, PointData spawnPoint)
        {
            GameObject gameObject = _diContainer.InstantiatePrefab(prefab, spawnPoint.Position, spawnPoint.Rotation, null);
            
            _diContainer.Resolve<IPauseService>().Register(gameObject);
            _diContainer.Resolve<IReplayService>().Register(gameObject);
            _diContainer.Resolve<IDefeatService>().Register(gameObject);
            _diContainer.Resolve<IVictoryService>().Register(gameObject);
        }
    }
}