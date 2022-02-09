using CodeBase.Data.Perseistent;

namespace CodeBase.UI
{
    public class GeneratorPowerBar : UIBar
    {
        private GeneratorSessionData _generatorSessionData;

        public void Construct(GeneratorSessionData generatorSessionData) =>
            _generatorSessionData = generatorSessionData;
        
        private void Start() => 
            _generatorSessionData.ChangePower += OnChangePower;

        private void OnDestroy() => 
            _generatorSessionData.ChangePower -= OnChangePower;

        private void OnChangePower(float power) => 
            Fill = power / GeneratorSessionData.MaxPower;
    }
}