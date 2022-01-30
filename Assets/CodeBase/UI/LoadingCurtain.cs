using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show(float speed = 0, float delay = 0, Action done = null)
        {
            if (speed == 0)
            {
                _canvasGroup.alpha = 1f;
                done?.Invoke();
            }
            else
            {
                StartCoroutine(ShowCurtain(speed, delay, done));
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
                StartCoroutine(HideCurtain(speed, delay, done));
            }
        }

        private IEnumerator ShowCurtain(float speed = 0, float delay = 0, Action done = null)
        {
            yield return new WaitForSeconds(delay);

            while (_canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += Time.deltaTime * speed;
                yield return null;
            }
            
            done?.Invoke();
        }
        
        private IEnumerator HideCurtain(float speed = 0, float delay = 0, Action done = null)
        {
            yield return new WaitForSeconds(delay);

            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime * speed;
                yield return null;
            }

            done?.Invoke();
        }
    }
}