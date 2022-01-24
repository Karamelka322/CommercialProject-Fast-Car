using UnityEngine;

namespace CodeBase.logic.Player
{
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour
    {
        [SerializeField] 
        private WheelTorque torque;
        
        [SerializeField] 
        private WheelCollider _collider;
    }
}