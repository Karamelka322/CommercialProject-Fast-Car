using CodeBase.Logic.Car;
using CodeBase.Services.Factories.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Mediator.Level
{
    public class LevelMediator : MonoBehaviour, ILevelMediator
    {
        private IPlayerFactory _playerFactory;

        [Inject]
        private void Construct(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public Accident GetPlayerAccident() => _playerFactory.Player.GetComponentInChildren<Accident>();
    }
}