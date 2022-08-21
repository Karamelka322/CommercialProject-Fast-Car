using UnityEngine;

namespace CodeBase.Extension
{
    public static class VectorExtension
    {
        public static Vector3 Random(int range) => 
            new Vector3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-range, range));

        public static void ClampEuler(this ref Vector3 vector, float maxMinX, float maxMinZ)
        {
            float rotationX = vector.x % 360;
            
            if (rotationX > 180) 
                rotationX -= 360;
            else if (rotationX < -180) 
                rotationX += 360;
            
            vector.x = Mathf.Clamp(rotationX, -maxMinX, maxMinX);
            
            float rotationZ = vector.z % 360;
            
            if (rotationZ > 180) 
                rotationZ -= 360;
            else if (rotationZ < -180) 
                rotationZ += 360;
            
            vector.z = Mathf.Clamp(rotationZ, -maxMinZ, maxMinZ);
        }
    }
}