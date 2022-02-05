using UnityEngine;

namespace CodeBase.Extension
{
    public static class SceneExtension
    {
        public static T FindComponentInRootGameObjects<T>(this UnityEngine.SceneManagement.Scene scene)
        {
            GameObject[] objects = scene.GetRootGameObjects();

            for (int i = 0; i < objects.Length; i++)
            {
                if(objects[i].TryGetComponent(out T component))
                    return component;
            }
            
            for (int i = 0; i < objects.Length; i++)
            {
                T component = objects[i].GetComponentInChildren<T>();

                if(component != null)
                    return component;
            }

            return default;
        }
    }
}