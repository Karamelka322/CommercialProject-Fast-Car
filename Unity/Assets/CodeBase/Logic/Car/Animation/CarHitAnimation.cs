using System.Collections;
using CodeBase.Infrastructure;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    public class CarHitAnimation : MonoBehaviour
    {
        [SerializeField] 
        private MeshRenderer _meshRenderer;
        
        [Space]
        [SerializeField] 
        private float _speed;

        [SerializeField] 
        private int _number;

        private ICoroutineRunner _coroutineRunner;
        private static readonly int MainColorPropertyToID = Shader.PropertyToID("_Color");

        [Inject]
        private void Construct(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void Play() => 
            _coroutineRunner.StartCoroutine(PlayAnimation());

        private IEnumerator PlayAnimation()
        {
            for (int i = 0; i < _number; i++)
            {
                for (float j = 0; j < 1; j += Time.deltaTime * _speed)
                {
                    foreach (Material material in _meshRenderer.materials)
                    {
                        material.SetColor(MainColorPropertyToID, Color.Lerp(material.color, Color.red, j));
                    }
                    
                    yield return new WaitForEndOfFrame();
                }
                
                for (float j = 0; j < 1; j += Time.deltaTime * _speed)
                {
                    foreach (Material material in _meshRenderer.materials)
                    {
                        material.SetColor(MainColorPropertyToID, Color.Lerp(material.color, Color.white, j));
                    }
                    
                    yield return new WaitForEndOfFrame();
                }
            }
            
            foreach (Material material in _meshRenderer.materials)
            {
                material.SetColor(MainColorPropertyToID, Color.white);
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
}