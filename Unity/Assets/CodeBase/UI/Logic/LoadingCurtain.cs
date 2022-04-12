using System;
using CodeBase.Services.Tween;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private ITweenService _tweenService;

        private void Awake() => 
            DontDestroyOnLoad(this);

        [Inject]
        public void Construct(ITweenService tweenService)
        {
            _tweenService = tweenService;
        }

        public void Show(float speed = 0, float delay = 0, Action done = null)
        {
            if (speed == 0)
            {
                _canvasGroup.alpha = 1f;
                done?.Invoke();
            }
            else
            {
                _tweenService.Show<LoadingCurtain>(_canvasGroup, speed, delay, done);
            }
        }

        public void Hide(float speed = 0, float delay = 0, Action done = null)
        {
            if (speed == 0)
            {
                _canvasGroup.alpha = 0f;
                done?.Invoke();
            }
            else
            {
                _tweenService.Hide<LoadingCurtain>(_canvasGroup, speed, delay, done);
            }
        }
    }
}