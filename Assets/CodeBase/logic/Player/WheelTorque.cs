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

        private void Forward()
        {
            
        }
    }
}