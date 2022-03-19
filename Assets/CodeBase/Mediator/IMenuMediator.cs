using CodeBase.Data.Static.Player;
using CodeBase.Logic.Menu;
using CodeBase.Scene.Menu;

namespace CodeBase.Mediator
{
    public interface IMenuMediator
    {
        MenuUIViewer MenuUIViewer { get; }
        MenuAnimator MenuAnimator { get; }
        Garage Garage { get; }
        void ChangePlayerCar(PlayerTypeId playerTypeId);
        void ChangeMenuState(MenuState state);
        void SkipIntro();
        void RebindAnimator();
    }
}