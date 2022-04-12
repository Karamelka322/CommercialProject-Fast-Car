using UnityEngine;

namespace CodeBase.Post_Processing.Saturation
{
    public static class SaturationPostEffectConstants
    {
        public const string ShaderPath = "Custom/PostEffects/Saturation";
        
        private const string IntensityProperty = "_Intensity";

        public static readonly int IntensityPropertyID = Shader.PropertyToID("_Intensity");
    }
}