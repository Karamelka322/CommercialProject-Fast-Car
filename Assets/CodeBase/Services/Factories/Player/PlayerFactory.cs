using CodeBase.Data.Static.Player;
using CodeBase.Infrastructure;
using CodeBase.Logic.Car;
using CodeBase.Logic.Player;
using CodeBase.Services.Input;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Services.Factories.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly ITweenService _tweenService;
        private readonly IUpdateService _updateService;
        private readonly IPauseService _pauseService;

        public GameObject Player { get; private set; }
        
        public PlayerFactory(
            IStaticDataService staticDataService,
            IInputService inputService,
            ITweenService tweenService,
            IUpdateService updateService,
            IPersistentDataService persistentDataService,
            IPauseService pauseService)
        {
            _persistentDataService = persistentDataService;
            _pauseService = pauseService;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _tweenService = tweenService;
            _updateService = updateService;
        }

        public GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at)
        {
            PlayerStaticData playerStaticData = _staticDataService.ForPlayer(typeId);

            GameObject player = InstantiateRegister(at, playerStaticData);
            
            if(player.TryGetComponent(out PlayerMovement movement))
                movement.Construct(_inputService, _updateService);

            if(player.TryGetComponent(out PlayerHook hook))
                hook.Construct(_tweenService);
         
            if(player.TryGetComponent(out PlayerHealth health))
                health.Construct(_persistentDataService.PlayerData.SessionData.PlayerData);

            foreach (Wheel wheel in player.GetComponentsInChildren<Wheel>()) 
                wheel.Construct(_updateService);

            return Player = player;
        }

        private GameObject InstantiateRegister(Vector3 at, PlayerStaticData playerStaticData)
        {
            GameObject gameObject = Object.Instantiate(playerStaticData.Prefab.gameObject, at, Quaternion.identity);
            _pauseService.Register(gameObject);
            return gameObject;
        }
    }
}