using UnityEngine;

namespace CodeBase.Services.Input
{
    public class EditorInputModule : IInputModule
    {
        private readonly IInputVariant _standeloneInput;

        public IInputVariant CurrentInputVariant { get; set; }
        public Vector2 Axis
        {
            get
            {
                Vector2 axis = CurrentInputVariant.Axis + _standeloneInput.Axis;
                
                axis.x = Mathf.Clamp(axis.x, -1, 1);
                axis.y = Mathf.Clamp(axis.y, -1, 1);
                
                return axis;
            }
        }

        public EditorInputModule()
        {
            _standeloneInput = new KeyboardsInputVariant();
        }
    }
}