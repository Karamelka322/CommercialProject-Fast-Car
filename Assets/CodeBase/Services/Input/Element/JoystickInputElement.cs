using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Services.Input.Element
{
    public class JoystickInputElement : MonoBehaviour, IJoystickInputElement, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] 
        private string _id;

        [Space, SerializeField] 
        private RectTransform _stick;

        [SerializeField] 
        private RectTransform _joystick;

        [Space, SerializeField, Range(1f, 2f)]
        private float _multiplier;
        
        public string Id => _id;
        public Vector2 Value => _value;

        private int _radius;
        private Vector2 _value;

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
            _value.x = Mathf.Clamp01((1f / _radius) * position.y * _multiplier);
            _value.y = Mathf.Clamp01((1f / _radius) * position.x * _multiplier);
        }

        private void ResetNormalizeValue() => 
            _value = Vector2.zero;
    }
}