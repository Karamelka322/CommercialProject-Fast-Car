using CodeBase.Logic.Car;
using CodeBase.Logic.Enemy;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Pause;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Services.Factories.Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly IUpdateService _updateService;
        private readonly IPauseService _pauseService;

        public EnemyFactory(IAssetProviderService assetProviderService, IUpdateService updateService, IPauseService pauseService)
        {
            _assetProviderService = assetProviderService;
            _updateService = updateService;
            _pauseService = pauseService;
        }

        public void CreateEnemy(Transform player, Vector3 at)
        {
            GameObject prefab = _assetProviderService.LoadEnemy();
            GameObject enemy = InstantiateRegister(at, prefab);
         
            if(enemy.TryGetComponent(out EnemyMovement movement))
                movement.Construct(_updateService);
            
            enemy.GetComponentInChildren<NavMeshAgentWrapper>()?.Construct(_updateService, player);
            
            foreach (Wheel wheel in enemy.GetComponentsInChildren<Wheel>()) 
                wheel.Construct(_updateService);
        }

        private GameObject InstantiateRegister(Vector3 at, GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            _pauseService.Register(gameObject);
            return gameObject;
        }
    }
}