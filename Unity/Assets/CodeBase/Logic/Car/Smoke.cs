using System;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Smoke : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _particle;

        [SerializeField] 
        private Car _car;

        private IUpdateService _updateService;
        private float _startLifeTime;

        [Inject]
        private void Construct(IUpdateService updateService)
        {
            _startLifeTime = _particle.emissionRate;
            _updateService = updateService;
        }

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            float nextLifeTime = (float) Math.Round(_startLifeTime * (_car.Info.Speed / _car.Property.MaxSpeed), 1);

            _particle.emissionRate = nextLifeTime;
        }
    }
}