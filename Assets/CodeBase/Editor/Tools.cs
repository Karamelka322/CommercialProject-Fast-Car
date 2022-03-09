using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public static class Tools
    {
        private const string PlayerDataKey = "PlayerData";
        private const string DeveloperDataKey = "DeveloperData";

        [MenuItem("Tools/Game/Clear Player Data")]
        public static void ClearPlayerData()
        {
            PlayerPrefs.DeleteKey(PlayerDataKey);
            PlayerPrefs.Save();
        }

        [MenuItem("Tools/Game/Clear Developer Data")]
        public static void ClearDeveloperData()
        {
            PlayerPrefs.DeleteKey(DeveloperDataKey);
            PlayerPrefs.Save();
        }
    }
}