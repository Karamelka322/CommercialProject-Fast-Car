using System;
using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.UI
{
    public class Waypoints : MonoBehaviour
    {
        [SerializeField] private RectTransform _mark;

        [SerializeField] private RectTransform _area;

        public float _multiplyer = 1;

        private Camera _camera;
        private Vector3 _offset;
        private SizeValue _screenSize;
        
        private GameObject _point;
        private Canvas _canvas;

        public Transform Target { get; set; }

        private void Awake()
        {
            _camera = Camera.main;

            _point = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity, _camera.transform);

            _canvas = GetComponentInParent<Canvas>();

            _screenSize.Height = Screen.height / 2;
            _screenSize.Width = Screen.width / 2;
        }

        private void Update()
        {
            if (Target == null)
                return;

            Follow();
        }

        private void Follow()
        {
            if (true)
            {
                _point.transform.position = GetPosition();
                //_point.transform.localPosition = new Vector3(_point.transform.localPosition.x, _point.transform.localPosition.y, _camera.nearClipPlane);
                
                Vector2 screen = _point.transform.localPosition;

                screen.x = _area.rect.width / 2 * Mathf.Clamp(screen.x, -1, 1);
                screen.y = _area.rect.height / 2 * Mathf.Clamp(screen.y, -1, 1);
            
                //Debug.Log(RectTransformUtility.PixelAdjustPoint(screen, Target, _canvas));
                
                _mark.anchoredPosition = screen;
                //_mark.anchoredPosition = MathfExtension.ViewportClamp(RectTransformUtility.PixelAdjustPoint(screen, _camera.transform, _canvas), _screenSize);
            }
            else
            {
                _mark.position = MathfExtension.ViewportClamp(_camera.WorldToScreenPoint(Target.position), _screenSize);
            }
            
            //_mark.position = MathfExtension.ViewportClamp(_camera.WorldToScreenPoint(_point.transform.position), _screenSize);

            //Variant_1();
        }
        
        private bool IsPointVisible(Vector3 point) => 
             Vector3.Angle(_camera.transform.forward, point - _camera.transform.position) < _camera.fieldOfView;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_camera.transform.position + (_camera.transform.forward * _camera.nearClipPlane), _point.transform.position);
        }

        private Vector3 GetPosition()
        {
            return _camera.transform.position + (Target.position - _camera.transform.position).normalized * _multiplyer;
        }

        // private void Variant_1()
        // {
        //     if (IsPointVisible(Target.position))
        //     {
        //         Vector3 nextPosition = MathfExtension.ViewportClamp(_camera.WorldToScreenPoint(Target.position), _screenSize);
        //         _mark.position = Vector3.Lerp(_mark.position, nextPosition, Time.deltaTime * 5);
        //     }
        //     else
        //     {
        //         Vector3 nextPosition = MathfExtension.ViewportClamp(_camera.WorldToScreenPoint(GetPosition()), _screenSize);
        //         _mark.position = Vector3.Lerp(_mark.position, nextPosition, Time.deltaTime * 5);
        //     }
        // }
        //
        // private bool IsPointVisible(Vector3 point) => 
        //     Vector3.Angle(_camera.transform.forward, point - _camera.transform.position) < _camera.fieldOfView;
        //
        // private void OnDrawGizmos()
        // {
        //     if(Target == null)
        //         return;
        //
        //     Gizmos.color = Color.yellow;
        //     
        //     Gizmos.DrawLine(_camera.transform.position, StartingPoint());
        //     Gizmos.DrawLine(StartingPoint(), Target.position);
        //     Gizmos.DrawLine(_camera.transform.position, _offset);
        //
        //     Gizmos.DrawSphere(GetPosition(), 1);
        //     Gizmos.DrawLine(_offset, GetPosition());
        // }
        //
        // private Vector3 GetPosition()
        // {
        //     Vector3 startingPoint = StartingPoint();
        //     float distance = (Target.position - startingPoint).magnitude;
        //     
        //     _offset = startingPoint + (-_camera.transform.forward * distance * 2);
        //     return _offset + (Target.position - _offset).normalized * distance * 2.5f;
        // }
        //
        // private Vector3 StartingPoint() =>
        //     _camera.transform.position + (_camera.transform.forward * 28);
    }
}