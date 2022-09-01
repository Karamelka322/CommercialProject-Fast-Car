using CodeBase.Extension;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.World
{
    public class RandomRotate : MonoBehaviour
    {
        [SerializeField] 
        private Transform _transform;
        
        [Space, SerializeField] 
        private int _frequency;

        [SerializeField] 
        private int _range;
        
        [SerializeField]
        private int _smooth;

        [SerializeField] 
        private AnimationCurve _curve;

        private Vector3 _direction;
        private Vector3 _nextDirection;
        private float _time;

        private IUpdateService _updateService;

        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
            _nextDirection = VectorExtension.Random(_range);
        }

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            _time += Time.deltaTime;
            _transform.Rotate(_direction, Space.World);
                
            if (_time >= _frequency)
            {
                _time = 0;
                _nextDirection = VectorExtension.Random(_range);
            }
                
            _direction = Vector3.Lerp(_direction, _nextDirection, _curve.Evaluate(_time / _smooth));
        }
    }
}