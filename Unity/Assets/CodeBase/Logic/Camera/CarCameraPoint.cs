using CodeBase.Logic.World;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Camera
{
    public class CarCameraPoint : Point
    {
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField, Range(0, 10)] private int _offset;
        [SerializeField, Range(0.1f, 5)] private float _speedOffset;
        [SerializeField, Range(1, 25)] private int _maxCarSpeed; 
        [SerializeField, Range(0, 1)] private float _treshold;

        private Vector3 _deflection;
        private IUpdateService _updateService;

        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
            _deflection = new Vector3(0, 0, _offset);
        }
        
        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate() => 
            Movement();

        private void Movement()
        {
            float speed = _rigidbody.velocity.magnitude;
            float minSpeed = _maxCarSpeed * _treshold;
            
            if (speed >= minSpeed)
            {
                MoveForward(speed, minSpeed);
            }
            else
            {
                ResetOffset();
            }
        }

        private void ResetOffset() => 
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, SpeedOffset());

        private void MoveForward(in float speed, in float minSpeed) => 
            transform.localPosition = Vector3.Lerp(transform.localPosition, NextPosition(speed, minSpeed), SpeedOffset());

        private Vector3 NextPosition(in float speed, in float minSpeed) => 
            _deflection * ((speed - minSpeed) / (_maxCarSpeed - minSpeed));

        private float SpeedOffset() => 
            Time.deltaTime * _speedOffset;
    }
}