using System;
using CodeBase.Data.Perseistent;
using CodeBase.Logic.Item;
using CodeBase.Logic.Player;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorPower : MonoBehaviour, ISingleReadData, IStreamingWriteData, IReplayHandler, IAffectPlayerDefeat, IPlayerDefeatHandler, IPlayerVictoryHandler
    {
        [SerializeField] 
        private GeneratorHook _hook;

        private float _power;
        private int _startValuePower;
        private float _powerSpeedChange;
        private bool _changePower = true;

        private IUpdateService _updateService;
        
        public event Action OnDefeat;

        public void Construct(IUpdateService updateService) => 
            _updateService = updateService;

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
            
            if(_power == 0) 
                OnDefeat?.Invoke();
        }

        public void SingleReadData(PlayerPersistentData persistentData)
        {
            _startValuePower = persistentData.SessionData.LevelData.CurrentLevelConfig.Generator.StartValuePower;
            _powerSpeedChange = persistentData.SessionData.LevelData.CurrentLevelConfig.Generator.PowerChangeSpeed;
            _power = _startValuePower;
        }

        public void StreamingWriteData(PlayerPersistentData persistentData) => 
            persistentData.SessionData.LevelData.GeneratorData.Power = _power;

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