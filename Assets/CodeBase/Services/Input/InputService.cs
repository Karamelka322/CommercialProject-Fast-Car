using System;
using System.Collections.Generic;
using CodeBase.Services.Input.Element;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        private Dictionary<string, IInputBoolValue> _buttons { get; } = new Dictionary<string, IInputBoolValue>();
        private Dictionary<string, IInputBoolValue> _areas { get; } = new Dictionary<string, IInputBoolValue>();
        private Dictionary<string, IInputVector2Value> _joysticks { get; } = new Dictionary<string, IInputVector2Value>();

        private IInputModule Module { get; } = DefineModule();

        public Vector2 Axis => Module.Axis;

        public void RegisterInput(InputTypeId typeId, GameObject gameObject)
        {
            Module.CurrentInputVariant = GetCore(typeId);

            foreach (IButtonInputElement inputElement in gameObject.GetComponentsInChildren<IButtonInputElement>())
                _buttons.Add(inputElement.Id, inputElement);
            
            foreach (IAreaInputElement inputElement in gameObject.GetComponentsInChildren<IAreaInputElement>())
                _areas.Add(inputElement.Id, inputElement);
            
            foreach (IJoystickInputElement inputElement in gameObject.GetComponentsInChildren<IJoystickInputElement>())
                _joysticks.Add(inputElement.Id, inputElement);
        }

        public bool GetButton(string id) => 
            _buttons.TryGetValue(id, out IInputBoolValue input) && input.Value;

        public bool GetArea(string id) => 
            _areas.TryGetValue(id, out IInputBoolValue input) && input.Value;

        public Vector2 GetJoystick(string id) => 
            _joysticks.TryGetValue(id, out IInputVector2Value input) ? input.Value : Vector2.zero;

        public void Clenup()
        {
            _buttons.Clear();
            _areas.Clear();
            _joysticks.Clear();
        }
        
        private static IInputModule DefineModule()
        {
            if (Application.isEditor)
                return new EditorInputModule();

            if (Application.isMobilePlatform)
                return new MobileInputModule();

            throw new PlatformNotSupportedException();
        }

        private IInputVariant GetCore(InputTypeId typeId)
        {
            return typeId switch
            {
                InputTypeId.Keyboards => new KeyboardsInputVariant(),
                InputTypeId.Joystick => new JoystickInputVariant(this),
                InputTypeId.Buttons => new ButtonsInputVariant(this),
                InputTypeId.Areas => new AreasInputVariant(this),
                _ => throw new ArgumentOutOfRangeException(nameof(typeId), typeId, null)
            };
        }
    }
}