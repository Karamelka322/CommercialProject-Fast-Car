using UnityEngine;

namespace CodeBase.Services.Input
{
    public class StandaloneInputService : InputService
    {
        private Vector2 _axis;
        
        public override Vector2 Axis => 
            UnityAxis();

        private Vector2 UnityAxis()
        {
            _axis.x = UnityEngine.Input.GetAxis(Vertical);
            _axis.y = UnityEngine.Input.GetAxis(Horizontal);
            
            return _axis;
        }
    }
}