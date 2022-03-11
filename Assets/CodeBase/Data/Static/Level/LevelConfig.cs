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
        [GUIColor(0.8f, 0.8f, 0), InfoBox("Already in use!", InfoMessageType.Error, "CheckLevelType")]
        public LevelTypeId Type;

        [GUIColor(0.8f, 0.8f, 0), InfoBox("It is not geometry scene name!", InfoMessageType.Error, "CheckSceneName"), ValueDropdown("GetAllSceneNames")] 
        public string SceneName;

        [GUIColor(0.8f, 0.8f, 0), MinValue(1), MaxValue(999)]
        public int VictoryTime;

        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10), ReadOnly, GUIColor(1f, 1f, 0), InfoBox("Empty", InfoMessageType.Error,"CheckPlayerSpawnPoints")]
        public PointData[] PlayerSpawnPoints;

#if UNITY_EDITOR

        [UsedImplicitly, Button("Collect"), GUIColor(0.5f, 0.7f, 1f)]
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
                    if (levelStaticDatas[j].Level == this)
                        continue;
                    
                    names.Remove(levelStaticDatas[i].Level.ToString());
                }
            }

            string value = string.IsNullOrEmpty(names[names.Count - 1]) ? Enum.GetNames(typeof(LevelTypeId)).Last() : names[names.Count - 1];
            Type = (LevelTypeId)Enum.Parse(typeof(LevelTypeId), value);
        }
        
        private void FillPlayerSpawnPoints()
        {
            PlayerSpawnPoint[] generatorSpawnPoints = UnityEngine.Object.FindObjectsOfType<PlayerSpawnPoint>();

            PlayerSpawnPoints = new PointData[generatorSpawnPoints.Length];
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                PlayerSpawnPoints[i] = new PointData(generatorSpawnPoints[i].WorldPosition, generatorSpawnPoints[i].WorldRotation);
        }

        [ShowIf("CheckPlayerSpawnPoints"), Button("Generate Spawn Point"), GUIColor(0.5f, 0.7f, 1f)]
        private void GenerateSpawnPoint() => 
            UnityEngine.Object.Instantiate(Resources.Load<PlayerSpawnPoint>("Level/SpawnPoint/PlayerSpawnPoint"), Vector3.zero, Quaternion.identity);
        
        [UsedImplicitly]
        private bool CheckLevelType()
        {
            LevelStaticData[] levelStaticDatas = Resources.LoadAll<LevelStaticData>("StaticData/Level");

            for (int i = 0; i < levelStaticDatas.Length; i++)
            {
                if (levelStaticDatas[i].Level == this)
                    continue;

                if (levelStaticDatas[i].Level.Type == Type)
                    return true;
            }

            return false;
        }
        
        [UsedImplicitly]
        private bool CheckSceneName() => 
            !AssetDatabase.LoadAssetAtPath<SceneAsset>($"Assets/Scenes/Geometry/{SceneName}.unity");

        [UsedImplicitly]
        private static IEnumerable GetAllSceneNames()
        {
            List<string> files = Directory.GetFiles(Application.dataPath + "/Scenes/Geometry/").ToList();

            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].EndsWith(".unity"))
                {
                    files[i] = files[i].Substring(files[i].LastIndexOf("/") + 1).Replace(".unity", "");
                }
                else
                {
                    files.RemoveAt(i);
                    i--;
                }
            }
            
            return files;
        }
        
        [UsedImplicitly]
        private bool CheckPlayerSpawnPoints() => 
            PlayerSpawnPoints == null || PlayerSpawnPoints.Length == 0;
        
#endif
    }
}