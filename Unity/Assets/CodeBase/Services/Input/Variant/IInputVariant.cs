using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputVariant
    {
        Vector2 Axis { get; }
        
        event Action OnStartDrift;
        event Action OnStopDrift;
    }
}