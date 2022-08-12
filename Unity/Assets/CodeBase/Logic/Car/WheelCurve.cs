using UnityEngine;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(WheelCollider))]
    public class WheelCurve : MonoBehaviour
    {
        public AnimationCurve ForwardFriction;
        public AnimationCurve SidewaysFriction;
    }
}