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
    public class PlayerVictory : MonoBehaviour, IAffectPlayerVictory, IStreamingReadData, ISingleWriteData
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
                _uiFactory.LoadVictoryWindow();
                OnVictory?.Invoke();
            }
        }

        public void SingleWriteData(PlayerPersistentData persistentData)
        {
            if (!_isVictory)
                return;

            // KeyValue<LevelTypeId,bool>[] levels = persistentData.ProgressData.Levels;
            //
            // LevelTypeId typeId = LevelTypeId.Level_2;
            //
            // typeId++;
        }

        private bool IsVictory(PlayerPersistentData persistentData) => 
            _isVictory == false && persistentData.SessionData.LevelData.CurrentLevelConfig.Level.VictoryTime < persistentData.SessionData.StopwatchTime;
    }
}