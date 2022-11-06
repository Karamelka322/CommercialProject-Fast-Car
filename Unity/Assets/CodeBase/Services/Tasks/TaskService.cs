using System.Collections.Generic;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Services.Factories.UI;
using CodeBase.Services.Spawner;
using CodeBase.UI;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Tasks
{
    [UsedImplicitly]
    public class TaskService : ITaskService
    {
        private readonly List<ITask> _tasks = new List<ITask>();

        private readonly DiContainer _diContainer;

        public TaskService(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            _diContainer.Resolve<ISpawnerService>().EnergySpawnerModule.OnSpawnEnergy += OnSpawnEnergy;
        }

        private void OnSpawnEnergy(GameObject energy) => 
            CreateEnergyDeliveryTask(energy);

        public void CleanUp()
        {
            foreach (ITask task in _tasks)
            {
                task.ChangeStatus -= OnChangeStatus;
                task.Stop();
            }

            _tasks.Clear();
        }

        private void CreateEnergyDeliveryTask(GameObject energy)
        {
            Waymarker marker = _diContainer.Resolve<IUIFactory>().LoadEnergyMarker(energy.transform);

            ITask task = new EnergyDeliveryTask(
                _diContainer.Resolve<ILevelMediator>().PlayerHook,
                _diContainer.Resolve<ILevelMediator>().GeneratorHook,
                marker);

            _tasks.Add(task);
            task.Start();

            task.ChangeStatus += OnChangeStatus;
        }

        private void OnChangeStatus(ITask task, TaskStatus status)
        {
            if (status == TaskStatus.Finish)
            {
                _tasks.Remove(task);
                task.ChangeStatus -= OnChangeStatus;
            }
        }
    }
}