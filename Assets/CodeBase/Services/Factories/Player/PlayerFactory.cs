using CodeBase.logic.Player;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Services.Factories.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;

        public PlayerFactory(IStaticDataService staticDataService, IInputService inputService)
        {
            _staticDataService = staticDataService;
            _inputService = inputService;
        }

        public GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at)
        {
            PlayerStaticData staticData = _staticDataService.ForPlayer(typeId);

            GameObject player = Object.Instantiate(staticData.Prefab.gameObject, at, Quaternion.identity);
            
            if(player.TryGetComponent(out PlayerMovement component))
                component.Construct(_inputService);
            
            return player;
        }
    }
}