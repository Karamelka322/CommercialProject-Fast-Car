using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Saturation
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class SaturationPostEffect : MonoBehaviour
    {
        [Range(1, 10), SerializeField]
        private float _intensity = 3;

        private Material _saturationMaterial;
        
        private void Awake()
        {
            _saturationMaterial = CreateNewSaturationMaterial();
            SetDefaultPropertyInSaturationMaterial();
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest) => 
            Graphics.Blit(src, dest, _saturationMaterial);

        private static Material CreateNewSaturationMaterial() => 
            new Material(Shader.Find(SaturationPostEffectConstants.ShaderPath));

        private void SetDefaultPropertyInSaturationMaterial() => 
            _saturationMaterial.SetFloat(SaturationPostEffectConstants.IntensityPropertyID, _intensity);
    }
}