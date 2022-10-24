using UnityEngine;

namespace CodeBase.Shaders.Post_Processing
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class PostProcessing : MonoBehaviour
    {
        [SerializeField] private float _threshold = 0.4f;
        [SerializeField] private float _offset = 0.001f;

        [SerializeField, Range(0, 4)] private float _saturation = 2;
        [SerializeField, Range(0, 2)] private float _contrast = 0.99f;

        private Material _material;

        private void Awake()
        { 
            CreateMaterial();
            SetProperty();
        }

        private void OnValidate()
        {
            if(_material == false)
                return;
            
            SetProperty();
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            RenderTexture emissiveTexture = RenderTexture.GetTemporary(src.width, src.height);
            Graphics.Blit(src, emissiveTexture, _material, 0);
            
            _material.SetTexture(PostProcessingConstants.EmissiveTexPropertyID, emissiveTexture);
            Graphics.Blit(src, dest, _material, 1);
            
            RenderTexture.ReleaseTemporary(emissiveTexture);
        }

        private void CreateMaterial() => 
            _material = new(Shader.Find(PostProcessingConstants.ShaderPath));

        private void SetProperty()
        {
            _material.SetFloat(PostProcessingConstants.ThresholdPropertyID, _threshold);
            _material.SetFloat(PostProcessingConstants.OffsetPropertyID, _offset);
            _material.SetFloat(PostProcessingConstants.SaturationPropertyID, _saturation * 0.01f);
            _material.SetFloat(PostProcessingConstants.ContrastPropertyID, _contrast);
        }
    }
}