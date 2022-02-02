using UnityEngine;

namespace CodeBase.Services.Input
{
    public class EditorInputModule : IInputModule
    {
        private readonly IInputVariant _standeloneInput;

        public IInputVariant CurrentInputVariant { get; set; }
        public Vector2 Axis => CurrentInputVariant.Axis + _standeloneInput.Axis;

        public EditorInputModule()
        {
            _standeloneInput = new KeyboardsInputVariant();
        }
    }
}