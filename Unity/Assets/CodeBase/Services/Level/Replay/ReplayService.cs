using System.Collections.Generic;
using CodeBase.Extension;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Services.Replay
{
    [UsedImplicitly]
    public class ReplayService : IReplayService
    {
        private readonly List<IReplayHandler> _handlers = new List<IReplayHandler>();

        public void Register(GameObject gameObject)
        {
            foreach (IReplayHandler handler in gameObject.GetComponentsInChildren<IReplayHandler>())
                _handlers.Add(handler);
        }
        
        public void InformHandlers()
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                if(_handlers[i].IsNullHandler() == false)
                {
                    _handlers[i].OnReplay();
                }
                else
                {
                    _handlers.RemoveAt(i);
                    i--;
                }
            }
        }

        public void CleanUp() => 
            _handlers.Clear();
    }
}