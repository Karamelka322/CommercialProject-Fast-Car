using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.Logic.Audio.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicSource : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        
        [SerializeField] 
        private AudioGroup _audioGroup;
        
        private void Start() => 
            PlayRandomClip();

        private void PlayRandomClip()
        {
            _audioSource.clip = _audioGroup.Clips.Random();
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}