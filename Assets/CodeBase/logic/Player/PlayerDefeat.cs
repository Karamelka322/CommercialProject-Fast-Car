using CodeBase.Services.Defeat;
using CodeBase.Services.Factories.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerDefeat : MonoBehaviour, IPlayerDefeatHandler
    {
        private IUIFactory _uiFactory;

        [Inject]
        public void Construct(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void OnDefeat() => 
            _uiFactory.LoadDefeatWindow();
    }
}