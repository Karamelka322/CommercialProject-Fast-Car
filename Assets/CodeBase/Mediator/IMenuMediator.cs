using CodeBase.Data.Static.Player;
using CodeBase.Logic.Menu;
using CodeBase.Scene.Menu;

namespace CodeBase.Mediator
{
    public interface IMenuMediator
    {
        MenuAnimator MenuAnimator { get; }
        void ChangePlayerCar(PlayerTypeId playerTypeId);
        void ChangeMenuState(MenuState state);
        void SkipIntro();
        void RebindAnimator();
    }
}