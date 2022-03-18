using System.Collections.Generic;
using CodeBase.Extension;
using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Services.Defeat
{
    public class DefeatService : IDefeatService
    {
        private readonly List<IAffectPlayerDefeat> _influential = new List<IAffectPlayerDefeat>();
        private readonly List<IPlayerDefeatHandler> _handlers = new List<IPlayerDefeatHandler>();

        public bool IsDefeat { get; private set; }

        public void Register(GameObject obj)
        {
            foreach (IAffectPlayerDefeat influential in obj.GetComponentsInChildren<IAffectPlayerDefeat>())
            {
                influential.OnDefeat += OnDefeat;
                _influential.Add(influential);
            }

            foreach (IPlayerDefeatHandler handler in obj.GetComponentsInChildren<IPlayerDefeatHandler>())
                _handlers.Add(handler);
        }

        public void Clenup()
        {
            for (int i = 0; i < _influential.Count; i++) 
                _influential[i].OnDefeat -= OnDefeat;

            _handlers.Clear();
            _influential.Clear();
        }

        public void SetDefeat(bool isDefeat)
        {
            if (IsDefeat != isDefeat && isDefeat) 
                InformHandlers();
            
            IsDefeat = isDefeat;
        }

        private void OnDefeat()
        {
            if(IsDefeat == false)
                SetDefeat(true);
        }

        private void InformHandlers()
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                if(_handlers[i].IsNullHandler() == false)
                {
                    _handlers[i].OnDefeat();
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