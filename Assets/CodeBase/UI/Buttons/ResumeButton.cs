using CodeBase.Services.Pause;
using UnityEngine;

namespace CodeBase.UI.Buttons
{
    public class ResumeButton : UIButton
    {
        [SerializeField] 
        private GameObject _window;

        private IPauseService _pauseService;

        public void Construct(IPauseService pauseService) => 
            _pauseService = pauseService;

        protected override void OnClickButton()
        {
            _pauseService.SetPause(false);
            Destroy(_window);
        }
    }
}