using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace CodeBase.Data.Static.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "Level", order = 51)]
    public class LevelStaticData : ScriptableObject
    {
        [FoldoutGroup("General"),GUIColor(0.8f, 0.8f, 0), MinValue(1), MaxValue(9999)]
        public int VictoryTime;

        [FoldoutGroup("Spawn"), HideLabel]
        public SpawnConfig Spawn;
        
        [FoldoutGroup("Reward"), HideLabel]
        public RewardConfig Reward;
        
        public LevelTypeId Type => (LevelTypeId)Enum.Parse(typeof(LevelTypeId), name.Substring(0, 7));
        public string SceneName => name;

#if UNITY_EDITOR
        
        [PropertySpace, Button("Open Scene")]
        private void OpenScene() => 
            EditorSceneManager.OpenScene($"Assets/Scenes/Level/{name}/{name}.unity");
        
#endif
    }
}