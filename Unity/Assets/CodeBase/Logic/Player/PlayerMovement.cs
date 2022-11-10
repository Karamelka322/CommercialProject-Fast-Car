using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Services.Defeat;
using CodeBase.Services.Input;
using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    [RequireComponent(typeof(PlayerPrefab), typeof(Car.Car))]
    public class PlayerMovement : MonoBehaviour, IReplayHandler
    {
        [SerializeField]
        private Car.Car _car;

        private IUpdateService _updateService;
        private ILevelMediator _mediator;
        private IRandomService _randomService;

        [Inject]
        public void Construct(ILevelMediator mediator, IUpdateService updateService, IRandomService randomService)
        {
            _randomService = randomService;
            _mediator = mediator;
            _updateService = updateService;
        }
        
        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            _car.Property.Axis = _mediator.MovementAxis();
            
            if(_mediator.Drift)
                _car.EnableDrift();
            else
                _car.DisableDrift();
        }

        public void OnReplay()
        {
            transform.position = _randomService.PlayerSpawnPoint();
            transform.rotation = Quaternion.identity;
            
            _car.DisableDrift();
        }
    }
}