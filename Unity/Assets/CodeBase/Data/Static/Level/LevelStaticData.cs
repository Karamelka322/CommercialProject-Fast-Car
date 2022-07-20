using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "Level", order = 51)]
    public class LevelStaticData : ScriptableObject
    {
        [FoldoutGroup("Level"), HideLabel]
        public LevelConfig Level;

        [FoldoutGroup("Spawn"), HideLabel] 
        public SpawnConfig Spawn;
        
        [FoldoutGroup("Reward"), HideLabel]
        public RewardConfig Reward;
        
        public LevelTypeId Type => (LevelTypeId)Enum.Parse(typeof(LevelTypeId), name.Substring(0, 7));
        public string SceneName => name;
    }
}