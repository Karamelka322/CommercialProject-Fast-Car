using System;
using UnityEngine;

namespace CodeBase.Services.Tween
{
    public interface ITweenService : IService
    {
        void Move<T>(Transform transform, Vector3 position, float speed = 1, TweenMode mode = TweenMode.Global, Action done = null);
        void Show<T>(CanvasGroup canvasGroup, float speed = 0, float delay = 0, Action done = null);
        void Hide<T>(CanvasGroup canvasGroup, float speed = 0, float delay = 0, Action done = null);
        void Timer<T>(float time, Action callBack);
    }
}