using UnityEngine;

namespace CodeBase.Extension
{
    public static class CameraExtension
    {
        public static bool IsPointVisible(this Camera camera, Vector3 screenPos, float offset = 0) => 
            screenPos.z > 0 && screenPos.x - offset > 0 && screenPos.x + offset < camera.pixelWidth && screenPos.y - offset > 0 && screenPos.y + offset < camera.pixelHeight;
    }
}