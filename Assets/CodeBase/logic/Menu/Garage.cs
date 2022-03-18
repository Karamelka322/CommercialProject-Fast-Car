using CodeBase.Data.Static.Player;
using CodeBase.Services.Factories.Player;
using UnityEngine;

namespace CodeBase.Logic.Menu
{
    public class Garage : MonoBehaviour
    {
        private IPlayerFactory _playerFactory;

        public void Construct(IPlayerFactory playerFactory) => 
            _playerFactory = playerFactory;

        public void ChangePlayerCar(PlayerTypeId playerTypeId) => 
            _playerFactory.RebuildBasePreviewPlayerObject(playerTypeId);
    }
}