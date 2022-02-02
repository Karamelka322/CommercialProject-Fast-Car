using UnityEngine;

namespace CodeBase.Services.Input
{
    public class MobileInputModule : IInputModule
    {
        public IInputVariant CurrentInputVariant { get; set; }
        public Vector2 Axis => CurrentInputVariant.Axis;
    }
}