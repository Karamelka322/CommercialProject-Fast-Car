using UnityEngine;

namespace CodeBase.Post_Processing.Bloom
{
    public static class BloomPostEffectConstants
    {
        public const string ShaderPath = "Custom/PostEffects/Bloom";
        
        private const string EmissionTextureProperty = "_EmissionTex";
        private const string BlurEmissionTextureOffsetProperty = "_BlurEmissionTextureOffset";
        private const string EmissionIntensityProperty = "_EmissionIntensity";

        public const int FragPassGenerateEmissionTexture = 0;
        public const int FragPassBlurTextureDown = 1;
        public const int FragPassBlurTextureUp = 2;
        public const int FragPassCombineEmissionTextureAndMainTexture = 3;
        
        public static readonly int BlurEmissionTextureOffsetPropertyID = Shader.PropertyToID(name: BloomPostEffectConstants.BlurEmissionTextureOffsetProperty);
        public static readonly int EmissionTexturePropertyID = Shader.PropertyToID(name: BloomPostEffectConstants.EmissionTextureProperty);
        public static readonly int EmissionIntensitPropertyID = Shader.PropertyToID(name: BloomPostEffectConstants.EmissionIntensityProperty);
    }
}