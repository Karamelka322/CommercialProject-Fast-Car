using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Saturation
{
    public static class SaturationPostEffectConstants
    {
        public const string ShaderPath = "Custom/Post Effects/Saturation";
        public static readonly int IntensityPropertyID = Shader.PropertyToID("_Intensity");
    }
}