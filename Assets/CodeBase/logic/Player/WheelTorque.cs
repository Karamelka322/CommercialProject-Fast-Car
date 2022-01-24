using UnityEngine;

namespace CodeBase.logic.Player
{
    public class WheelTorque : MonoBehaviour
    {
        [SerializeField] 
        private float _movement;

        private void Update()
        {
            if(_movement != 0)
                Forward();
        }

        private void Forward() => 
            transform.Rotate(transform.localEulerAngles + Vector3.right * Time.deltaTime * _movement, Space.Self);
    }
}