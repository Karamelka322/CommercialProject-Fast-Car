using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReadWrite;

namespace CodeBase.UI
{
    public class GeneratorPowerBar : UIBar, IStreamingReadData, ISingleReadData
    {
        private int _startValuePower;
        
        public void SingleReadData(PlayerPersistentData persistentData) => 
            _startValuePower = persistentData.SessionData.LevelData.CurrentLevelConfig.Generator.StartValuePower;

        public void StreamingReadData(PlayerPersistentData persistentData) => 
            Fill = persistentData.SessionData.LevelData.GeneratorData.Power / _startValuePower;
    }
}