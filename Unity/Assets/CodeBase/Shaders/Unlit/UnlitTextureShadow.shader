Shader "Custom/Unlit/Texture (Shadow)"
{
    Properties 
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader 
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        CGPROGRAM
        #pragma surface surf Empty noambient

        sampler2D _MainTex;
        sampler2D _Emissive;
        
        struct Input
        {
            float2 uv_MainTex;
        };

        inline float4 LightingEmpty (SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            float4 col;
            col.rgb = s.Albedo.rgb;
            col.a = s.Alpha;
            
            return col;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}