using UnityEngine;

namespace CodeBase.Extension
{
    public static class MathfExtension
    {
        public static Vector3 ViewportClamp(Vector3 point, SizeValue viewportSize)
        {
            point = point.z > 0 ? point : -point;
            
            point.x = Mathf.Clamp(point.x, 0, viewportSize.Width);
            point.y = Mathf.Clamp(point.y, 0, viewportSize.Height);
            point.z = 0;

            return point;
        }
        
        public static Vector3 ViewportClamp(Vector2 point, SizeValue viewportSize)
        {
            point.x = Mathf.Clamp(point.x, -viewportSize.Width, viewportSize.Width);
            point.y = Mathf.Clamp(point.y, -viewportSize.Width, viewportSize.Height);

            return point;
        }
    }
}