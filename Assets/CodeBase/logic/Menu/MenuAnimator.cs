using System;
using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.Scene.Menu
{
    [RequireComponent(typeof(Animator))]
    public class MenuAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private static readonly int PlayOpenMenuParameter = Animator.StringToHash("PlayOpenMenu");
        private static readonly int PlayIdleMenuParameter = Animator.StringToHash("PlayIdleMenu");
        private static readonly int PlayCloseMenuParameter = Animator.StringToHash("PlayCloseMenu");
        
        private static readonly int PlayOpenSettingsParameter = Animator.StringToHash("PlayOpenSettings");
        private static readonly int PlayCloseSettingsParameter = Animator.StringToHash("PlayCloseSettings");

        private static readonly int PlayOpenGarageParameter = Animator.StringToHash("PlayOpenGarage");
        private static readonly int PlayCloseGarageParameter = Animator.StringToHash("PlayCloseGarage");

        public event Action StartPlayOpenMenu;
        public event Action StartPlayIdleMenu;
        public event Action StartPlayCloseMenu;
        
        public event Action StartPlayOpenSettings;
        public event Action StartPlayCloseSettings;
        
        public event Action StartPlayOpenGarage;
        public event Action StartPlayCloseGarage;

        public void StartPlayAnimator(bool isFirstPlay)
        {
            if (isFirstPlay)
                PlayOpenMenu();
            else
                PlayIdleMenu();
        }
        
        public void Rebind() => 
            _animator.Rebind();

        public void PlayOpenMenu()
        {
            _animator.SetTrigger(id: PlayOpenMenuParameter);
            StartPlayOpenMenu?.Invoke();
        }

        public void PlayIdleMenu()
        {
            _animator.SetTrigger(id: PlayIdleMenuParameter);
            StartPlayIdleMenu?.Invoke();
        }

        public void PlayCloseMenu()
        {
            _animator.SetTrigger(id: PlayCloseMenuParameter);
            StartPlayCloseMenu?.Invoke();
        }

        public void PlayOpenSettings()
        {
            _animator.SetTrigger(id: PlayOpenSettingsParameter);
            StartPlayOpenSettings?.Invoke();
        }

        public void PlayCloseSettings()
        {
            _animator.SetTrigger(id: PlayCloseSettingsParameter);
            StartPlayCloseSettings?.Invoke();
        }

        public void PlayOpenGarage()
        {
            _animator.SetTrigger(id: PlayOpenGarageParameter);
            StartPlayOpenGarage?.Invoke();
        }
        
        public void PlayCloseGarage()
        {
            _animator.SetTrigger(id: PlayCloseGarageParameter);
            StartPlayCloseGarage?.Invoke();
        }
    }
}