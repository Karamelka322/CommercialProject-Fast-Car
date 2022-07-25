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
            _axis.x = UnityEngine.Input.GetAxis(Vertical);
            _axis.y = UnityEngine.Input.GetAxis(Horizontal);

            return _axis;
        }
    }
}