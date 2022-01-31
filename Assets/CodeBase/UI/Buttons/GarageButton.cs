using CodeBase.Scene.Menu;

namespace CodeBase.UI.Buttons
{
    public class GarageButton : UIButton
    {
        private MenuAnimator _menuAnimator;
        
        public void Construct(MenuAnimator menuAnimator) => 
            _menuAnimator = menuAnimator;

        protected override void OnClickButton() => 
            _menuAnimator.PlayOpenGarage();
    }
}