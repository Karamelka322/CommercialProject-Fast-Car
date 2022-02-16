using CodeBase.Services.Factories.UI;
using UnityEngine;

namespace CodeBase.UI.Buttons
{
    public class PauseButton : UIButton
    {
        private IUIFactory _uiFactory;

        public void Construct(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        protected override void OnClickButton()
        {
            _uiFactory.LoadPauseWindow();
            Time.timeScale = 0;
        }
    }
}