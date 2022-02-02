using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        void RegisterInput(InputTypeId typeId, GameObject gameObject);
        bool GetButton(string id);
        bool GetArea(string id);
        Vector2 GetJoystick(string id);
        void Clenup();
    }
}