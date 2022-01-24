using UnityEngine;

namespace CodeBase.logic.Player
{
    [RequireComponent(typeof(Player), typeof(Motor), typeof(SteeringGear))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private Motor _motor;

        [SerializeField] 
        private SteeringGear _steeringGear;

        private void Update()
        {
            _motor.Torque(Input.GetAxis("Vertical"));
            _steeringGear.Angle(Input.GetAxis("Horizontal"));
        }
    }
}