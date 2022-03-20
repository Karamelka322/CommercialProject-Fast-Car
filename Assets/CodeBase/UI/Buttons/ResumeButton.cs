using CodeBase.Services.Pause;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class ResumeButton : UIButton
    {
        [SerializeField] 
        private GameObject _window;

        private IPauseService _pauseService;

        [Inject]
        public void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }

        protected override void OnClickButton()
        {
            _pauseService.SetPause(false);
            Destroy(_window);
        }
    }
}