Shader "Custom/Lit/Texture"
{
    Properties
    {
        _Color ("Color", Color) = (0, 0, 0, 0)
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader 
    {
        Tags { "RenderType"="Opaque" "BW" = "TrueProbes" "ForceNoShadowCasting" = "True" }
        LOD 100
        
        CGPROGRAM
        #pragma surface surf Diffuse
        
        sampler2D _MainTex;
        half4 _Color;
        
        struct Input
        {
            float2 uv_MainTex : TEXCOORD0;
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
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex.xy).rgb * _Color.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}