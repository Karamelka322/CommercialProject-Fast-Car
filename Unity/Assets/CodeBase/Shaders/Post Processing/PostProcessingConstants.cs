using UnityEngine;

namespace CodeBase.Shaders.Post_Processing
{
    public static class PostProcessingConstants
    {
        public const string ShaderPath = "Custom/Post Effects/PostProcessing";

        public static readonly int EmissiveTexPropertyID = Shader.PropertyToID(name: "_EmissiveTex");
        
        public static readonly int ContrastPropertyID = Shader.PropertyToID(name: "_Contrast");
        public static readonly int ExposurePropertyID = Shader.PropertyToID(name: "_Exposure");
        public static readonly int ChanelRedPropertyID = Shader.PropertyToID(name: "_ChanelRed");
    }
}