using System;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Scene.Menu
{
    [RequireComponent(typeof(Animator))]
    public class MenuAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private static readonly int PlayIntroParameter = Animator.StringToHash("PlayIntro");
        private static readonly int SkipIntroParameter = Animator.StringToHash("SkipIntro");
        
        public event Action StartPlayIntro;
        public event Action IdleInMenu;
        
        [UsedImplicitly]
        public void PlayIdleEventInMenu() => 
            IdleInMenu?.Invoke();

        public void PlayIntro()
        {
            _animator.SetTrigger(PlayIntroParameter);
            StartPlayIntro?.Invoke();
        }

        public void SkipIntro() => 
            _animator.SetTrigger(SkipIntroParameter);
    }
}