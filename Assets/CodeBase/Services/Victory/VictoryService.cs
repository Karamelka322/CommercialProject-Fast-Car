using System.Collections.Generic;
using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.Services.Victory
{
    public class VictoryService : IVictoryService
    {
        private readonly List<IAffectPlayerVictory> _influential = new List<IAffectPlayerVictory>();
        private readonly List<IPlayerVictoryHandler> _handlers = new List<IPlayerVictoryHandler>();

        public bool IsVictory { get; private set; }

        public void Register(GameObject obj)
        {
            foreach (IAffectPlayerVictory influential in obj.GetComponentsInChildren<IAffectPlayerVictory>())
            {
                influential.OnVictory += OnVictory;
                _influential.Add(influential);
            }

            foreach (IPlayerVictoryHandler handler in obj.GetComponentsInChildren<IPlayerVictoryHandler>())
                _handlers.Add(handler);
        }

        public void Clenup()
        {
            for (int i = 0; i < _influential.Count; i++) 
                _influential[i].OnVictory -= OnVictory;

            _handlers.Clear();
            _influential.Clear();
        }

        public void SetVictory(bool isDefeat)
        {
            if (IsVictory != isDefeat && isDefeat) 
                InformHandlers();
            
            IsVictory = isDefeat;
        }

        private void OnVictory()
        {
            if(IsVictory == false)
                SetVictory(true);
        }

        private void InformHandlers()
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                if(_handlers[i].IsNullHandler() == false)
                {
                    _handlers[i].OnVictory();
                }
                else
                {
                    _handlers.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}