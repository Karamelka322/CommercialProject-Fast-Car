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
            _updateService.OnFixedUpdate += OnFixedUpdate;
        }

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnFixedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                _car.EnableDrift();
            
            if(Input.GetKeyUp(KeyCode.Space))
                _car.DisableDrift();
        }

        private void OnUpdate()
        {
            _car.Movement(_inputService.Axis.x);
            _car.Rotation(_inputService.Axis.y);
        }

        public void OnReplay() => 
            _car.Property.UseDrift = false;

        public void OnDefeat()
        {
            _car.Movement(0);
            _car.Rotation(0);
        }

        public void OnVictory()
        {
            _car.Movement(0);
            _car.Rotation(0);
        }
    }
}