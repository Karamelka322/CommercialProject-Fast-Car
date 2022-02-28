using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Player;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Data.Static.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "Level", order = 51)]
    public class LevelStaticData : ScriptableObject
    {
        [FoldoutGroup("Level"), GUIColor(0.8f, 0.8f, 0), ValidateInput("CheckLevelType", "Already in use!")]
        public LevelTypeId LevelType;

        [FoldoutGroup("Level"), GUIColor(0.8f, 0.8f, 0), ValidateInput("CheckSceneName", "It is not geometry scene name!")] 
        public string SceneName;

        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10), ReadOnly, FoldoutGroup("Level"), GUIColor(1f, 1f, 0), ValidateInput("CheckPlayerSpawnPoints", "Empty")]
        public List<Vector3> PlayerSpawnPoints;

        [FoldoutGroup("Using"), Toggle("UsingGenerator")]
        public GeneratorSpawnConfig Generator;

        [FoldoutGroup("Using"), Toggle("UsingCapsule")]
        public CapsuleSpawnConfig Capsule;

        [FoldoutGroup("Using"), Toggle("UsingEnemy")]
        public EnemySpawnConfig Enemy;

#if UNITY_EDITOR

        [UsedImplicitly, Button("Collect"), FoldoutGroup("Level"), GUIColor(0.5f, 0.7f, 1f)]
        private void FillLevelData()
        {
            FillSceneName();
            FillLevelTypeId();
            FillPlayerSpawnPoints();
        }
        
        private void FillSceneName() => 
            SceneName = SceneManager.GetActiveScene().name;

        private void FillLevelTypeId()
        {
            LevelStaticData[] levelStaticDatas = Resources.LoadAll<LevelStaticData>("StaticData/Level");
            List<string> names = Enum.GetNames(typeof(LevelTypeId)).ToList();
            
            for (int i = 0; i < names.Count; i++)
            {
                for (int j = 0; j < levelStaticDatas.Length; j++)
                {
                    if (levelStaticDatas[j] == this)
                        continue;
                    
                    names.Remove(levelStaticDatas[j].LevelType.ToString());
                }
            }

            string value = string.IsNullOrEmpty(names[names.Count - 1]) ? Enum.GetNames(typeof(LevelTypeId)).Last() : names[names.Count - 1];
            LevelType = (LevelTypeId)Enum.Parse(typeof(LevelTypeId), value);
        }
        
        private void FillPlayerSpawnPoints()
        {
            PlayerSpawnPoint[] generatorSpawnPoints = FindObjectsOfType<PlayerSpawnPoint>();
            
            PlayerSpawnPoints.Clear();
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                PlayerSpawnPoints.Add(generatorSpawnPoints[i].WorldPosition);
        }


        [UsedImplicitly]
        private bool CheckLevelType()
        {
            LevelStaticData[] levelStaticDatas = Resources.LoadAll<LevelStaticData>("StaticData/Level");

            for (int i = 0; i < levelStaticDatas.Length; i++)
            {
                if (levelStaticDatas[i] == this)
                    continue;

                if (levelStaticDatas[i].LevelType == LevelType)
                    return false;
            }

            return true;
        }

        [UsedImplicitly]
        private bool CheckSceneName() => 
            AssetDatabase.LoadAssetAtPath<SceneAsset>($"Assets/Scenes/Geometry/{SceneName}.unity");

        [UsedImplicitly]
        private bool CheckPlayerSpawnPoints() => 
            PlayerSpawnPoints.Count != 0;
#endif
    }

    [Serializable]
    public struct MaxMinValue
    {
        [HorizontalGroup("MaxMin")]
        public float Max;
        
        [HorizontalGroup("MaxMin")]
        public float Min;
    }
}