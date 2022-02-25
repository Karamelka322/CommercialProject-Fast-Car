using System;
using CodeBase.Data.Perseistent;
using CodeBase.Logic.Item;
using CodeBase.Services.Data.ReaderWriter;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorPower : MonoBehaviour, IReadData, IWriteData, IReplayHandler
    {
        [SerializeField] 
        private GeneratorHook _hook;

        private IUpdateService _updateService;

        private float _startPower;
        private float _startSpeed;
        
        private float _speed;
        private float _power;

        public event Action<float> OnChangePower;
        
        public void Construct(IUpdateService updateService) => 
            _updateService = updateService;

        private void OnUpdate()
        {
            if(_power == 0)
                return;

            ReducePower(Time.deltaTime * _speed);
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

        private void OnCapsuleLift(Capsule capsule) =>
            AddPower(capsule.Power);

        private void AddPower(float value)
        {
            _power = Mathf.Clamp(_power + value, GeneratorSessionData.MinPower, GeneratorSessionData.MaxPower);
            OnChangePower?.Invoke(_power);
        }

        private void ReducePower(float value)
        {
            _power = Mathf.Clamp(_power - value, GeneratorSessionData.MinPower, GeneratorSessionData.MaxPower);
            OnChangePower?.Invoke(_power);
        }

        public void ReadData(PlayerPersistentData persistentData)
        {
            _startPower = persistentData.SessionData.GeneratorData.Power;
            _startSpeed = persistentData.SessionData.GeneratorData.PowerSpeedChange;

            _power = _startPower;
            _speed = _startSpeed;
        }

        public void WriteData(PlayerPersistentData persistentData)
        {
            persistentData.SessionData.GeneratorData.Power = _power;
            persistentData.SessionData.GeneratorData.PowerSpeedChange = _speed;
        }

        public void OnReplay()
        {
            _power = _startPower;
            _speed = _startSpeed;
            
            OnChangePower?.Invoke(_power);
        }
    }
}