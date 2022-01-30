using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.logic.Player
{
    [RequireComponent(typeof(PlayerPrefab), typeof(Car.Car))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Car.Car _car;

        private IInputService _inputService;
        
        public void Construct(IInputService inputService) => 
            _inputService = inputService;

        private void Update()
        {
            _car.Movement(_inputService.Axis.x);
            _car.Rotation(_inputService.Axis.y);
        }
    }
}