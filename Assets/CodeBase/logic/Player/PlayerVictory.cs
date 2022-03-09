using System;
using CodeBase.Data;
using CodeBase.Data.Perseistent;
using CodeBase.Data.Static.Level;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Victory;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerVictory : MonoBehaviour, IAffectPlayerVictory, IStreamingReadData
    {
        private IUIFactory _uiFactory;
        private bool _isVictory;

        public event Action OnVictory;

        public void Construct(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void StreamingReadData(PlayerPersistentData persistentData)
        {
            if (IsVictory(persistentData))
            {
                _isVictory = true;
                SetPassedInLastLevel(persistentData.ProgressData.Levels);

                _uiFactory.LoadVictoryWindow();
                OnVictory?.Invoke();
            }
        }

        private static void SetPassedInLastLevel(in KeyValue<LevelTypeId, bool>[] levels)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i].Value == false)
                {
                    levels[i].Value = true;
                    return;
                }
            }
        }

        private bool IsVictory(PlayerPersistentData persistentData) => 
            _isVictory == false && persistentData.SessionData.LevelData.CurrentLevelConfig.VictoryTime < persistentData.SessionData.StopwatchTime;
    }
}