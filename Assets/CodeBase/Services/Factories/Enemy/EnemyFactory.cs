using CodeBase.Infrastructure;
using CodeBase.Logic.Car;
using CodeBase.Logic.Enemy;
using CodeBase.Services.AssetProvider;
using UnityEngine;

namespace CodeBase.Services.Factories.Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly IUpdatable _updatable;

        public EnemyFactory(IAssetProviderService assetProviderService, IUpdatable updatable)
        {
            _assetProviderService = assetProviderService;
            _updatable = updatable;
        }

        public void CreateEnemy(Transform player, Vector3 at)
        {
            GameObject prefab = _assetProviderService.LoadEnemy();
            GameObject enemy = Object.Instantiate(prefab, at, Quaternion.identity);
         
            if(enemy.TryGetComponent(out EnemyMovement movement))
                movement.Construct(_updatable);
            
            enemy.GetComponentInChildren<NavMeshAgentWrapper>()?.Construct(_updatable, player);
            
            foreach (Wheel wheel in enemy.GetComponentsInChildren<Wheel>()) 
                wheel.Construct(_updatable);
        }
    }
}