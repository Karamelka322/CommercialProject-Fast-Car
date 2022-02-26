using CodeBase.Data.Perseistent.Developer;
using CodeBase.Extension;
using CodeBase.Infrastructure;
using CodeBase.Scene;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(GameBootstrapper))]
    public class GameBootstrapperEditor : UnityEditor.Editor
    {
        private const string DeveloperDataKey = "DeveloperData";

        private static FirstSceneId _firstSceneId;
        
        private DeveloperPersistentData _developerData;
        private GUIStyle _headerStyle;

        private void OnEnable()
        {
            _developerData = LoadDeveloperPersistentDataOrInstNew();
            _firstSceneId = _developerData.FirstScene == SceneNameConstant.Level ? FirstSceneId.Level : FirstSceneId.Menu;
            
            SetHeaderStyle();
        }

        private void OnDisable() => 
            SaveDeveloperPersistentData();

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying == false)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                
                EditorGUILayout.PrefixLabel("Developer Settings", _headerStyle, _headerStyle);

                _firstSceneId = (FirstSceneId)EditorGUILayout.EnumPopup("First Scene", _firstSceneId);
                _developerData.FirstScene = _firstSceneId == FirstSceneId.Level ? SceneNameConstant.Level : "";
            }
        }

        private void SaveDeveloperPersistentData() => 
            PlayerPrefs.SetString(DeveloperDataKey, _developerData.SerializeToJson());

        private static DeveloperPersistentData LoadDeveloperPersistentDataOrInstNew() => 
            PlayerPrefs.GetString(DeveloperDataKey).DeserializeFromJson<DeveloperPersistentData>() ?? NewDeveloperPersistentData();

        private static DeveloperPersistentData NewDeveloperPersistentData()
        {
            return new DeveloperPersistentData()
            {
                FirstScene = ""
            };
        }

        private void SetHeaderStyle()
        {
            _headerStyle = new GUIStyle();
            _headerStyle.normal.textColor = Color.yellow;
            _headerStyle.fontStyle = FontStyle.Bold;
        }

        private enum FirstSceneId
        {
            Menu = 0,
            Level = 1,
        }
    }
}