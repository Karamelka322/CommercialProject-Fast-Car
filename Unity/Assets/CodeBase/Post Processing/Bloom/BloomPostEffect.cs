using UnityEngine;

namespace CodeBase.Post_Processing.Bloom
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class BloomPostEffect : MonoBehaviour
    {
        [Range(0f, 5f), SerializeField] 
        private float _bloomIntensity = 1.27f;

        [Range(0f, 5f), SerializeField] 
        private float _blurOffset = 1.25f;
        
        private const int BlurDegree = 1;

        private Material _bloomMaterial;
        private int _bloomPropertyID;

        private SizeValue _screenSize;
        private SizeValue _screenSizeCropped;
        
        private void Awake()
        {
            _bloomMaterial = CreateNewBloomMaterial();
            SetDefaultPropertyInBloomMaterial(_bloomMaterial);
            
            _bloomPropertyID = BloomPostEffectConstants.EmissionTexturePropertyID;

            _screenSize = new SizeValue(Screen.width, Screen.height);
            _screenSizeCropped = new SizeValue(Screen.width >> 1, Screen.height >> 1);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest) => 
            RenderBloomEffect(src, dest, _bloomMaterial);

        private void RenderBloomEffect(Texture src, RenderTexture dest, in Material bloomMaterial)
        {
            RenderTexture emissionTexture = GenerateEmissionTexture(src, bloomMaterial);
            emissionTexture = BlurEmissionTexture(emissionTexture);
            CombineEmissionTextureAndSourceTexture(src, dest, emissionTexture, bloomMaterial);

            RenderTexture.ReleaseTemporary(emissionTexture);
        }

        private RenderTexture GenerateEmissionTexture(Texture src, Material bloomMaterial)
        {
            RenderTexture bloomTexture = GetTemporaryTexture(_screenSizeCropped);
            Graphics.Blit(src, bloomTexture, bloomMaterial, pass: BloomPostEffectConstants.FragPassGenerateEmissionTexture);
            return bloomTexture;
        }

        private RenderTexture BlurEmissionTexture(RenderTexture emissionTexture)
        {
            for (int i = 0; i < BlurDegree; i++)
            {
                emissionTexture = BlurTexture(emissionTexture, _screenSizeCropped, pass: BloomPostEffectConstants.FragPassBlurTextureDown);
                emissionTexture = BlurTexture(emissionTexture, _screenSize, pass: BloomPostEffectConstants.FragPassBlurTextureUp);
            }

            return emissionTexture;
        }

        private void CombineEmissionTextureAndSourceTexture(in Texture src, RenderTexture dest, in Texture emissionTexture, in Material bloomMaterial)
        {
            bloomMaterial.SetTexture(_bloomPropertyID, emissionTexture);
            Graphics.Blit(src, dest, bloomMaterial, pass: BloomPostEffectConstants.FragPassCombineEmissionTextureAndMainTexture);
        }

        private static Material CreateNewBloomMaterial() => 
            new Material(Shader.Find(BloomPostEffectConstants.ShaderPath));

        private void SetDefaultPropertyInBloomMaterial(in Material material)
        {
            material.SetFloat(BloomPostEffectConstants.BlurEmissionTextureOffsetPropertyID, _blurOffset);
            material.SetFloat(BloomPostEffectConstants.EmissionIntensitPropertyID, _bloomIntensity);
        }

        private RenderTexture BlurTexture(RenderTexture bloomTexture, in SizeValue size, in int pass)
        {
            RenderTexture tempTexture = GetTemporaryTexture(size);
            Graphics.Blit(bloomTexture, tempTexture, _bloomMaterial, pass);

            RenderTexture.ReleaseTemporary(bloomTexture);
            bloomTexture = tempTexture;
            bloomTexture.DiscardContents();

            return bloomTexture;
        }

        private static RenderTexture GetTemporaryTexture(in SizeValue sizeTexture)
        {
            RenderTexture tempTexture = RenderTexture.GetTemporary(sizeTexture.Width, sizeTexture.Height, 0, RenderTextureFormat.Default);
            tempTexture.DiscardContents();
            return tempTexture;
        }
    }
}