using System.Collections.Generic;
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
            Cleaning();
            
            for (int i = 0; i < _handlers.Count; i++)
            {
                if (IsPause)
                {
                    _handlers[i].OnEnabledPause();
                }
                else
                {
                    _handlers[i].OnDisabledPause();
                }
            }
        }

        private void Cleaning()
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                try
                {
                    _handlers[i].name.Equals(_handlers[i].name);
                }
                catch (MissingReferenceException exception)
                {
                    _handlers.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}