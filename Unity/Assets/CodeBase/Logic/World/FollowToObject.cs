using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.World
{
    public class FollowToObject : MonoBehaviour
    {
        public Transform Object;

        [Space] 
        public float Speed;
        
        private IUpdateService _updateService;

        [Inject]
        private void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Start() => 
            _updateService.OnFixedUpdate += OnFixedUpdate;

        private void OnDestroy() => 
            _updateService.OnFixedUpdate -= OnFixedUpdate;

        private void OnFixedUpdate()
        {
            if(Object)
                Movement();
        }

        private void Movement() => 
            transform.position = Vector3.Lerp(transform.position, Object.position, Time.deltaTime * Speed);
    }
}