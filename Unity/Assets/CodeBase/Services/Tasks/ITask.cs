using System;

namespace CodeBase.Services.Tasks
{
    public interface ITask
    {
        TaskStatus Status { get; }
        event Action<ITask, TaskStatus> ChangeStatus;
        void Start();
        void Stop();
    }
}