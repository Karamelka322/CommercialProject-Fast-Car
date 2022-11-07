using CodeBase.Services.Input.Element;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputVariant
    {
        Vector2 Axis { get; }
        bool Drift { get; }


        void EnableMoveBackwardsButton();
        (ButtonInputElement, ButtonInputElement) GetBackwardsButton();
        void DisableMoveBackwardsButton();
    }
}