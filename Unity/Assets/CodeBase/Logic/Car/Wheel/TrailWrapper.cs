using CodeBase.Infrastructure;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailWrapper : MonoBehaviour, IWrapper, IPauseHandler, IReplayHandler
    {
        [SerializeField] 
        private TrailRenderer _trailRenderer;

        public bool Enabled
        {
            get => _trailRenderer.enabled;
            set => _trailRenderer.enabled = value;
        }
        
        private IUpdatable _updatable;
        private TrailBackup _backup;

        private float _lifeTime;

        [Inject]
        private void Construct(IUpdatable updatable)
        {
            _updatable = updatable;
        }

        private void Awake()
        {
            _backup = new TrailBackup(transform.parent,
                transform.localPosition, transform.localRotation,
                _trailRenderer.time, _trailRenderer.enabled);
        }

        public void Reset()
        {
            transform.parent = _backup.Parent;
            transform.localPosition = _backup.LocalPosition;
            transform.localRotation = _backup.LocalRotation;
            
            _trailRenderer.enabled = _backup.Enable;
            _trailRenderer.time = _backup.Time;
            _trailRenderer.Clear();

            _lifeTime = 0;
        }

        public void ResetAfterStoppingDrawing() => 
            _updatable.OnUpdate += OnReset;

        public void OnEnabledPause()
        {
            if(_trailRenderer.enabled)
                _updatable.OnUpdate += OnPause;
        }

        public void OnDisabledPause()
        {
            if(_trailRenderer.enabled)
                _updatable.OnUpdate -= OnPause;
        }

        public void OnReplay()
        {
            _updatable.OnUpdate -= OnReset;
            _updatable.OnUpdate -= OnPause;
            
            Reset();
        }

        private void OnPause() => 
            _trailRenderer.time += Time.deltaTime;

        private void OnReset()  
        {
            if (_lifeTime >= _trailRenderer.time)
            {
                _updatable.OnUpdate -= OnReset;
                Reset();
            }
            else
            {
                _lifeTime += Time.deltaTime;
            }
        }

        private class TrailBackup
        {
            public readonly Transform Parent;
            public readonly Vector3 LocalPosition;
            public readonly Quaternion LocalRotation;

            public readonly float Time;
            public readonly bool Enable;

            public TrailBackup(Transform parent, Vector3 localPosition, Quaternion localRotation, float time, bool enable)
            {
                Enable = enable;
                Parent = parent;
                Time = time;
                LocalPosition = localPosition;
                LocalRotation = localRotation;
            }
        }
    }
}