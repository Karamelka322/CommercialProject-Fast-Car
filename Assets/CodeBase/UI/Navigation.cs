using UnityEngine;

namespace CodeBase.UI
{
    public class Navigation : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _mark;

        [SerializeField] 
        private RectTransform _area;

        private Camera _camera;

        public Transform Target { get; set; }

        private void Awake() => 
            _camera = Camera.main;

        private void Update()
        {
            if(Target == null)
                return;

            Follow(Target);
        }

        private void Follow(Transform target)
        {
            Vector3 position = _area.InverseTransformPoint(_camera.WorldToScreenPoint(target.position));
            position = position.z > 0 ? position : -position;
            
            _mark.localPosition = new Vector3(Mathf.Clamp(position.x, -_area.rect.width / 2, _area.rect.width / 2), 
                Mathf.Clamp(position.y, -_area.rect.height / 2, _area.rect.height / 2), 0);
        }
    }
}