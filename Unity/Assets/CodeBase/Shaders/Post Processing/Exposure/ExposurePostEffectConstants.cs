using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Exposure
{
    public static class ExposurePostEffectConstants
    {
        public const string ShaderPath = "Custom/Post Effects/Exposure";
        public static readonly int IntensityPropertyID = Shader.PropertyToID("_Intensity");
    }
}