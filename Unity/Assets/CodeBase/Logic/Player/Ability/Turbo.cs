using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Services.Update;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player.Ability
{
    public class Turbo : MonoBehaviour, IAbility
    {
        [SerializeField] 
        private Car.Car _car;
        
        [Space, SerializeField] 
        private float _actionTime;

        [SerializeField] 
        private float _driftTime;
        
        [SerializeField] 
        private int _acceleration;

        [ReadOnly, Space, ShowInInspector]
        private bool _isUsed;

        [ReadOnly, Space, ShowInInspector]
        private float _nowDriftTime;

        [ReadOnly, ShowInInspector]
        private float _nowActionTime;

        private IUpdateService _updateService;
        private ILevelMediator _mediator;

        [Inject]
        private void Construct(IUpdateService updateService, ILevelMediator mediator)
        {
            _mediator = mediator;
            _updateService = updateService;
        }

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        public void Enable()
        {
            _isUsed = true;
            _nowActionTime = 0;
            _car.Property.MaxSpeed += _acceleration;
        }

        public void Disable()
        {
            _isUsed = false;
            _nowDriftTime = 0;
            _car.Property.MaxSpeed -= _acceleration;
        }

        private void OnUpdate()
        {
            if (_car.Property.UseDrift && _nowDriftTime < _driftTime && _isUsed == false)
            {
                _nowDriftTime += Time.deltaTime;
                _mediator.UpdateAbilityBar(_nowDriftTime / _driftTime);
            }

            if (_isUsed)
            {
                _nowActionTime += Time.deltaTime;

                if (_nowActionTime >= _actionTime) 
                    Disable();
            }
        }
    }
}