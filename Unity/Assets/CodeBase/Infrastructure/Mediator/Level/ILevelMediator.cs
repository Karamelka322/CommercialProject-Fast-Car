using CodeBase.Logic.Level.Generator;
using CodeBase.Logic.Player;
using CodeBase.Services.Input.Element;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Mediator.Level
{
    public interface ILevelMediator
    {
        void EnablePlayerAbility();
        void Construct(PlayerPrefab player);
        void EnableMoveBackwardsButton();
        void Construct(HUD hud);
        void UpdateHealthBar(float health);
        void UpdateGeneratorBar(float power);
        void UpdateAbilityBar(float energy);
        
        Transform Player { get; }
        
        bool Drift { get; }
        PlayerHook PlayerHook { get; }
        GeneratorHook GeneratorHook { get; }
        Vector2 MovementAxis();
        void Construct(GeneratorPrefab generator);
        (ButtonInputElement, ButtonInputElement) GetBackwardsButton();
        void DisableMoveBackwardsButton();
    }
}