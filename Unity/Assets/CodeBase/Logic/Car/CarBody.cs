using CodeBase.Services.Input;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    public class CarBody : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private const string Rotate = "Rotate";
        
        private IInputService _inputService;
        private IUpdateService _updateService;

        [Inject]
        public void Construct(IInputService inputService, IUpdateService updateService)
        {
            _inputService = inputService;
            _updateService = updateService;
        }

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate() => 
            _animator.SetFloat(Rotate, _inputService.Axis.y);
    }
}