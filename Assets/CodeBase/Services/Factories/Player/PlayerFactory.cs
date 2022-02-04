using CodeBase.Data.Static.Player;
using CodeBase.logic.Player;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.Services.Tween;
using UnityEngine;

namespace CodeBase.Services.Factories.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly ITweenService _tweenService;

        public PlayerFactory(IStaticDataService staticDataService, IInputService inputService, ITweenService tweenService)
        {
            _staticDataService = staticDataService;
            _inputService = inputService;
            _tweenService = tweenService;
        }

        public GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at)
        {
            PlayerStaticData staticData = _staticDataService.ForPlayer(typeId);

            GameObject player = Object.Instantiate(staticData.Prefab.gameObject, at, Quaternion.identity);
            
            if(player.TryGetComponent(out PlayerMovement movement))
                movement.Construct(_inputService);

            if(player.TryGetComponent(out PlayerHook hook))
                hook.Construct(_tweenService);

            return player;
        }
    }
}