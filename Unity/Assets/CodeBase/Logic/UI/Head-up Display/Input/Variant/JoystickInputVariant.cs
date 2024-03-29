using System;
using CodeBase.Services.Input.Element;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class JoystickInputVariant : MonoBehaviour, IInputVariant
    {
        [SerializeField] 
        private JoystickInputElement _joystick;
        
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        private Vector2 _axis;
        public Vector2 Axis => GetAxis();
        public bool Drift { get; }

        private Vector2 GetAxis()
        {
            _axis = Vector2.zero;
            _axis += _joystick.Axis;
            
#if UNITY_EDITOR
            
            _axis.x += UnityEngine.Input.GetAxis(Vertical);
            _axis.y += UnityEngine.Input.GetAxis(Horizontal);
            
#endif

            return _axis;
        }

        public void EnableMoveBackwardsButton() { }
        public (ButtonInputElement, ButtonInputElement) GetBackwardsButton()
        {
            throw new NotImplementedException();
        }

        public void DisableMoveBackwardsButton() { }
    }
}