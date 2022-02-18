using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    [RequireComponent(typeof(PlayerPrefab), typeof(Car.Car))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Car.Car _car;

        private IInputService _inputService;
        private IUpdatable _updatable;

        public void Construct(IInputService inputService, IUpdatable updatable)
        {
            _inputService = inputService;
            _updatable = updatable;
        }

        private void Start() => 
            _updatable.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updatable.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            _car.Movement(_inputService.Axis.x);
            _car.Rotation(_inputService.Axis.y);
        }
    }
}