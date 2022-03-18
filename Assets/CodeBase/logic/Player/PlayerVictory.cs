using System;
using CodeBase.Data.Perseistent;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Extension;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Reward;
using CodeBase.Services.Victory;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerVictory : MonoBehaviour, IAffectPlayerVictory, IStreamingReadData, ISingleWriteData
    {
        private IRewardService _rewardService;
        private IUIFactory _uiFactory;
        
        private bool _isVictory;

        public event Action OnVictory;

        public void Construct(IUIFactory uiFactory, IRewardService rewardService)
        {
            _rewardService = rewardService;
            _uiFactory = uiFactory;
        }

        public void StreamingReadData(PlayerPersistentData persistentData)
        {
            if (IsVictory(persistentData))
            {
                _isVictory = true;
                _uiFactory.LoadVictoryWindow();
                OnVictory?.Invoke();
            }
        }

        public void SingleWriteData(PlayerPersistentData persistentData)
        {
            if (!_isVictory)
                return;

            TrySetPassedLevel(ref persistentData.ProgressData);
            SwitchLevel(ref persistentData.ProgressData.CurrentLevel);
            
            _rewardService.TakeRewardToCompletingLevel();
            persistentData.ProgressData.CurrentPlayer = PlayerTypeId.Falcon;
        }

        private static void TrySetPassedLevel(ref ProgressPersistentData progressData) => 
            progressData.Levels.TrySetValueToKey(progressData.CurrentLevel, true);

        private static void SwitchLevel(ref LevelTypeId currentLevel) => 
            currentLevel = (int)currentLevel < Enum.GetNames(typeof(LevelTypeId)).Length - 1 ? currentLevel + 1 : currentLevel;

        private bool IsVictory(PlayerPersistentData persistentData) => 
            _isVictory == false && persistentData.SessionData.LevelData.CurrentLevelConfig.Level.VictoryTime < persistentData.SessionData.StopwatchTime;
    }
}