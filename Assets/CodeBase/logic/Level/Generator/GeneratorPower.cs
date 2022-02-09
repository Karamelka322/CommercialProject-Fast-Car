using System.Collections;
using CodeBase.Data.Perseistent;
using CodeBase.Logic.Item;
using UnityEngine;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorPower : MonoBehaviour
    {
        [SerializeField] 
        private GeneratorHook _hook;

        private GeneratorSessionData _generatorSessionData;
        
        public void Construct(GeneratorSessionData generatorSessionData) =>
            _generatorSessionData = generatorSessionData;
        
        public void Start() => 
            StartCoroutine(ChangePower());

        private IEnumerator ChangePower()
        {
            while (_generatorSessionData.Power > 0)
            {
                _generatorSessionData.ReducePower(Time.deltaTime * _generatorSessionData.PowerSpeedChange);
                yield return null;
            }
        }

        private void OnEnable() => 
            _hook.OnCapsuleLift += OnCapsuleLift;

        private void OnDisable() => 
            _hook.OnCapsuleLift -= OnCapsuleLift;

        private void OnCapsuleLift(Capsule capsule) =>
            _generatorSessionData.AddPower(capsule.Power);
    }
}