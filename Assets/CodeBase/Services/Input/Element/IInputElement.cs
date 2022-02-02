using UnityEngine;

namespace CodeBase.Services.Input.Element
{
    public interface IInputElement
    {
        string Id { get; }
    }

    public interface IInputBoolValue
    {
        bool Value { get; }
    }

    public interface IInputVector2Value
    {
        Vector2 Value { get; }
    }

    public interface IButtonInputElement : IInputElement, IInputBoolValue
    {
        
    }
    
    public interface IAreaInputElement : IInputElement, IInputBoolValue
    {
        
    }
    
    public interface IJoystickInputElement : IInputElement, IInputVector2Value
    {
        
    }
}