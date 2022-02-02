using UnityEngine;

namespace CodeBase.Services.Input
{
    public class ButtonsInputVariant : IInputVariant
    {
        private readonly IInputService _inputService;
        
        private const string ButtonUpLeft = "UpLeft_button";
        private const string ButtonDownLeft = "DownLeft_button";
        private const string ButtonUpRight = "UpRight_button";
        private const string ButtonDownRight = "DownRight_button";

        private static readonly Vector2 UpLeft = new Vector2(1, -1);
        private static readonly Vector2 DownLeft = new Vector2(-1, -1);
        private static readonly Vector2 UpRight = new Vector2(1, 1);
        private static readonly Vector2 DownRight = new Vector2(-1, 1);

        private Vector2 _axis;

        public ButtonsInputVariant(IInputService inputService)
        {
            _inputService = inputService;
        }

        public Vector2 Axis => ButtonsAxis();

        private Vector2 ButtonsAxis()
        {
            _axis = Vector2.zero;
            
            _axis += _inputService.GetButton(ButtonUpLeft) ? UpLeft : Vector2.zero;
            _axis += _inputService.GetButton(ButtonDownLeft) ? DownLeft : Vector2.zero;
            _axis += _inputService.GetButton(ButtonUpRight) ? UpRight : Vector2.zero;
            _axis += _inputService.GetButton(ButtonDownRight) ? DownRight : Vector2.zero;
            
            return _axis;
        }
    }
}