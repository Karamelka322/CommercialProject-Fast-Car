using System;

namespace CodeBase.UI.Buttons
{
    public class SkipButton : UIButton
    {
        public event Action Click;
        
        protected override void OnClickButton() => 
            Click?.Invoke();
    }
}