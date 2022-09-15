using System;
using CodeBase.Extension;
using UnityEngine;

namespace CodeBase.Logic.Audio.Sound
{
    [Serializable]
    public class AudioGroup
    {
        public AudioClip[] AudioClips;

        public AudioClip GetRandomClip() => 
            AudioClips.Random();
    }
}