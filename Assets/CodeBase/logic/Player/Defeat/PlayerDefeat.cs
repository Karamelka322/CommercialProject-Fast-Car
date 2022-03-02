using CodeBase.Services.Factories.UI;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    [RequireComponent(typeof(IAffectPlayerDefeat))]
    public class PlayerDefeat : MonoBehaviour, IReplayHandler
    {
        private IUIFactory _uiFactory;
        private IAffectPlayerDefeat _handler;

        private void Awake() => 
            _handler = GetComponent<IAffectPlayerDefeat>();

        public void Construct(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        private void Start() => 
            _handler.OnDefeat += OnDefeat;

        private void OnDestroy() => 
            _handler.OnDefeat -= OnDefeat;

        private void OnDefeat()
        {
            _handler.OnDefeat -= OnDefeat;
            _uiFactory.LoadDefeatWindow();
        }

        public void OnReplay() => 
            _handler.OnDefeat += OnDefeat;
    }
}