using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.UI
{
    public class Waypoints : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _mark;

        [SerializeField] 
        private float _offset;

        private Vector3 _screenCenter;
        private Vector3 _screenBounds;
        
        private Camera _camera;

        public Transform Target { get; set; }

        private void Awake()
        {
            _camera = Camera.main;
            _screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            _screenBounds = _screenCenter - new Vector3(_offset, _offset, 0f);
        }

        private void Update()
        {
            if (Target == null)
                return;

            Follow();
        }

        private void Follow() => 
            _mark.position = GetMarkPosition();

        private Vector3 GetMarkPosition()
        {
            Vector3 point = _camera.CustomConvertWorldPointToScreenPoint(Target.position);

            if (_camera.IsPointVisible(point))
            {
                return point;
            }
            else
            {
                point -= _screenCenter;
                
                float angle = Mathf.Atan2(point.y, point.x);
                angle -= 90f * Mathf.Deg2Rad;

                float cos = Mathf.Cos(angle);
                float sin = -Mathf.Sin(angle);
                float cotangent = cos / sin;
                
                float boundsY = (cos > 0f) ? _screenBounds.y : -_screenBounds.y;
                
                point.Set(boundsY / cotangent, boundsY, 0f);
                
                if (point.x > _screenBounds.x)
                {
                    point.Set(_screenBounds.x, _screenBounds.x * cotangent, 0f);
                }
                else if (point.x < -_screenBounds.x)
                {
                    point.Set(-_screenBounds.x, -_screenBounds.x * cotangent, 0f);
                }

                return point + _screenCenter;
            }
        }
    }
}