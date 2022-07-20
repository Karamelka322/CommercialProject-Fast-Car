using CodeBase.Services.Defeat;
using CodeBase.Services.Input;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    [RequireComponent(typeof(PlayerPrefab), typeof(Car.Car))]
    public class PlayerMovement : MonoBehaviour, IReplayHandler, IPlayerDefeatHandler, IPlayerVictoryHandler
    {
        [SerializeField]
        private Car.Car _car;

        private IInputService _inputService;
        private IUpdateService _updateService;

        [Inject]
        public void Construct(IInputService inputService, IUpdateService updateService)
        {
            _inputService = inputService;
            _updateService = updateService;
        }

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            _car.Movement(_inputService.Axis.normalized.x);
            _car.Rotation(_inputService.Axis.normalized.y);
        }

        public void OnReplay() => 
            _updateService.OnUpdate += OnUpdate;

        public void OnDefeat()
        {
            _updateService.OnUpdate -= OnUpdate;
            
            _car.Movement(0);
            _car.Rotation(0);
        }

        public void OnVictory()
        {
            _updateService.OnUpdate -= OnUpdate;
            
            _car.Movement(0);
            _car.Rotation(0);
        }
    }
}