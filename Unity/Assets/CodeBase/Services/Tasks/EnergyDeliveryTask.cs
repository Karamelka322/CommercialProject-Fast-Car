using System;
using CodeBase.Logic.Item;
using CodeBase.Logic.Level.Generator;
using CodeBase.Logic.Player;
using CodeBase.UI;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Tasks
{
    public class EnergyDeliveryTask : ITask
    {
        private readonly PlayerHook _playerHook;
        private readonly GeneratorHook _generatorHook;
        private readonly Waymarker _marker;

        public TaskStatus Status { get; private set; }
        public event Action<ITask, TaskStatus> ChangeStatus;

        public EnergyDeliveryTask(
            PlayerHook playerHook,
            GeneratorHook generatorHook,
            Waymarker marker)
        {
            _playerHook = playerHook;
            _generatorHook = generatorHook;
            _marker = marker;
        }

        public void Start()
        {
            Status = TaskStatus.Start;
            ChangeStatus?.Invoke(this, Status);
            
            _playerHook.EnergyCapture += PlayerCaptureEnergy;
            _generatorHook.EnergyCapture += GeneratorCaptureEnergy;
        }

        public void Stop()
        {
            Status = TaskStatus.Finish;
            ChangeStatus?.Invoke(this, Status);

            _playerHook.EnergyCapture -= PlayerCaptureEnergy;
            _generatorHook.EnergyCapture -= GeneratorCaptureEnergy;
            
            Object.Destroy(_marker.gameObject);
        }

        private void PlayerCaptureEnergy()
        {
            Status = TaskStatus.InWork;
            ChangeStatus?.Invoke(this, Status);

            _marker.Target = _generatorHook.transform;
        }

        private void GeneratorCaptureEnergy(Energy energy) => 
            Stop();
    }
}