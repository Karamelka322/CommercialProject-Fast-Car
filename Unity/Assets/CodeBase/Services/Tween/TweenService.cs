using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Services.Tween
{
    [UsedImplicitly]
    public class TweenService : ITweenService
    {
        private Dictionary<Type, Coroutine> _dictionary { get; } = new Dictionary<Type, Coroutine>();

        private readonly ICorutineRunner _coroutineRunner;

        public TweenService(ICorutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void Move<T>(Transform transform, Vector3 position, float speed = 1, TweenMode mode = TweenMode.Global, Action done = null)
        {
            TryStopUnregisterCoroutine<T>();
            StartRegisterCoroutine<T>(Move(transform, position, speed, mode, done));
        }

        public void Show<T>(CanvasGroup canvasGroup, float speed = 0, float delay = 0, Action done = null)
        {
            TryStopUnregisterCoroutine<T>();
            StartRegisterCoroutine<T>(Show(canvasGroup, speed, delay, done));
        }

        public void Hide<T>(CanvasGroup canvasGroup, float speed = 0, float delay = 0, Action done = null)
        {
            TryStopUnregisterCoroutine<T>();
            StartRegisterCoroutine<T>(Hide(canvasGroup, speed, delay, done));
        }

        public void SingleTimer<T>(float time, Action callBack)
        {
            TryStopUnregisterCoroutine<T>();
            StartRegisterCoroutine<T>(StartTimer(time, callBack));
        }
        
        public void Timer<T>(float time, T component, Action<T> callBack) where T : class => 
            _coroutineRunner.StartCoroutine(StartTimer(time, component, callBack));

        private static IEnumerator StartTimer(float time, Action callBack)
        {
            yield return new WaitForSeconds(time);
            callBack?.Invoke();
        }
        
        private static IEnumerator StartTimer<T>(float time, T component, Action<T> callBack) where T : class
        {
            yield return new WaitForSeconds(time);
            callBack?.Invoke(component);
        }

        private void TryStopUnregisterCoroutine<T>()
        {
            if (_dictionary.TryGetValue(typeof(T), out Coroutine coroutine)) 
                StopUnregisterCoroutine<T>(coroutine);
        }

        private void StartRegisterCoroutine<T>(IEnumerator enumerator)
        {
            Coroutine coroutine = _coroutineRunner.StartCoroutine(enumerator);
            _dictionary.Add(typeof(T), coroutine);
        }

        private void StopUnregisterCoroutine<T>(Coroutine coroutine)
        {
            _coroutineRunner.StopCoroutine(coroutine);
            _dictionary.Remove(typeof(T));
        }

        private static IEnumerator Move(Transform transform, Vector3 position, float speed = 1, TweenMode mode = TweenMode.Global, Action done = null)
        {
            for (float i = 0; i < 1f; i += Time.deltaTime * speed)
            {
                if(transform == null)
                    break;
                
                if(mode == TweenMode.Global)
                {
                    transform.position = Vector3.Lerp(transform.position, position, i);
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, position, i);
                }
                
                yield return null;
            }
            
            done?.Invoke();
        }
        
        private static IEnumerator Show(CanvasGroup canvasGroup, float speed = 0, float delay = 0, Action done = null)
        {
            yield return new WaitForSeconds(delay);

            while (canvasGroup != null && canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += Time.deltaTime * speed;
                yield return null;
            }
            
            done?.Invoke();
        }
        
        private static IEnumerator Hide(CanvasGroup canvasGroup, float speed = 0, float delay = 0, Action done = null)
        {
            yield return new WaitForSeconds(delay);

            while (canvasGroup != null && canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * speed;
                yield return null;
            }

            done?.Invoke();
        }
    }
}