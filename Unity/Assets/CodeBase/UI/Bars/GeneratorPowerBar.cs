using CodeBase.Data.Perseistent;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Replay;

namespace CodeBase.UI
{
    public class GeneratorPowerBar : UIBar, IStreamingReadData, ISingleReadData, IReplayHandler
    {
        private int _startValuePower;
        
        public void SingleReadData(PlayerPersistentData persistentData) => 
            _startValuePower = persistentData.SessionData.LevelData.CurrentLevelConfig.Spawn.Generator.StartValuePower;

        public void StreamingReadData(PlayerPersistentData persistentData) => 
            Fill = persistentData.SessionData.LevelData.GeneratorData.Power / _startValuePower;

        public void OnReplay() => 
            Fill = 1;
    }
}