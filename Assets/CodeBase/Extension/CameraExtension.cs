using UnityEngine;

namespace CodeBase.Extension
{
    public static class CameraExtension
    {
        public static Vector3 CustomConvertWorldPointToScreenPoint(this Camera camera, Vector3 point)
        {
            Vector3 screenPoint = camera.WorldToScreenPoint(point);

            if (screenPoint.z > 0)
            {
                point.x = Screen.width - point.x;
                point.y = Screen.height - point.y;
            }

            return screenPoint;
        }
        
        public static bool IsPointVisible(this Camera camera, Vector3 screenPos) => 
            screenPos.z > 0 && screenPos.x > 0 && screenPos.x < camera.pixelWidth && screenPos.y > 0 && screenPos.y < camera.pixelHeight;
    }
}