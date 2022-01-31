using CodeBase.Scene.Menu;

namespace CodeBase.UI.Buttons
{
    public class SkipButton : UIButton
    {
        private MenuAnimator _menuAnimator;

        public void Construct(MenuAnimator menuAnimator)
        {
            _menuAnimator = menuAnimator;
            _menuAnimator.StartPlayIdleMenu += DestroySkipButton;
        }

        protected override void OnClickButton()
        {
            _menuAnimator.PlayIdleMenu();
            DestroySkipButton();
        }

        private void DestroySkipButton()
        {
            _menuAnimator.StartPlayIdleMenu -= DestroySkipButton;
            Destroy(gameObject);
        }
    }
}