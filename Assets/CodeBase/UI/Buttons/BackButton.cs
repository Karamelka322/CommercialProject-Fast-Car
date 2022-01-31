using System;

namespace CodeBase.UI.Buttons
{
    public class BackButton : UIButton
    {
        public event Action _backEvent;
        
        public void Construct(Action backEvent) => 
            _backEvent = backEvent;

        protected override void OnClickButton() => 
            _backEvent?.Invoke();
    }
}