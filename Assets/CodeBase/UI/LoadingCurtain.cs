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

        public void Show(float speed = 0) => 
            StartCoroutine(ShowCurtain(speed));

        public void Hide(float speed = 0, float delay = 0, Action done = null) =>
            StartCoroutine(HideCurtain(speed, delay, done));

        private IEnumerator ShowCurtain(float speed = 0)
        {
            if (speed == 0)
            {
                _canvasGroup.alpha = 1f;
                yield break;
            }

            while (_canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += Time.deltaTime * speed;
                yield return null;
            }
        }
        
        private IEnumerator HideCurtain(float speed = 0, float delay = 0, Action done = null)
        {
            if (speed == 0)
            {
                _canvasGroup.alpha = 0f;
                yield break;
            }

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