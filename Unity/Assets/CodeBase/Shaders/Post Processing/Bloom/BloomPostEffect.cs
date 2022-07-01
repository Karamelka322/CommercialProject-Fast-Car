using UnityEngine;

namespace CodeBase.Shaders.Post_Processing.Bloom
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class BloomPostEffect : MonoBehaviour
    {
        [SerializeField] private float _treshold = 0.4f;
        [SerializeField] private float _offset = 0.001f;

        private Material _material;

        private void Awake() => 
            CreateMaterial();

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            RenderTexture emmisiveTexture = RenderTexture.GetTemporary(src.width, src.height);
            Graphics.Blit(src, emmisiveTexture, _material, 0);
            
            _material.SetTexture(BloomPostEffectConstants.EmmisiveTexPropertyID, emmisiveTexture);
            Graphics.Blit(src, dest, _material, 1);
            
            RenderTexture.ReleaseTemporary(emmisiveTexture);
        }

        private void CreateMaterial()
        {
            _material = new Material(Shader.Find(BloomPostEffectConstants.ShaderPath));

            _material.SetFloat(BloomPostEffectConstants.ThresholdPropertyID, _treshold);
            _material.SetFloat(BloomPostEffectConstants.OffsetPropertyID, _offset);
        }
    }
}