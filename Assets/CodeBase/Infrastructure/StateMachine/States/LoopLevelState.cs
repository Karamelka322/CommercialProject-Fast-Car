using CodeBase.Logic.Item;
using CodeBase.Services.Factories.Enemy;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoopLevelState : IUpdateableState
    {
        private readonly IRandomService _randomService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ILevelFactory _levelFactory;
        private readonly IUpdatable _updatable;

        private Capsule _capsule;

        public LoopLevelState(ILevelFactory levelFactory, IEnemyFactory enemyFactory, IRandomService randomService, IUpdatable updatable)
        {
            _levelFactory = levelFactory;
            _enemyFactory = enemyFactory;
            _updatable = updatable;
            _randomService = randomService;
        }

        public void Enter() => 
            _updatable.OnUpdate += OnUpdate;

        public void Exit() => 
            _updatable.OnUpdate -= OnUpdate;

        public void OnUpdate()
        {
            if (_capsule == null)
                _capsule = _levelFactory.LoadCapsule(_randomService.CapsuleSpawnPoint());
        }

        private void InitEnemy(Transform player) => 
            _enemyFactory.CreateEnemy(player, _randomService.EnemySpawnPoint());
    }
}