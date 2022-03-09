using System;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using CodeBase.Services.Defeat;
using CodeBase.Services.Random;
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
        
        public static int GetEmptyIndex<T>(this T[] array) where T : Object
        {
            for (int i = 0; i < array.Length; i++)
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

        public static SpawnPointData[] ConvertSpawnPointDataToPointData(this PointData[] array)
        {
            SpawnPointData[] spawnPointDatas = new SpawnPointData[array.Length];

            for (int i = 0; i < spawnPointDatas.Length; i++) 
                spawnPointDatas[i] = new SpawnPointData(false, array[i]);

            return spawnPointDatas;
        }

        public static PointData[] ConvertPointDataToSpawnPointData(this SpawnPointData[] array)
        {
            PointData[] spawnPointDatas = new PointData[array.Length];

            for (int i = 0; i < spawnPointDatas.Length; i++) 
                spawnPointDatas[i] = new PointData(array[i].Point.Position, array[i].Point.Rotation);

            return spawnPointDatas;
        }

        public static int GetNumberUnlockedSpawnPoints(this SpawnPointData[] spawnPointDatas)
        {
            int counter = 0;

            for (int i = 0; i < spawnPointDatas.Length; i++)
            {
                if (spawnPointDatas[i].IsLocked == false) 
                    counter++;
            }

            return counter;
        }

        public static PointData[] GetUnblockedSpawnPoints(this SpawnPointData[] spawnPointDatas)
        {
            int numberUnlockedSpawnPoints = spawnPointDatas.GetNumberUnlockedSpawnPoints();
            
            if (numberUnlockedSpawnPoints == spawnPointDatas.Length)
            {
                return spawnPointDatas.ConvertPointDataToSpawnPointData();
            }
            else
            {
                PointData[] points = new PointData[numberUnlockedSpawnPoints];

                int counter = 0;
            
                for (int i = 0; i < spawnPointDatas.Length; i++)
                {
                    if (spawnPointDatas[i].IsLocked == false)
                    {
                        points[counter] = spawnPointDatas[i].Point;
                        counter++;
                    }
                }
            
                return points;   
            }
        }

        public static void BlockSpawnPoint(this SpawnPointData[] spawnPointDatas, PointData point)
        {
            for (int i = 0; i < spawnPointDatas.Length; i++)
            {
                if (spawnPointDatas[i].Point == point)
                {
                    spawnPointDatas[i].IsLocked = true;
                    break;
                }
            }
        }

        public static void UnlockSpawnPoint(this SpawnPointData[] spawnPointDatas, PointData point)
        {
            for (int i = 0; i < spawnPointDatas.Length; i++)
            {
                if (spawnPointDatas[i].Point == point)
                {
                    spawnPointDatas[i].IsLocked = false;
                    break;
                }
            }
        }

        public static string ConvertToDateTime(this float value) => 
            new DateTime().AddSeconds(value).ToString("mm:ss:ff");
        
        public static bool IsNullHandler(this IHandler handler)
        {
            try
            {
                return string.IsNullOrEmpty(handler.name);
            }
            catch (MissingReferenceException exception)
            {
                return true;
            }
        }
    }
}