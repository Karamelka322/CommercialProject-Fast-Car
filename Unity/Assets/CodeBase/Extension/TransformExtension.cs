using UnityEngine;

namespace CodeBase.Extension
{
    public static class TransformExtension
    {
        public static void Reset(this Transform transform)
        {
            transform.parent = null;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}