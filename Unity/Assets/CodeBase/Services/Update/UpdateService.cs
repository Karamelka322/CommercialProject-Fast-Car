using System;
using CodeBase.Infrastructure;
using CodeBase.Services.Window;

namespace CodeBase.Services.Update
{
    public class UpdateService : IUpdateService
    {
        private readonly IUpdatable _updatable;

        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        
        public UpdateService(IUpdatable updatable)
        {
            _updatable = updatable;
        }

        public void Enable()
        {
            _updatable.OnUpdate += Update;
            _updatable.OnFixedUpdate += FixedUpdate;
        }

        public void Disable()
        {
            _updatable.OnUpdate -= Update;
            _updatable.OnFixedUpdate -= FixedUpdate;
        }

        private void Update() => 
            OnUpdate?.Invoke();

        private void FixedUpdate() => 
            OnFixedUpdate?.Invoke();
    }
}