using UnityEngine;

namespace CodeBase.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] 
        private Transform _inputContainer;

        [SerializeField] 
        private Transform _waymarkerContainer;

        public Transform InputContainer => _inputContainer;
        public Transform WaymarkerContainer => _waymarkerContainer;
    }
}