using UnityEngine;

namespace CodeBase.Services.Input
{
    public class KeyboardsInputVariant : IInputVariant
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        private Vector2 _axis;
        
        public Vector2 Axis => UnityAxis();

        private Vector2 UnityAxis()
        {
            float axisX = UnityEngine.Input.GetAxis(Vertical);
            float axisY = UnityEngine.Input.GetAxis(Horizontal); 
            
            _axis.x = axisX != -1 ? axisX : -2;
            _axis.y = axisY != -1 ? axisY : -2;
            
            return _axis;
        }
    }
}