using System.Collections.Generic;
using CodeBase.Extension;
using CodeBase.Infrastructure;
using CodeBase.Services.Update;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Services.Pause
{
    [UsedImplicitly]
    public class PauseService : IPauseService
    {
        private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();

        public float PauseTime { get; private set; }
        public bool IsPause { get; private set; }
        
        private readonly IUpdateService _updateService;
        private readonly IUpdatable _updatable;

        public PauseService(IUpdateService updateService, IUpdatable updatable)
        {
            _updatable = updatable;
            _updateService = updateService;
        }

        public void SetPause(bool isPause)
        {
            if(isPause == IsPause)
                return;

            IsPause = isPause;

            EnableDisableUpdateService();
            InformHandlers();
            EnableDisableStopwatch();
        }

        private void EnableDisableUpdateService()
        {
            if (IsPause)
                _updateService.Disable();
            else
                _updateService.Enable();
        }

        private void EnableDisableStopwatch()
        {
            if (IsPause)
                _updatable.OnUpdate += Stopwatch;
            else
            {
                PauseTime = 0;
                _updatable.OnUpdate -= Stopwatch;
            }
        }

        private void Stopwatch() => 
            PauseTime += Time.deltaTime;

        public void Register(GameObject gameObject)
        {
            foreach (IPauseHandler handler in gameObject.GetComponentsInChildren<IPauseHandler>())
                _handlers.Add(handler);
        }

        public void CleanUp() => 
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