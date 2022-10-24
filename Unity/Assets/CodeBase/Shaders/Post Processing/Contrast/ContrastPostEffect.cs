using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Contrast
{
    public class ContrastPostEffect : MonoBehaviour
    {
        [Range(0, 2), SerializeField]
        private float _intensity = 1;

        private Material _material;
        
        private void Awake()
        {
            _material = CreateNewSaturationMaterial();
            SetDefaultPropertyInSaturationMaterial();
        }

        private void OnValidate()
        {
            if(_material == false)
                return;
            
            _material.SetFloat(ContrastPostEffectConstants.IntensityPropertyID, _intensity);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest) => 
            Graphics.Blit(src, dest, _material);

        private static Material CreateNewSaturationMaterial() => 
            new Material(Shader.Find(ContrastPostEffectConstants.ShaderPath));

        private void SetDefaultPropertyInSaturationMaterial() => 
            _material.SetFloat(ContrastPostEffectConstants.IntensityPropertyID, _intensity);
    }
}