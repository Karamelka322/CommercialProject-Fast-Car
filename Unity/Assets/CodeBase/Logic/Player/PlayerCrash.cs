using CodeBase.Infrastructure.Mediator.Level;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCrash : CarCrash
    {
        private ILevelMediator _mediator;

        [Inject]
        private void Construct(ILevelMediator mediator)
        {
            _mediator = mediator;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if(CheckCrash(other))
            {
                Crash = true;
                _mediator.EnableMoveBackwardsButton();

                _mediator.GetBackwardsButton().Item1.Disabled += DisableCrash;
                _mediator.GetBackwardsButton().Item2.Disabled += DisableCrash;
            }
        }

        private void DisableCrash()
        {
            Crash = false;
            _mediator.DisableMoveBackwardsButton();
            
            _mediator.GetBackwardsButton().Item1.Disabled -= DisableCrash;
            _mediator.GetBackwardsButton().Item2.Disabled -= DisableCrash;
        }

        private bool CheckCrash(Collider other) => 
            other.gameObject.layer == LayerMask.NameToLayer(Ground);
    }
}