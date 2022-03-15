using UnityEngine;

namespace CodeBase.Extension
{
    public static class VectorExtension
    {
        public static Vector3 Random(int range) => 
            new Vector3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-range, range));
    }
}