using CodeBase.Logic.World;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Camera
{
    public class CameraPoint : Point
    {
        [Space, SerializeField] 
        private Car.Car _car;
        
        [Space, SerializeField, Range(0, 10)] 
        private int _offset;
        
        [SerializeField, Range(0.1f, 5)] 
        private float _speedOffset;

        [SerializeField, Range(0, 1)] 
        private float _threshold;

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

        private void OnValidate() => 
            _deflection = new Vector3(0, 0, _offset);

        private void Movement()
        {
            float speed = _car.Info.Speed;
            float minSpeed = _car.Property.MaxSpeed * _threshold;
            
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
            _deflection * ((speed - minSpeed) / (_car.Property.MaxSpeed - minSpeed));

        private float SpeedOffset() => 
            Time.deltaTime * _speedOffset;
    }
}