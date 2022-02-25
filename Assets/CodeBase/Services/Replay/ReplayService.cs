using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.Replay
{
    public class ReplayService : IReplayService
    {
        private readonly List<IReplayHandler> _handlers = new List<IReplayHandler>();

        public void Register(GameObject gameObject)
        {
            foreach (IReplayHandler handler in gameObject.GetComponentsInParent<IReplayHandler>())
                _handlers.Add(handler);
        }
        
        public void InformHandlers()
        {
            Cleaning();

            for (int i = 0; i < _handlers.Count; i++) 
                _handlers[i].OnReplay();
        }

        public void Clenup() => 
            _handlers.Clear();

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