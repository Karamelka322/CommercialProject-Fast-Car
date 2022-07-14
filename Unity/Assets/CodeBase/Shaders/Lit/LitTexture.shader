Shader "Custom/Lit/Texture"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Emissive ("Emissive", 2D) = "white" {}
    }
    SubShader 
    {
        Tags { "RenderType"="Opaque" "BW" = "TrueProbes" "ForceNoShadowCasting" = "True" }
        LOD 100
        
        CGPROGRAM
        #pragma surface surf Diffuse
        
        sampler2D _MainTex;
        sampler2D _Emissive;
        
        struct Input
        {
            float2 uv_MainTex;
        };

        inline float4 LightingDiffuse (SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            float4 col;
            col.rgb = s.Albedo * _LightColor0.rgb;;
            col.a = s.Alpha;
            
            return col;
        } 

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
            o.Emission = tex2D (_Emissive, IN.uv_MainTex).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}