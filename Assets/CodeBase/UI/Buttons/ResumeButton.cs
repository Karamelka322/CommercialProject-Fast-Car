using UnityEngine;

namespace CodeBase.UI.Buttons
{
    public class ResumeButton : UIButton
    {
        [SerializeField] 
        private GameObject _window;
        
        protected override void OnClickButton()
        {
            Destroy(_window);
            Time.timeScale = 1;
        }
    }
}