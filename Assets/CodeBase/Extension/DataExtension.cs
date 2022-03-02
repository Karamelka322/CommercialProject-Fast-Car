using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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

        public static int NumberEmptyIndexes<T>(this IReadOnlyList<T> array) where T : Object
        {
            int counter = 0;
            
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] == null)
                    counter++;
            }

            return counter;
        }

        public static int GetEmptyIndex<T>(this IReadOnlyList<T> array) where T : Object
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] == null)
                    return i;
            }

            throw new ArgumentOutOfRangeException();
        }

        public static T Random<T>(this List<T> list) => 
            list[UnityEngine.Random.Range(0, list.Count)];

        public static T Random<T>(this T[] array) => 
            array[UnityEngine.Random.Range(0, array.Length)];
    }
}