using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Scene.Menu;

namespace CodeBase.UI.Buttons
{
    public class PlayButton : UIButton
    {
        private IGameStateMachine _stateMachine;
        private MenuAnimator _animator;

        public void Construct(IGameStateMachine stateMachine, MenuAnimator animator)
        {
            _stateMachine = stateMachine;
            _animator = animator;
        }

        protected override void OnClickButton()
        {
            _stateMachine.Enter<LoadLevelState>();
            _animator.PlayCloseMenu();
        }
    }
}