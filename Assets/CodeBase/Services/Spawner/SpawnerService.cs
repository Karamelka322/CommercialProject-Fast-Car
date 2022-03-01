using System;
using CodeBase.Data.Static.Level;
using CodeBase.Extension;
using CodeBase.Logic.Item;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Random;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Services.Spawner
{
    public class SpawnerService : ISpawnerService
    {
        private readonly IUpdateService _updateService;
        private readonly IRandomService _randomService;
        private readonly ILevelFactory _levelFactory;

        private LevelStaticData _config;
        
        private Capsule[] _capsules;

        public SpawnerService(IUpdateService updateService, IRandomService randomService, ILevelFactory levelFactory)
        {
            _updateService = updateService;
            _randomService = randomService;
            _levelFactory = levelFactory;
        }

        public void SetConfig(LevelStaticData levelConfig)
        {
            _config = levelConfig;

            _capsules = new Capsule[levelConfig.Capsule.Quantity];
        }

        public void Enable() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnUpdate()
        {
            if(_config == null)
                return;
            
            TrySpawnCapsule();
        }

        public void Disable() => 
            _updateService.OnUpdate -= OnUpdate;

        public void Clenup() => 
            _capsules = Array.Empty<Capsule>();

        private void TrySpawnCapsule()
        {
            if (IsSpawnedCapsule())
                _capsules[_capsules.GetEmptyIndex()] = LoadCapsule();
        }

        private bool IsSpawnedCapsule() => 
            _config.Capsule.UsingCapsule && _capsules.NumberEmptyIndexes() != 0;

        private Capsule LoadCapsule() => 
            _levelFactory.LoadCapsule(_randomService.CapsuleSpawnPoint());

        // private void InitEnemy() => 
        //     _enemyFactory.CreateEnemy(_playerFactory.Player.transform, _randomService.EnemySpawnPoint());
    }
}