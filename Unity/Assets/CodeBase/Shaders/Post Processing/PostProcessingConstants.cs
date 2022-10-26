using UnityEngine;

namespace CodeBase.Shaders.Post_Processing
{
    public static class PostProcessingConstants
    {
        public const string ShaderPath = "Custom/Post Effects/PostProcessing";

        public static readonly int EmissiveTexPropertyID = Shader.PropertyToID(name: "_EmissiveTex");
        
        public static readonly int ThresholdPropertyID = Shader.PropertyToID(name: "_Threshold");
        public static readonly int OffsetPropertyID = Shader.PropertyToID(name: "_Offset");
        
        public static readonly int SaturationPropertyID = Shader.PropertyToID(name: "_Saturation");
        public static readonly int ContrastPropertyID = Shader.PropertyToID(name: "_Contrast");
        public static readonly int ExposurePropertyID = Shader.PropertyToID(name: "_Exposure");
        
        public static readonly int ChanelRedPropertyID = Shader.PropertyToID(name: "_ChanelRed");
        public static readonly int ChanelGreenPropertyID = Shader.PropertyToID(name: "_ChanelGreen");
        public static readonly int ChanelBluePropertyID = Shader.PropertyToID(name: "_ChanelBlue");
    }
}