using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Exposure
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class ExposurePostEffect : MonoBehaviour
    {
        [Range(1, 10), SerializeField]
        private float _intensity = 3;
        
        private Material _material;
        
        private void Awake()
        {
            _material = CreateNewExposureMaterial();
            SetDefaultPropertyInExposureMaterial();
        }

        private void OnValidate()
        {
            if(_material == false)
                return;
            
            SetDefaultPropertyInExposureMaterial();
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest) => 
            Graphics.Blit(src, dest, _material);

        private static Material CreateNewExposureMaterial() => 
            new Material(Shader.Find(ExposurePostEffectConstants.ShaderPath));

        private void SetDefaultPropertyInExposureMaterial() => 
            _material.SetFloat(ExposurePostEffectConstants.IntensityPropertyID, _intensity);
    }
}