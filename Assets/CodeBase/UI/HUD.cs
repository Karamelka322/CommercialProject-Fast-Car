using UnityEngine;

namespace CodeBase.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] 
        private Transform _controlContainer;

        public Transform ControlContainer => _controlContainer;
    }
}