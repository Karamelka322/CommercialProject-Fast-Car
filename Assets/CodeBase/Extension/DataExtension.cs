using UnityEngine;

namespace CodeBase.Extension
{
    public static class DataExtension
    {
        public static string SerializeToJson(this object obj) => 
            JsonUtility.ToJson(obj);

        public static T DeserializeFromJson<T>(this string json) => 
            JsonUtility.FromJson<T>(json);
    }
}