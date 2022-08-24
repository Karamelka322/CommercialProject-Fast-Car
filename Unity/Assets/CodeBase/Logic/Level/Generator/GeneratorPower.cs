using System;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Logic.Item;
using CodeBase.Logic.Player;
using CodeBase.Services.Defeat;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorPower : MonoBehaviour, IReplayHandler, IAffectPlayerDefeat, IPlayerDefeatHandler, IPlayerVictoryHandler
    {
        [SerializeField] 
        private GeneratorHook _hook;

        private float _power;
        private int _startValuePower;
        private float _powerSpeedChange;
        private bool _changePower = true;

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
            _hook.OnCapsuleLift += OnCapsuleLift;
        }

        private void OnDestroy()
        {
            _updateService.OnUpdate -= OnUpdate;
            _hook.OnCapsuleLift -= OnCapsuleLift;
        }

        private void OnUpdate()
        {
            if(_power == 0 || _changePower == false)
                return;

            ReducePower(Time.deltaTime * _powerSpeedChange);
        }

        private void OnCapsuleLift(Capsule capsule) =>
            AddPower(capsule.Power);

        private void AddPower(float value) => 
            _power = Mathf.Clamp(_power + value, 0, _startValuePower);

        private void ReducePower(float value)
        {
            _power = Mathf.Clamp(_power - value, 0, _startValuePower);

            _mediator.UpdateGeneratorBar(_power / _startValuePower);
            
            if(_power == 0) 
                OnDefeat?.Invoke();
        }
        
        public void OnReplay()
        {
            _power = _startValuePower;
            _changePower = true;
        }

        void IPlayerDefeatHandler.OnDefeat() => 
            _changePower = false;

        public void OnVictory() => 
            _changePower = false;
    }
}