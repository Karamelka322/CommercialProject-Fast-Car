using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Services.Tween
{
    public class TweenService : ITweenService
    {
        private Dictionary<Type, Coroutine> _dictionary { get; } = new Dictionary<Type, Coroutine>();

        private readonly ICorutineRunner _corutineRunner;

        public TweenService(ICorutineRunner corutineRunner)
        {
            _corutineRunner = corutineRunner;
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

        private void TryStopUnregisterCoroutine<T>()
        {
            if (_dictionary.TryGetValue(typeof(T), out Coroutine coroutine)) 
                StopUnregisterCoroutine<T>(coroutine);
        }

        private void StartRegisterCoroutine<T>(IEnumerator enumerator)
        {
            Coroutine coroutine = _corutineRunner.StartCoroutine(enumerator);
            _dictionary.Add(typeof(T), coroutine);
        }

        private void StopUnregisterCoroutine<T>(Coroutine coroutine)
        {
            _corutineRunner.StopCoroutine(coroutine);
            _dictionary.Remove(typeof(T));
        }

        private static IEnumerator Move(Transform transform, Vector3 position, float speed = 1, TweenMode mode = TweenMode.Global, Action done = null)
        {
            for (float i = 0; i < 1f; i += Time.deltaTime * speed)
            {
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

            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += Time.deltaTime * speed;
                yield return null;
            }
            
            done?.Invoke();
        }
        
        private static IEnumerator Hide(CanvasGroup canvasGroup, float speed = 0, float delay = 0, Action done = null)
        {
            yield return new WaitForSeconds(delay);

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * speed;
                yield return null;
            }

            done?.Invoke();
        }
    }
}