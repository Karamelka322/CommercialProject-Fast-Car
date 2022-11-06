using System;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Logic.Item;
using CodeBase.Logic.Player;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorPower : MonoBehaviour, IReplayHandler, IAffectPlayerDefeat
    {
        [SerializeField] 
        private GeneratorHook _hook;

        private float _power = _startValuePower;
        private float _stepPower = _speedChangeStepPower;
        
        private const int _startValuePower = 100;
        private const float _speedChangeStepPower = 0.00001f;

        private IUpdateService _updateService;
        private ILevelMediator _mediator;

        public event Action OnDefeat;

        [Inject]
        public void Construct(IUpdateService updateService, ILevelMediator mediator)
        {
            _mediator = mediator;
            _updateService = updateService;
        }

        private void Start()
        {
            _updateService.OnUpdate += OnUpdate;
            _hook.EnergyCapture += EnergyCapture;
        }

        private void OnDestroy()
        {
            _updateService.OnUpdate -= OnUpdate;
            _hook.EnergyCapture -= EnergyCapture;
        }

        private void OnUpdate()
        {
            if(_power == 0)
                return;

            ReducePower();
            ReduceStepPower();
        }

        private void EnergyCapture(Energy energy) =>
            AddPower(energy.Power);

        private void AddPower(float value) => 
            _power = Mathf.Clamp(_power + value, 0, _startValuePower);

        private void ReducePower()
        {
            _power = Mathf.Clamp(_power - _stepPower, 0, _startValuePower);

            _mediator.UpdateGeneratorBar(_power / _startValuePower);
            
            if(_power == 0) 
                OnDefeat?.Invoke();
        }

        private void ReduceStepPower() => 
            _stepPower += _speedChangeStepPower;

        public void OnReplay()
        {
            _power = _startValuePower;
            _stepPower = _speedChangeStepPower;
            
            _mediator.UpdateGeneratorBar(1);
        }
    }
}