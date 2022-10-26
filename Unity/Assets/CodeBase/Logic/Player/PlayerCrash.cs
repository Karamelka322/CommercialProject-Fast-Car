using System;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Logic.Enemy;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCrash : CarCrash
    {
        [SerializeField] private Car _car;
        
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
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            if(CheckCrash(other))
            {
                Crash = false;
                _mediator.DisableMoveBackwardsButton();
            }
        }

        private bool CheckCrash(Collider other) => 
            other.gameObject.layer == LayerMask.NameToLayer(Ground) || other.TryGetComponent(out EnemyPrefab enemy);
    }
}