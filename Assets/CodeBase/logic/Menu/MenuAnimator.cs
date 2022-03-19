using System;
using CodeBase.Logic.Menu;
using UnityEngine;

namespace CodeBase.Scene.Menu
{
    [RequireComponent(typeof(Animator))]
    public class MenuAnimator : MonoBehaviour
    {
        [SerializeField] 
        private MenuStates _menuStates;

        [SerializeField]
        private Animator _animator;

        private static readonly int SkipHash = Animator.StringToHash("Skip");
        
        private static readonly int IntroHash = Animator.StringToHash("Intro");
        private static readonly int MainMenuHash = Animator.StringToHash("MainMenu");
        private static readonly int PlayGameHash = Animator.StringToHash("PlayGame");
        
        private static readonly int SettingsHash = Animator.StringToHash("Settings");
        private static readonly int GarageHash = Animator.StringToHash("Garage");

        private void Start() => 
            _menuStates.OnChangeState += OnChangeState;

        private void OnDestroy() => 
            _menuStates.OnChangeState -= OnChangeState;

        public void Rebind() => 
            _animator.Rebind();

        public void SkipIntro() => 
            _animator.SetTrigger(id: SkipHash);

        private void OnChangeState(MenuState state)
        {
            switch (state)
            {
                case MenuState.Intro:
                {
                    PlayIntro();
                }
                    break;
                
                case MenuState.MainMenu:
                {
                    PlayMainMenu();
                }
                    break;

                case MenuState.Garage:
                {
                    PlayGarage();
                }
                    break;

                case MenuState.Settings:
                {
                    PlaySettings();
                }
                    break;

                case MenuState.PlayGame:
                {
                    PlayGame();
                }
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void PlayIntro() => 
            _animator.SetTrigger(id: IntroHash);

        private void PlayMainMenu() => 
            _animator.SetTrigger(id: MainMenuHash);

        private void PlayGame() => 
            _animator.SetTrigger(id: PlayGameHash);

        private void PlaySettings() => 
            _animator.SetTrigger(id: SettingsHash);

        private void PlayGarage() => 
            _animator.SetTrigger(id: GarageHash);
    }
}