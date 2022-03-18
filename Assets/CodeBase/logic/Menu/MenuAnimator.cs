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

        private static readonly int PlayOpenMenuParameter = Animator.StringToHash("PlayOpenMenu");
        private static readonly int PlayIdleMenuParameter = Animator.StringToHash("PlayIdleMenu");
        private static readonly int PlayCloseMenuParameter = Animator.StringToHash("PlayCloseMenu");
        
        private static readonly int PlayOpenSettingsParameter = Animator.StringToHash("PlayOpenSettings");
        private static readonly int PlayCloseSettingsParameter = Animator.StringToHash("PlayCloseSettings");

        private static readonly int PlayOpenGarageParameter = Animator.StringToHash("PlayOpenGarage");
        private static readonly int PlayCloseGarageParameter = Animator.StringToHash("PlayCloseGarage");

        private void Start() => 
            _menuStates.OnChangeState += OnChangeState;

        private void OnDestroy() => 
            _menuStates.OnChangeState -= OnChangeState;

        private void OnChangeState(MenuState state)
        {
            switch (state)
            {
                case MenuState.Intro:
                {
                    Rebind();
                    PlayOpenMenu();
                }
                    break;
                
                case MenuState.MainMenu:
                {
                    PlayIdleMenu();
                }
                    break;

                case MenuState.Garage:
                {
                    PlayOpenGarage();
                }
                    break;

                case MenuState.Settings:
                {
                    PlayOpenSettings();
                }
                    break;

                case MenuState.PlayGame:
                {
                    PlayCloseMenu();
                }
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Rebind() => 
            _animator.Rebind();

        private void PlayOpenMenu() => 
            _animator.SetTrigger(id: PlayOpenMenuParameter);

        private void PlayIdleMenu() => 
            _animator.SetTrigger(id: PlayIdleMenuParameter);

        private void PlayCloseMenu() => 
            _animator.SetTrigger(id: PlayCloseMenuParameter);

        private void PlayOpenSettings() => 
            _animator.SetTrigger(id: PlayOpenSettingsParameter);

        private void PlayOpenGarage() => 
            _animator.SetTrigger(id: PlayOpenGarageParameter);

        private void PlayCloseSettings() => 
            _animator.SetTrigger(id: PlayCloseSettingsParameter);

        private void PlayCloseGarage() => 
            _animator.SetTrigger(id: PlayCloseGarageParameter);
    }
}