using CodeBase.Data.Perseistent;
using CodeBase.Logic.Item;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Replay;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorPower : MonoBehaviour, ISingleReadData, IStreamingWriteData, IReplayHandler
    {
        [SerializeField] 
        private GeneratorHook _hook;

        private float _power;
        private int _startValuePower;
        private float _powerSpeedChange;

        private IUpdateService _updateService;

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
            if(_power == 0)
                return;

            ReducePower(Time.deltaTime * _powerSpeedChange);
        }

        private void OnCapsuleLift(Capsule capsule) =>
            AddPower(capsule.Power);

        private void AddPower(float value) => 
            _power = Mathf.Clamp(_power + value, 0, _startValuePower);

        private void ReducePower(float value) => 
            _power = Mathf.Clamp(_power - value, 0, _startValuePower);

        public void SingleReadData(PlayerPersistentData persistentData)
        {
            _startValuePower = persistentData.SessionData.LevelData.CurrentLevelConfig.Generator.StartValuePower;
            _powerSpeedChange = persistentData.SessionData.LevelData.CurrentLevelConfig.Generator.PowerChangeSpeed;
            _power = _startValuePower;
        }

        public void StreamingWriteData(PlayerPersistentData persistentData) => 
            persistentData.SessionData.LevelData.GeneratorData.Power = _power;

        public void OnReplay() => 
            _power = _startValuePower;
    }
}