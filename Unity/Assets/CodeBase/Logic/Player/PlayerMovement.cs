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
        
        private void Start()
        {
            _updateService.OnUpdate += OnUpdate;
            _inputService.InputVariant.OnStartDrift += OnStartDrift;
            _inputService.InputVariant.OnStopDrift += OnStopDrift;
        }

        private void OnDestroy()
        {
            _updateService.OnUpdate -= OnUpdate;
            _inputService.InputVariant.OnStartDrift -= OnStartDrift;
            _inputService.InputVariant.OnStopDrift -= OnStopDrift;
        }

        private void OnUpdate() => 
            _car.Property.Axis = _inputService.InputVariant.Axis;

        private void OnStartDrift() => 
            _car.EnableDrift();

        private void OnStopDrift() => 
            _car.DisableDrift();

        public void OnReplay() => 
            _car.DisableDrift();

        public void OnDefeat() => 
            _car.Property.Axis = Vector2.zero;

        public void OnVictory() => 
            _car.Property.Axis = Vector2.zero;
    }
}