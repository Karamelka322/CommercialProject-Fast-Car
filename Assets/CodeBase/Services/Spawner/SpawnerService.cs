using CodeBase.Logic.Item;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.Update;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Services.Spawner
{
    public class SpawnerService : ISpawnerService
    {
        private readonly IUpdateService _updateService;
        private readonly IRandomService _randomService;
        private readonly IUIFactory _uiFactory;
        private readonly ILevelFactory _levelFactory;
        private readonly IPersistentDataService _persistentDataService;
        
        private Capsule _capsule;

        public SpawnerService(IUpdateService updateService, IRandomService randomService, IUIFactory uiFactory, ILevelFactory levelFactory, IPersistentDataService persistentDataService)
        {
            _updateService = updateService;
            _randomService = randomService;
            _uiFactory = uiFactory;
            _levelFactory = levelFactory;
            _persistentDataService = persistentDataService;
        }

        public void Enable() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnUpdate()
        {
            TrySpawnCapsule();

            AddTimeInStopwatch(Time.deltaTime);
        }

        public void Disable() => 
            _updateService.OnUpdate -= OnUpdate;

        public void Clenup()
        {
            _capsule = null;
        }

        private void TrySpawnCapsule()
        {
            if (_capsule == null)
            {
                _capsule = _levelFactory.LoadCapsule(_randomService.CapsuleSpawnPoint());
                _uiFactory.HUD.GetComponentInChildren<Waymarkers>().Target = _capsule.transform;
            }
        }

        // private void InitEnemy() => 
        //     _enemyFactory.CreateEnemy(_playerFactory.Player.transform, _randomService.EnemySpawnPoint());

        private void AddTimeInStopwatch(float time) => 
            _persistentDataService.PlayerData.SessionData.StopwatchTime += time;
    }

    public interface ISpawnerService : IService
    {
        void Enable();
        void Disable();
        void Clenup();
    }
}