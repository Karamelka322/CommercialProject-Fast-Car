using System;
using System.Collections;
using CodeBase.Data.Static.Level;
using CodeBase.Logic.Item;
using UnityEngine;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorPower : MonoBehaviour
    {
        private const float MaxPower = 100;

        [SerializeField] 
        private GeneratorHook _hook;

        public event Action<float> Change;
        public event Action IsZero;

        private float _speedChange;
        private float _power = MaxPower;

        private float Power
        {
            get => _power;
            
            set
            {
                _power = value;
                Change?.Invoke(value);
            }
        }

        public void Construct(LevelStaticData levelStaticData) =>
         _speedChange = levelStaticData.PowerChangeSpeed;

        public void Start() => 
            StartCoroutine(ChangePower());

        private IEnumerator ChangePower()
        {
            while (_power > 0)
            {
                Power -= Time.deltaTime * _speedChange;
                yield return null;
            }
            
            IsZero?.Invoke();
        }

        private void OnEnable() => 
            _hook.OnCapsuleLift += OnCapsuleLift;

        private void OnDisable() => 
            _hook.OnCapsuleLift -= OnCapsuleLift;

        private void OnCapsuleLift(Capsule capsule) => 
            Power = Mathf.Clamp(Power + capsule.Power, 0, MaxPower);
    }
}