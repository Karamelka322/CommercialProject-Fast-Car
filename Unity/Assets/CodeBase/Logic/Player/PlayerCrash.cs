using CodeBase.Infrastructure.Mediator.Level;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCrash : MonoBehaviour
    {
        private const string Ground = "Ground";

        public bool Crash { get; private set; }
        
        private ILevelMediator _mediator;

        [Inject]
        private void Construct(ILevelMediator mediator)
        {
            _mediator = mediator;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer(Ground))
            {
                Crash = true;
                _mediator.EnableMoveBackwardsButton();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer(Ground))
            {
                Crash = false;
                _mediator.DisableMoveBackwardsButton();
            }
        }
    }
}