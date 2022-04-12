using UnityEngine;

namespace CodeBase.Services.Input
{
    public class JoystickInputVariant : IInputVariant
    {
        private readonly IInputService _inputService;

        private const string JoystickID = "Joystick";

        private Vector2 _axis;
        public Vector2 Axis => JoystickAxis();

        public JoystickInputVariant(IInputService inputService)
        {
            _inputService = inputService;
        }

        private Vector2 JoystickAxis()
        {
            _axis = Vector2.zero;
            _axis += _inputService.GetJoystick(JoystickID);

            return _axis;
        }
    }
}