using CodeBase.Logic.Car;
using CodeBase.Logic.Enemy;
using CodeBase.Services.Defeat;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField] private EnemyCrash _crash;
        [SerializeField] private NavMeshAgentWrapper _navMeshAgentWrapper;

        private IUpdateService _updateService;

        private float _time;

        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Start() =>
            _updateService.OnUpdate += OnUpdate;

        private void OnDestroy() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            if (_crash.Crash)
                _time = 1;
            else
                _time -= Time.deltaTime;

            ToggleActivityMeshAgent();
            UpdateAxis();
        }

        private void UpdateAxis() => 
            _car.Property.Axis = _time >= 0 ? -_navMeshAgentWrapper.Axis : _navMeshAgentWrapper.Axis;

        private void ToggleActivityMeshAgent()
        {
            if (!_car.Info.IsGroundedStrict && _navMeshAgentWrapper.Enabled)
            {
                _navMeshAgentWrapper.Enabled = false;
            }
            else if (_car.Info.IsGroundedStrict && !_navMeshAgentWrapper.Enabled)
            {
                _navMeshAgentWrapper.Enabled = true;
            }
        }
    }
}