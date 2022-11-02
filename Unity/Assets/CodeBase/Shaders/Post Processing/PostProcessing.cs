using UnityEngine;

namespace CodeBase.Shaders.Post_Processing
{
    [DisallowMultipleComponent, RequireComponent(typeof(Camera)), ExecuteInEditMode, ImageEffectAllowedInSceneView]
    public class PostProcessing : MonoBehaviour
    {
        [SerializeField, Range(0, 2)] private float _contrast = 0.98f;
        [SerializeField, Range(0, 2)] private float _exposure = 1.3f;
        [SerializeField, Range(-1, 2)] private float _chanelRed = 0.6f;

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

#if UNITY_EDITOR
        
        private void OnPreRender() => 
            Awake();

#endif

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            RenderTexture emissiveTexture = RenderTexture.GetTemporary(src.width / 2, src.height / 2);
            Graphics.Blit(src, emissiveTexture, _material, 0);

            _material.SetTexture(PostProcessingConstants.EmissiveTexPropertyID, emissiveTexture);
            Graphics.Blit(src, dest, _material, 1);
            
            RenderTexture.ReleaseTemporary(emissiveTexture);
        }

        private void CreateMaterial() => 
            _material = new(Shader.Find(PostProcessingConstants.ShaderPath));

        private void SetProperty()
        {
            _material.SetFloat(PostProcessingConstants.ContrastPropertyID, _contrast);
            _material.SetFloat(PostProcessingConstants.ExposurePropertyID, _exposure);
            _material.SetFloat(PostProcessingConstants.ChanelRedPropertyID, _chanelRed);
        }
    }
}