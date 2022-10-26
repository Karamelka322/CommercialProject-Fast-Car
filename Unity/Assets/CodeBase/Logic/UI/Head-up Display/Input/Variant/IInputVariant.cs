using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputVariant
    {
        Vector2 Axis { get; }
        bool Drift { get; }


        void EnableMoveBackwardsButton();
        void DisableMoveBackwardsButton();
    }
}