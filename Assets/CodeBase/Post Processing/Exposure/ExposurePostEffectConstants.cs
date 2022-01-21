using UnityEngine;

namespace CodeBase.Post_Processing.Exposure
{
    public static class ExposurePostEffectConstants
    {
        public const string ShaderPath = "Custom/PostEffects/Exposure";
        
        private const string IntensityProperty = "_Intensity";

        public static readonly int IntensityPropertyID = Shader.PropertyToID(IntensityProperty);
    }
}