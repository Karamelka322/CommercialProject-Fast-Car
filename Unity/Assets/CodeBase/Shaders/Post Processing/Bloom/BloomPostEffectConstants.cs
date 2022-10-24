using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Bloom
{
    public static class BloomPostEffectConstants
    {
        public const string ShaderPath = "Custom/Post Effects/Bloom";

        public static readonly int EmissiveTexPropertyID = Shader.PropertyToID(name: "_EmissiveTex");
        public static readonly int ThresholdPropertyID = Shader.PropertyToID(name: "_Threshold");
        public static readonly int OffsetPropertyID = Shader.PropertyToID(name: "_Offset");
    }
}