using CodeBase.Logic.Car;
using CodeBase.Logic.Enemy;
using CodeBase.Services.Defeat;
using CodeBase.Services.Update;
using CodeBase.Services.Victory;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories.Enemy
{
    public class EnemyMovement : MonoBehaviour, IPlayerDefeatHandler, IPlayerVictoryHandler
    {
        [SerializeField] private Car _car;

        [SerializeField] private NavMeshAgentWrapper _navMeshAgentWrapper;

        private IUpdateService _updateService;

        private const int BackwardsMovementDuration = 1;
        private const int StopDuration = 1;

        private float _stopwatch;
        private float _timer;

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
            ToggleActivityMeshAgent();

            if (IsStopped() == false)
            {
                MovingForward();
            }
            else
            {
                MovingBackwards();
            }
        }

        private void ToggleActivityMeshAgent()
        {
            if (!_car.Info.IsGrounded && _navMeshAgentWrapper.Enabled)
            {
                _navMeshAgentWrapper.Enabled = false;
            }
            else if (_car.Info.IsGrounded && !_navMeshAgentWrapper.Enabled)
            {
                _navMeshAgentWrapper.Enabled = true;
            }
        }

        private bool IsStopped()
        {
            if (IsSleepStopwatch())
            {
                UpdateTimer();

                if (IsSleepTimer())
                    ResetStopwatch();
            }
            else
            {
                ResetTimer();

                if (IsSleepTimer())
                    UpdateStopwatch();
            }

            return IsTimerRun();
        }

        private void UpdateTimer() =>
            _timer = Mathf.Clamp(_timer - Time.deltaTime, 0, BackwardsMovementDuration);

        private void UpdateStopwatch() =>
            _stopwatch = Mathf.Clamp(_stopwatch + (_car.Info.Speed < 4f ? Time.deltaTime : -_stopwatch), 0, StopDuration);

        private void ResetTimer() =>
            _timer = BackwardsMovementDuration;

        private void ResetStopwatch() =>
            _stopwatch = 0;

        private bool IsSleepStopwatch() =>
            _stopwatch == StopDuration;

        private bool IsSleepTimer() =>
            _timer == BackwardsMovementDuration || _timer == 0;

        private bool IsTimerRun() =>
            _timer < BackwardsMovementDuration;

        private void MovingForward()
        {
            _car.Rotation(_navMeshAgentWrapper.GetNormalizeAngle());
            _car.Movement(_navMeshAgentWrapper.GetNormalizeSpeed());
        }

        private void MovingBackwards()
        {
            _car.Rotation(-_navMeshAgentWrapper.GetNormalizeAngle());
            _car.Movement(-_navMeshAgentWrapper.GetNormalizeSpeed());
        }
        
        public void OnDefeat()
        {
            _updateService.OnUpdate -= OnUpdate;
            
            _car.Movement(0);
            _car.Rotation(0);
        }

        public void OnVictory()
        {
            _updateService.OnUpdate -= OnUpdate;
            
            _car.Movement(0);
            _car.Rotation(0);
        }
    }
}