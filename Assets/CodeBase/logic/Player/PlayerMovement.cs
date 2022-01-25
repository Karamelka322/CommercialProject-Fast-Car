using UnityEngine;

namespace CodeBase.logic.Player
{
    [RequireComponent(typeof(Player), typeof(Car.Car))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Car.Car _car;
        
        private void Update()
        {
            _car.Movement(Input.GetAxis("Vertical"));
            _car.Rotation(Input.GetAxis("Horizontal"));
        }
    }
}