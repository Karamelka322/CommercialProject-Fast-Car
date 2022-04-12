using System;
using UnityEngine.UI;

namespace CodeBase.UI.Buttons
{
    public class ButtonWrapper : Button
    {
        public event Action OnClick;

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(OnClickButton);           
        }

        public void Enable()
        {
            if(!interactable)
                interactable = true;
        }

        public void Disable()
        {
            if(interactable)
                interactable = false;
        }

        private void OnClickButton() => 
            OnClick?.Invoke();
    }
}