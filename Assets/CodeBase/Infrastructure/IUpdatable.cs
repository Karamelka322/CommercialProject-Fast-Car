using System;

namespace CodeBase.Infrastructure
{
    public interface IUpdatable
    {
        event Action OnUpdate;
    }
}