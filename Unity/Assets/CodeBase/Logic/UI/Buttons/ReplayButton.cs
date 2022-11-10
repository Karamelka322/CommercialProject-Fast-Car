using CodeBase.Infrastructure;
using CodeBase.Services.Pause;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class ReplayButton : UIButton
    {
        [SerializeField] 
        private GameObject _root;
        
        private IGameStateMachine _gameStateMachine;
        private IPauseService _pauseService;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _gameStateMachine = gameStateMachine;
        }

        protected override void OnClickButton()
        {
            _gameStateMachine.Enter<ReplayLevelState>();
            Destroy(_root);
        }
    }
}