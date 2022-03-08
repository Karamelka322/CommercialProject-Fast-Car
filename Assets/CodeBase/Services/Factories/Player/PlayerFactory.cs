using CodeBase.Data.Static.Player;
using CodeBase.Logic.Car;
using CodeBase.Logic.Player;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Input;
using CodeBase.Services.Pause;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;

namespace CodeBase.Services.Factories.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly ITweenService _tweenService;
        private readonly IUpdateService _updateService;
        private readonly IPauseService _pauseService;
        private readonly IReadWriteDataService _readWriteDataService;
        private readonly IReplayService _replayService;
        private readonly IRandomService _randomService;
        private readonly IUIFactory _uiFactory;
        private readonly IDefeatService _defeatService;
        private readonly IVictoryService _victoryService;

        public GameObject Player { get; private set; }
        
        public PlayerFactory(
            IStaticDataService staticDataService,
            IInputService inputService,
            ITweenService tweenService,
            IUpdateService updateService,
            IPauseService pauseService,
            IReadWriteDataService readWriteDataService,
            IReplayService replayService,
            IRandomService randomService,
            IUIFactory uiFactory,
            IDefeatService defeatService,
            IVictoryService victoryService)
        {
            _pauseService = pauseService;
            _readWriteDataService = readWriteDataService;
            _replayService = replayService;
            _randomService = randomService;
            _uiFactory = uiFactory;
            _defeatService = defeatService;
            _victoryService = victoryService;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _tweenService = tweenService;
            _updateService = updateService;
        }

        public GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at)
        {
            PlayerStaticData playerStaticData = _staticDataService.ForPlayer(typeId);

            Player = InstantiateRegister(at, playerStaticData);
            
            if(Player.TryGetComponent(out PlayerPrefab player))
                player.Construct(_randomService);

            if(Player.TryGetComponent(out PlayerMovement movement))
                movement.Construct(_inputService, _updateService);

            if(Player.TryGetComponent(out PlayerHook hook))
                hook.Construct(_tweenService);
         
            if(Player.TryGetComponent(out PlayerDefeat defeat))
                defeat.Construct(_uiFactory);
            
            if(Player.TryGetComponent(out PlayerVictory victory))
                victory.Construct(_uiFactory);
            
            foreach (Wheel wheel in Player.GetComponentsInChildren<Wheel>()) 
                wheel.Construct(_updateService);

            return Player;
        }

        private GameObject InstantiateRegister(Vector3 at, PlayerStaticData playerStaticData)
        {
            GameObject gameObject = Object.Instantiate(playerStaticData.Prefab.gameObject, at, Quaternion.identity);
            _pauseService.Register(gameObject);
            _readWriteDataService.Register(gameObject);
            _replayService.Register(gameObject);
            _defeatService.Register(gameObject);
            _victoryService.Register(gameObject);
            return gameObject;
        }
    }
}