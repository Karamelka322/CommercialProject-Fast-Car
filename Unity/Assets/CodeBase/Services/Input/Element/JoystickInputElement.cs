using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Services.Input.Element
{
    public class JoystickInputElement : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Space, SerializeField] 
        private RectTransform _stick;

        [SerializeField] 
        private RectTransform _joystick;

        [Space, SerializeField, Range(1f, 2f)]
        private float _multiplier;
        
        public Vector2 Axis => _axis;

        private int _radius;
        private Vector2 _axis;

        private void Awake() => 
            _radius = (int)_joystick.rect.width / 2;

        public void OnPointerDown(PointerEventData eventData)
        {
            MoveStick(eventData);
            SetNormalizeValue(_stick.localPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveStick(eventData);
            SetNormalizeValue(_stick.localPosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetStick();
            ResetNormalizeValue();
        }

        private void MoveStick(PointerEventData eventData)
        {
            Vector3 point = _joystick.InverseTransformPoint(eventData.position);
            _stick.localPosition = point.magnitude < _radius ? point : point.normalized * _radius;
        }

        private void ResetStick() => 
            _stick.localPosition = Vector3.zero;

        private void SetNormalizeValue(in Vector3 position)
        {
            _axis.x = Mathf.Clamp((1f / _radius) * position.y * _multiplier, -1, 1);
            _axis.y = Mathf.Clamp((1f / _radius) * position.x * _multiplier, -1, 1);
        }

        private void ResetNormalizeValue() => 
            _axis = Vector2.zero;
    }
}