using CodeBase.Logic.Player;
using CodeBase.Logic.World;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Audio.Sound
{
    public class SoundSourceArea : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField] 
        private Area _area;

        [SerializeField] 
        private float _outputTime;

        [SerializeField] 
        private AudioGroup _audioGroup;

        private IUpdateService _updateService;
        private float _time;

        [Inject]
        private void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }
        
        private void Start()
        {
            _area.OnAreaEnter += OnAreaEnter;
            _area.OnAreaExit += OnAreaExit;
        }

        private void OnDestroy()
        {
            _area.OnAreaEnter -= OnAreaEnter;
            _area.OnAreaExit -= OnAreaExit;
            _updateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            _time += Time.deltaTime;

            if (_time >= _outputTime)
            {
                PlaySound();
                _time = 0;
            }
        }

        private void OnAreaEnter(Collider obj)
        {
            if (obj.TryGetComponent(out PlayerPrefab player)) 
                _updateService.OnUpdate += OnUpdate;
        }

        private void OnAreaExit(Collider obj)
        {
            if (obj.TryGetComponent(out PlayerPrefab player))
            {
                _updateService.OnUpdate -= OnUpdate;
                _time = 0;
            }
        }

        private void PlaySound() => 
            _audioSource.PlayOneShot(_audioGroup.GetRandomClip());
    }
}