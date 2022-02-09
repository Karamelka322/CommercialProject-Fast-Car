using UnityEngine;

namespace CodeBase.Extension
{
    public static class DataExtension
    {
        public static float CosAngle(this Vector3 center, Vector3 vectorA, Vector3 vectorB) => 
            ((vectorB.x - vectorA.x) * (center.z - vectorA.z) - (center.x - vectorA.x) * (vectorB.z - vectorA.z)) /
            (Mathf.Sqrt(Mathf.Pow(vectorB.x - vectorA.x, 2) + Mathf.Pow(vectorB.z - vectorA.z, 2)) *
             Mathf.Sqrt(Mathf.Pow(center.x - vectorA.x, 2) + Mathf.Pow(center.z - vectorA.z, 2)));

        public static string SerializeToJson(this object obj) => 
            JsonUtility.ToJson(obj);

        public static T DeserializeFromJson<T>(this string json) => 
            JsonUtility.FromJson<T>(json);
    }
}