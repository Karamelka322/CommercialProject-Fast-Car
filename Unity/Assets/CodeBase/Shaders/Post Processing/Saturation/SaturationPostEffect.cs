using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Saturation
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class SaturationPostEffect : MonoBehaviour
    {
        [Range(0, 4), SerializeField]
        private float _intensity = 0;

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
            
            SetDefaultPropertyInSaturationMaterial();
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest) => 
            Graphics.Blit(src, dest, _material);

        private static Material CreateNewSaturationMaterial() => 
            new Material(Shader.Find(SaturationPostEffectConstants.ShaderPath));

        private void SetDefaultPropertyInSaturationMaterial() => 
            _material.SetFloat(SaturationPostEffectConstants.IntensityPropertyID, _intensity * 0.01f);
    }
}