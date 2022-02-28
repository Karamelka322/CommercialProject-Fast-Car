using CodeBase.Data.Perseistent;
using CodeBase.Logic.Level.Generator;
using CodeBase.Services.Data.ReaderWriter;

namespace CodeBase.UI
{
    public class GeneratorPowerBar : UIBar, IReadData
    {
        private GeneratorPower _generatorPower;
        
        public void Construct(GeneratorPower generatorPower) => 
            _generatorPower = generatorPower;

        private void Start() => 
            _generatorPower.OnChangePower += OnChangePower;

        private void OnDestroy() => 
            _generatorPower.OnChangePower -= OnChangePower;

        private void OnChangePower(float power) => 
            Fill = power / GeneratorSessionData.MaxPower;

        public void ReadData(PlayerPersistentData persistentData) => 
            Fill = persistentData.SessionData.LevelData.GeneratorData.Power / GeneratorSessionData.MaxPower;
    }
}