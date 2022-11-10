using CodeBase.Logic.Level.Generator;
using CodeBase.Logic.Player;
using CodeBase.Logic.Player.Ability;
using CodeBase.Services.Input;
using CodeBase.Services.Input.Element;
using CodeBase.UI;
using CodeBase.UI.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Mediator.Level
{
    public class LevelMediator : MonoBehaviour, ILevelMediator
    {
        public Transform Player { get; private set; }
        public PlayerHook PlayerHook { get; private set; }
        public GeneratorHook GeneratorHook { get; private set; }
        
        public bool Drift => _inputVariant.Drift;

        private IAbility _playerAbility;
        private IInputVariant _inputVariant;
        private PlayerHealthBar _healthBar;
        private GeneratorPowerBar _powerBar;
        private AbilityBar _abilityBar;
        private StopwatchDisplay _stopwatchDisplay;

        public void Construct(PlayerPrefab player)
        {
            Player = player.transform;
            PlayerHook = player.GetComponent<PlayerHook>();
            _playerAbility = player.GetComponentInChildren<IAbility>();
        }

        public void Construct(HUD hud)
        {
            _inputVariant = hud.GetComponentInChildren<IInputVariant>();
            _healthBar = hud.GetComponentInChildren<PlayerHealthBar>();
            _powerBar = hud.GetComponentInChildren<GeneratorPowerBar>();
            _abilityBar = hud.GetComponentInChildren<AbilityBar>();
            _stopwatchDisplay = hud.GetComponentInChildren<StopwatchDisplay>();
        }

        public void Construct(GeneratorPrefab generator)
        {
            GeneratorHook = generator.GetComponent<GeneratorHook>();
        }

        public void UpdateAbilityBar(float energy) => _abilityBar.Value = energy;
        public void UpdateHealthBar(float health) => _healthBar.Value = health;
        public void UpdateGeneratorBar(float power) => _powerBar.Value = power;
        public void ShowStopwatch(float time) => _stopwatchDisplay.Show(time);
        public float StopwatchTime() => _stopwatchDisplay.Time;

        public void EnablePlayerAbility() => _playerAbility.Enable();
        public Vector2 MovementAxis() => _inputVariant.Axis;
        public void EnableMoveBackwardsButton() => _inputVariant.EnableMoveBackwardsButton();
        public void DisableMoveBackwardsButton() => _inputVariant.DisableMoveBackwardsButton();
        public (ButtonInputElement, ButtonInputElement) GetBackwardsButton() => _inputVariant.GetBackwardsButton();
    }
}