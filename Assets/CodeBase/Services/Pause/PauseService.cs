using System.Collections.Generic;
using CodeBase.Extension;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Services.Pause
{
    public class PauseService : IPauseService
    {
        private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();

        public bool IsPause { get; private set; }
        
        private readonly IUpdateService _updateService;

        public PauseService(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        public void SetPause(bool isPause)
        {
            if(isPause == IsPause)
                return;

            IsPause = isPause;

            EnableDisableUpdateService();
            InformHandlers();
        }

        private void EnableDisableUpdateService()
        {
            if (IsPause)
                _updateService.Disable();
            else
                _updateService.Enable();
        }

        public void Register(GameObject gameObject)
        {
            foreach (IPauseHandler handler in gameObject.GetComponents<IPauseHandler>()) 
                _handlers.Add(handler);
        }

        public void Clenup() => 
            _handlers.Clear();

        private void InformHandlers()
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                if (_handlers[i].IsNullHandler() == false)
                {
                    Inform(_handlers[i]);
                }
                else
                {
                    _handlers.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Inform(IPauseHandler handler)
        {
            if (IsPause)
                handler.OnEnabledPause();
            else
                handler.OnDisabledPause();
        }
    }
}