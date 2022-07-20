using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.Logic.Player;
using CodeBase.Services.Random;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class LevelConfig
    {
        [GUIColor(0.8f, 0.8f, 0), MinValue(1), MaxValue(999)]
        public int VictoryTime;

        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10), ReadOnly, GUIColor(1f, 1f, 0), InfoBox("Empty", InfoMessageType.Error,"CheckPlayerSpawnPoints")]
        public PointData[] PlayerSpawnPoints;

#if UNITY_EDITOR

        [UsedImplicitly]
        private bool CheckPlayerSpawnPoints() => 
            PlayerSpawnPoints == null || PlayerSpawnPoints.Length == 0;
        
#endif
    }
}