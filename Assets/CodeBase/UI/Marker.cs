using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.UI
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _mark;

        [SerializeField] 
        private Transform _target;

        [SerializeField] 
        private float _offset;

        private Vector3 _screenCenter;
        private Vector3 _screenBounds;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            _screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            _screenBounds = _screenCenter - new Vector3(_offset, _offset, 0f);
        }

        private void Update() => 
            Movement();

        private void Movement()
        {
            Vector3 point = _camera.WorldToScreenPoint(_target.position);
            
            if(!_camera.IsPointVisible(point, _offset))
            {
                if(point.z < 0)
                {
                    point.x = Screen.width - point.x;
                    point.y = Screen.height - point.y;
                }
                
                point -= _screenCenter;
                
                float angle = Mathf.Atan2(point.y, point.x);
                angle -= 90f * Mathf.Deg2Rad;

                float cos = Mathf.Cos(angle);
                float sin = Mathf.Sin(-angle);
                float cotangent = cos / sin;
                
                float boundsY = (cos > 0f) ? _screenBounds.y : -_screenBounds.y;
                
                point.Set(boundsY / cotangent, boundsY, 0f);
                
                if(point.x > _screenBounds.x)
                {
                    point.Set(_screenBounds.x, _screenBounds.x * cotangent, 0f);
                }
                else if(point.x < -_screenBounds.x)
                {
                    point.Set(-_screenBounds.x, -_screenBounds.x * cotangent, 0f);
                }

                _mark.position = point + _screenCenter;
            }

            _mark.position = point;
        }
    }
}