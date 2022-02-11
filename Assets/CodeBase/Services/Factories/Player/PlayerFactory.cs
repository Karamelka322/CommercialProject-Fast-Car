using CodeBase.Data.Static.Player;
using CodeBase.Infrastructure;
using CodeBase.Logic.Car;
using CodeBase.Logic.Player;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using UnityEngine;

namespace CodeBase.Services.Factories.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly ITweenService _tweenService;
        private readonly IUpdatable _updatable;

        public GameObject Player { get; private set; }
        
        public PlayerFactory(IStaticDataService staticDataService, IInputService inputService, ITweenService tweenService, IUpdatable updatable, IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _tweenService = tweenService;
            _updatable = updatable;
        }

        public GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at)
        {
            PlayerStaticData playerStaticData = _staticDataService.ForPlayer(typeId);

            GameObject player = Object.Instantiate(playerStaticData.Prefab.gameObject, at, Quaternion.identity);
            
            if(player.TryGetComponent(out PlayerMovement movement))
                movement.Construct(_inputService, _updatable);

            if(player.TryGetComponent(out PlayerHook hook))
                hook.Construct(_tweenService);
         
            if(player.TryGetComponent(out PlayerHealth health))
                health.Construct(_persistentDataService.PlayerData.SessionData.PlayerData);

            if(player.TryGetComponent(out Stabilization stabilization))
                stabilization.Construct(_updatable);

            foreach (Wheel wheel in player.GetComponentsInChildren<Wheel>()) 
                wheel.Construct(_updatable);

            return Player = player;
        }
    }
}