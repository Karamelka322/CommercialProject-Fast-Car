using CodeBase.Data.Static.Player;
using CodeBase.Infrastructure;
using CodeBase.Logic.Car;
using CodeBase.Logic.Player;
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
        private readonly IUpdatable _updatable;

        public PlayerFactory(IStaticDataService staticDataService, IInputService inputService, ITweenService tweenService, IUpdatable updatable)
        {
            _staticDataService = staticDataService;
            _inputService = inputService;
            _tweenService = tweenService;
            _updatable = updatable;
        }

        public GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at)
        {
            PlayerStaticData staticData = _staticDataService.ForPlayer(typeId);

            GameObject player = Object.Instantiate(staticData.Prefab.gameObject, at, Quaternion.identity);
            
            if(player.TryGetComponent(out PlayerMovement movement))
                movement.Construct(_inputService, _updatable);

            if(player.TryGetComponent(out PlayerHook hook))
                hook.Construct(_tweenService);

            Wheel[] wheels = player.GetComponentsInChildren<Wheel>();

            for (int i = 0; i < wheels.Length; i++)
                wheels[i].Construct(_updatable);

            return player;
        }
    }
}