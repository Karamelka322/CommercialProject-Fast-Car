using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.UI
{
    public class Waymarkers : MonoBehaviour
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
            Movement();

        private void Movement()
        {
            Vector3 screenPoint = _camera.WorldToScreenPoint(Target.position);
            
            if(!_camera.IsPointVisible(screenPoint, _offset))
            {
                if(screenPoint.z < 0)
                {
                    screenPoint.x = Screen.width - screenPoint.x;
                    screenPoint.y = Screen.height - screenPoint.y;
                }
                
                screenPoint -= _screenCenter;
                
                float angle = Mathf.Atan2(screenPoint.y, screenPoint.x);
                angle -= 90f * Mathf.Deg2Rad;

                float cos = Mathf.Cos(angle);
                float sin = Mathf.Sin(-angle);
                float cotangent = cos / sin;
                
                float boundsY = (cos > 0f) ? _screenBounds.y : -_screenBounds.y;
                
                screenPoint.Set(boundsY / cotangent, boundsY, 0f);
                
                if(screenPoint.x > _screenBounds.x)
                {
                    screenPoint.Set(_screenBounds.x, _screenBounds.x * cotangent, 0f);
                }
                else if(screenPoint.x < -_screenBounds.x)
                {
                    screenPoint.Set(-_screenBounds.x, -_screenBounds.x * cotangent, 0f);
                }

                screenPoint += _screenCenter;
            }

            _mark.position = screenPoint;
        }
    }
}