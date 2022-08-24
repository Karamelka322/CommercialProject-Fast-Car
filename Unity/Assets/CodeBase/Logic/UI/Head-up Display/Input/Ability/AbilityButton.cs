using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.UI.Buttons;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Input
{
    public class AbilityButton : UIButton
    {
        [SerializeField] 
        private AbilityBar _abilityBar;

        private ILevelMediator _mediator;

        [Inject]
        private void Construct(ILevelMediator mediator)
        {
            _mediator = mediator;
        }

        protected override void OnClickButton()
        {
            if (_abilityBar.Value == 1)
            {
                _mediator.EnablePlayerAbility();
                _abilityBar.Value = 0;
            }
        }
    }
}