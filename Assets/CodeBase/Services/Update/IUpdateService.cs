using System;

namespace CodeBase.Services.Update
{
    public interface IUpdateService : IService
    {
        event Action OnUpdate;
        void Enable();
        void Disable();
        event Action OnFixedUpdate;
    }
}