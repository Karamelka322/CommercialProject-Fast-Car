using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    public class CarAnimation : MonoBehaviour
    {
        [SerializeField] 
        private Animator _animator;

        [Space, SerializeField] 
        private float _speedRotationBody;

        [SerializeField] 
        private AnimationCurve _curveRotationBody;

        private static readonly int RotateHash = Animator.StringToHash("Rotate");

        private IUpdateService _updateService;
        private ILevelMediator _mediator;

        private float _axis;

        [Inject]
        public void Construct(ILevelMediator mediator, IUpdateService updateService)
        {
            _mediator = mediator;
            _updateService = updateService;
        }

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate() => 
            RotateBody();

        private void RotateBody()
        {
            _axis = Mathf.Lerp(_axis, _mediator.MovementAxis().y, _curveRotationBody.Evaluate(Time.deltaTime * _speedRotationBody));
            _animator.SetFloat(RotateHash, _axis);
        }
    }
}