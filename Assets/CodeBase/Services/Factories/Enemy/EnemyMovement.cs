using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Car;
using CodeBase.Logic.Enemy;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Services.Factories.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] 
        private Car _car;

        [SerializeField] 
        private NavMeshAgentWrapper _navMeshAgentWrapper;

        private IUpdateService _updateService;

        private const int BackwardsMovementDuration = 1;
        private const int StopDuration = 1;

        private float _stopwatch;
        private float _timer;

        public void Construct(IUpdateService updateService) =>
            _updateService = updateService;

        private void Start() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDisable() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            if (IsStopped() == false)
            {
                MovingForward();
            }
            else
            {
                MovingBackwards();
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

                if(IsSleepTimer())
                    UpdateStopwatch();
            }
            
            return IsTimerRun();
        }

        private void UpdateTimer() => 
            _timer = Mathf.Clamp(_timer - Time.deltaTime, 0, BackwardsMovementDuration);

        private void UpdateStopwatch() => 
            _stopwatch = Mathf.Clamp(_stopwatch + (_car.Speed < 4f ? Time.deltaTime : -_stopwatch), 0, StopDuration);

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
    }
}