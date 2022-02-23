using CodeBase.Services.Input;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    [RequireComponent(typeof(PlayerPrefab), typeof(Car.Car))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Car.Car _car;

        private IInputService _inputService;
        private IUpdateService _updateService;

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
            _car.Movement(_inputService.Axis.x);
            _car.Rotation(_inputService.Axis.y);
        }
    }
}