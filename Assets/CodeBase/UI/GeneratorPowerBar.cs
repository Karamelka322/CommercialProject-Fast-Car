using CodeBase.Logic.Level.Generator;

namespace CodeBase.UI
{
    public class GeneratorPowerBar : UIBar
    {
        private const float MaxPower = 100;
        
        private GeneratorPower _generatorPower;

        public void Construct(GeneratorPower generatorPower) => 
            _generatorPower = generatorPower;

        private void Start() => 
            _generatorPower.Change += OnChangePower;

        private void OnDestroy() => 
            _generatorPower.Change -= OnChangePower;

        private void OnChangePower(float power) => 
            Fill = power / MaxPower;
    }
}