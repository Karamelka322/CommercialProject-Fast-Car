Shader "Custom/PostEffects/Bloom"
{
    Properties
    {
        [HideInInspector] _MainTex ("Main Texture", 2D) = "white" {}
        [HideInInspector] _EmissionTex ("Emission Texture", 2D) = "black" {}
    }

    CGINCLUDE
    
    #include "UnityCG.cginc"

    uniform sampler2D _MainTex;
    uniform half4 _MainTex_TexelSize;
    uniform half4 _MainTex_ST;

    uniform sampler2D _EmissionTex;
    uniform half _BlurEmissionTextureOffset;
    uniform half _EmissionIntensity;

    struct v2fBlurUp
    {
        float4 pos : SV_POSITION;
        half4 uv0 : TEXCOORD0;
        half4 uv1 : TEXCOORD1;
        half4 uv2 : TEXCOORD2;
        half4 uv3 : TEXCOORD3;
    };

    struct v2fBlurDown
    {
        float4 pos : SV_POSITION;
        half2 uv0 : TEXCOORD0;
        half4 uv1 : TEXCOORD1;
        half4 uv2 : TEXCOORD2;
    };

    struct v2fCombineEmissionTextureAndMainTexture
    {
        float4 pos : SV_POSITION;
        half2 uv0 : TEXCOORD0;
        half2 uv1 : TEXCOORD1;
    };

    v2fBlurUp vertexBlurUp(appdata_img v)
    {
        v2fBlurUp o;
        
        const half2 offset = _MainTex_TexelSize.xy * _BlurEmissionTextureOffset;

        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv0.xy = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2( 1.0h, 1.0h) * offset, _MainTex_ST);
        o.uv0.zw = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2(-1.0h, 1.0h) * offset, _MainTex_ST);
        o.uv1.xy = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2(-1.0h, -1.0h) * offset, _MainTex_ST);
        o.uv1.zw = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2( 1.0h, -1.0h) * offset, _MainTex_ST);
        o.uv2.xy = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2( 0.0h, 2.0h) * offset, _MainTex_ST);
        o.uv2.zw = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2( 0.0h, -2.0h) * offset, _MainTex_ST);
        o.uv3.xy = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2( 2.0h, 0.0h) * offset, _MainTex_ST);
        o.uv3.zw = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2(-2.0h, 0.0h) * offset, _MainTex_ST);
        
        return o;
    }
    
    v2fBlurDown vertexBlurDown(appdata_img v)
    {
        v2fBlurDown o;

        const half2 offset = _MainTex_TexelSize.xy * _BlurEmissionTextureOffset;
        
        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv0 = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy, _MainTex_ST);
        o.uv1.xy = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2( 1.0h, 1.0h) * offset, _MainTex_ST);
        o.uv1.zw = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2(-1.0h, 1.0h) * offset, _MainTex_ST);
        o.uv2.xy = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2(-1.0h, -1.0h) * offset, _MainTex_ST);
        o.uv2.zw = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy + half2( 1.0h, -1.0h) * offset, _MainTex_ST);

        return o;
    }

    v2fCombineEmissionTextureAndMainTexture vertexCombineEmissionTextureAndMainTexture(appdata_img v)
    {
        v2fCombineEmissionTextureAndMainTexture o;

        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv0 = UnityStereoScreenSpaceUVAdjust(v.texcoord, _MainTex_ST);

        #if UNITY_UV_STARTS_AT_TOP

        o.uv1 = o.uv0;
        
        if (_MainTex_TexelSize.y < 0.0)
        {
            o.uv0.y = 1.0 - o.uv0.y;
        }
        
        #endif

        return o;
    }

    fixed4 fragBlurDownFirstPass(v2fBlurDown i) : SV_Target
    {
        fixed4 col0 = tex2D(_MainTex, i.uv0);
        fixed4 col1 = tex2D(_MainTex, i.uv1.xy) * 0.25;
        fixed4 col2 = tex2D(_MainTex, i.uv1.zw) * 0.25;
        fixed4 col3 = tex2D(_MainTex, i.uv2.xy) * 0.25;
        fixed4 col4 = tex2D(_MainTex, i.uv2.zw) * 0.25;

        fixed4 col = col0 + col1 + col2 + col3 + col4;
        return max((col * 0.5) - 1, 0.0);
    }

    fixed4 fragBlurDown(v2fBlurDown i) : SV_Target
    {
        fixed4 col0 = tex2D(_MainTex, i.uv0);
        fixed4 col1 = tex2D(_MainTex, i.uv1.xy) * 0.25;
        fixed4 col2 = tex2D(_MainTex, i.uv1.zw) * 0.25;
        fixed4 col3 = tex2D(_MainTex, i.uv2.xy) * 0.25;
        fixed4 col4 = tex2D(_MainTex, i.uv2.zw) * 0.25;

        return (col0 + col1 + col2 + col3 + col4) * 0.5;
    }

    #define oneThree   0.3333333
    #define oneSix     0.1666666

    fixed4 fragBlurUp(v2fBlurUp i) : SV_Target
    {
        fixed4 col1 = tex2D(_MainTex, i.uv0.xy) * oneThree;
        fixed4 col2 = tex2D(_MainTex, i.uv0.zw) * oneThree;
        fixed4 col3 = tex2D(_MainTex, i.uv1.xy) * oneThree;
        fixed4 col4 = tex2D(_MainTex, i.uv1.zw) * oneThree;
        fixed4 col5 = tex2D(_MainTex, i.uv2.xy) * oneSix;
        fixed4 col6 = tex2D(_MainTex, i.uv2.zw) * oneSix;
        fixed4 col7 = tex2D(_MainTex, i.uv3.xy) * oneSix;
        fixed4 col8 = tex2D(_MainTex, i.uv3.zw) * oneSix;

        return col1 + col2 + col3 + col4 + col5 + col6 + col7 + col8;
    }
    
    fixed4 fragCombineEmissionTextureAndMainTexture(v2fCombineEmissionTextureAndMainTexture i) : SV_Target
    {
        fixed4 col = tex2D(_MainTex, i.uv1);
        return col + tex2D(_EmissionTex, i.uv0) * _EmissionIntensity;
    }
    
    ENDCG

    SubShader
    {
        Cull Back
        ZWrite Off
        ZTest LEqual
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vertexBlurDown
            #pragma fragment fragBlurDownFirstPass
            ENDCG
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vertexBlurDown
            #pragma fragment fragBlurDown
            ENDCG
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vertexBlurUp
            #pragma fragment fragBlurUp
            ENDCG
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vertexCombineEmissionTextureAndMainTexture
            #pragma fragment fragCombineEmissionTextureAndMainTexture
            ENDCG
        }
    }
}