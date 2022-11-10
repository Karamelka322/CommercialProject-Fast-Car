using System.Collections.Generic;
using CodeBase.Logic.Player;
using CodeBase.Services.Factories.UI;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Services.Defeat
{
    [UsedImplicitly]
    public class DefeatService : IDefeatService
    {
        private readonly List<IAffectPlayerDefeat> _influential = new List<IAffectPlayerDefeat>();
        
        private readonly IUIFactory _uiFactory;

        public bool IsDefeat { get; private set; }

        public DefeatService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public void Register(GameObject obj)
        {
            foreach (IAffectPlayerDefeat influential in obj.GetComponentsInChildren<IAffectPlayerDefeat>())
            {
                influential.OnDefeat += OnDefeat;
                _influential.Add(influential);
            }
        }

        public void CleanUp()
        {
            for (int i = 0; i < _influential.Count; i++) 
                _influential[i].OnDefeat -= OnDefeat;

            _influential.Clear();
        }

        public void SetDefeat(bool isDefeat) => 
            IsDefeat = isDefeat;

        private void OnDefeat()
        {
            if(IsDefeat == false)
            {
                SetDefeat(true);
                _uiFactory.LoadDefeatWindow();
            }
        }
    }
}