using UnityEngine;

namespace CodeBase.Post_Processing.Exposure
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class ExposurePostEffect : MonoBehaviour
    {
        [Range(1, 10), SerializeField]
        private float _intensity = 3;
        
        private Material _saturationMaterial;
        
        private void Awake()
        {
            _saturationMaterial = CreateNewExposureMaterial();
            SetDefaultPropertyInExposureMaterial();
        }
        
        private void OnRenderImage(RenderTexture src, RenderTexture dest) => 
            Graphics.Blit(src, dest, _saturationMaterial);

        private static Material CreateNewExposureMaterial() => 
            new Material(Shader.Find(ExposurePostEffectConstants.ShaderPath));

        private void SetDefaultPropertyInExposureMaterial() => 
            _saturationMaterial.SetFloat(ExposurePostEffectConstants.IntensityPropertyID, _intensity);
    }
}