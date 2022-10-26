using System;
using CodeBase.Logic.Player;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Mediator.Level
{
    public interface ILevelMediator
    {
        void EnablePlayerAbility();
        void Construct(PlayerPrefab player);
        void EnableMoveBackwardsButton();
        void DisableMoveBackwardsButton();
        void Construct(HUD hud);
        void UpdateHealthBar(float health);
        void UpdateGeneratorBar(float power);
        void UpdateAbilityBar(float energy);
        Transform Player { get; set; }
        bool Drift { get; }
        Vector2 MovementAxis();
    }
}