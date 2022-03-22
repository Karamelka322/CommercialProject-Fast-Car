using UnityEngine;

namespace CodeBase.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] 
        private Transform _inputContainer;

        public Transform InputContainer => _inputContainer;
    }
}