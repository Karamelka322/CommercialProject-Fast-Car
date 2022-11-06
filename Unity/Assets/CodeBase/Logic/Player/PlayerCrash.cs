using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Logic.Enemy;
using CodeBase.Services.Tween;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCrash : CarCrash
    {
        [SerializeField] private Car _car;
        
        private ILevelMediator _mediator;
        private ITweenService _tweenService;

        [Inject]
        private void Construct(ILevelMediator mediator, ITweenService tweenService)
        {
            _tweenService = tweenService;
            _mediator = mediator;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if(CheckCrash(other))
            {
                Crash = true;
                _mediator.EnableMoveBackwardsButton();
                
                _tweenService.SingleTimer<PlayerCrash>(3f, () =>
                {
                    Crash = false;
                    _mediator.DisableMoveBackwardsButton();
                });
            }
        }

        private bool CheckCrash(Collider other) => 
            other.gameObject.layer == LayerMask.NameToLayer(Ground) || other.TryGetComponent(out EnemyPrefab enemy);
    }
}