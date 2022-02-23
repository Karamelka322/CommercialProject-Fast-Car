using CodeBase.Services.Factories.UI;
using CodeBase.Services.Pause;

namespace CodeBase.UI.Buttons
{
    public class PauseButton : UIButton
    {
        private IPauseService _pauseService;
        private IUIFactory _uiFactory;

        public void Construct(IUIFactory uiFactory, IPauseService pauseService)
        {
            _uiFactory = uiFactory;
            _pauseService = pauseService;
        }

        protected override void OnClickButton()
        {
            _pauseService.SetPause(true);
            _uiFactory.LoadPauseWindow();
        }
    }
}