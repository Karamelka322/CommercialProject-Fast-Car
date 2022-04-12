using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputModule
    {
        Vector2 Axis { get; }
        IInputVariant CurrentInputVariant { get; set; }
    }
}