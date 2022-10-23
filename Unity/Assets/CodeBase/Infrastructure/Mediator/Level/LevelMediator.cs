using CodeBase.Logic.Player;
using CodeBase.Logic.Player.Ability;
using CodeBase.Services.Input;
using CodeBase.UI;
using CodeBase.UI.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Mediator.Level
{
    public class LevelMediator : MonoBehaviour, ILevelMediator
    {
        public Transform Player { get; set; }
        
        private IAbility _playerAbility;
        private ButtonsInputVariant _inputVariant;
        private PlayerHealthBar _healthBar;
        private GeneratorPowerBar _powerBar;
        private AbilityBar _abilityBar;

        public void Construct(PlayerPrefab player)
        {
            Player = player.transform;
            _playerAbility = player.GetComponentInChildren<IAbility>();
        }

        public void Construct(HUD hud)
        {
            _inputVariant = hud.GetComponentInChildren<ButtonsInputVariant>();
            _healthBar = hud.GetComponentInChildren<PlayerHealthBar>();
            _powerBar = hud.GetComponentInChildren<GeneratorPowerBar>();
            _abilityBar = hud.GetComponentInChildren<AbilityBar>();
        }

        public void UpdateAbilityBar(float energy) => _abilityBar.Value = energy;
        public void UpdateHealthBar(float health) => _healthBar.Value = health;
        public void UpdateGeneratorBar(float power) => _powerBar.Value = power;

        public void EnablePlayerAbility() => _playerAbility.Enable();
        public void EnableMoveBackwardsButton() => _inputVariant.EnableMoveBackwardsButton();
        public void DisableMoveBackwardsButton() => _inputVariant.DisableMoveBackwardsButton();
    }
}