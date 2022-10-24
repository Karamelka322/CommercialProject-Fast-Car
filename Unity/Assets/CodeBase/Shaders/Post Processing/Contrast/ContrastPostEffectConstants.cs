using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Contrast
{
    public class ContrastPostEffectConstants
    {
        public const string ShaderPath = "Custom/Post Effects/Contrast";
        public static readonly int IntensityPropertyID = Shader.PropertyToID("_Intensity");
    }
}