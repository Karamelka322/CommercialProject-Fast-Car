Shader "Custom/Post Effects/PostProcessing"
{
    Properties
    {
        [HideInInspector] _MainTex ("Main Texture", 2D) = "white" {}
        [HideInInspector] _EmissiveTex ("Emmisive Texture", 2D) = "white" {}
        
        [HideInInspector] _Offset ("Offset", float) = 0
        [HideInInspector] _Threshold ("_Treshold", float) = 1.0
        
        [HideInInspector] _Saturation ("Saturation", float) = 1.0
        [HideInInspector] _Contrast ("Contrast", float) = 1.0
        [HideInInspector] _Exposure ("Exposure", float) = 1.0
        
        [HideInInspector] _ChanelRed ("_ChanelRed", float) = 0 
        [HideInInspector] _ChanelGreen ("_ChanelGreen", float) = 0 
        [HideInInspector] _ChanelBlue ("_ChanelBlue", float) = 0 
    }
    SubShader
    {
        Cull Off 
        ZWrite Off 
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            sampler2D _MainTex;
            float _Threshold;
            
            half4 GetEmissionTexture(half4 mainTex)
            {
                half brightness = max(mainTex.r, max(mainTex.g, mainTex.b));
			    half contribution = max(0, brightness - _Threshold);

                contribution /= max(brightness, 0.00001);

                return mainTex * contribution;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainTex = tex2D(_MainTex, i.uv);
                return GetEmissionTexture(mainTex);
            }
            
            ENDCG
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _EmissiveTex;

            float _Offset;

            float _Saturation;
            float _Contrast;
            float _Exposure;

            float _ChanelRed;
            float _ChanelGreen;
            float _ChanelBlue;

            half4 Blur(sampler2D tex, in half2 uv)
            {
                half4 tex1 = tex2D(tex, uv + half2(-_Offset, _Offset));
                half4 tex2 = tex2D(tex, uv + half2(_Offset, _Offset));
                half4 tex3 = tex2D(tex, uv + half2(_Offset, -_Offset));
                half4 tex4 = tex2D(tex, uv + half2(-_Offset, -_Offset));

                half4 col = (tex1 + tex2 + tex3 + tex4) / 4;
                
                return col;
            }

            void Contrast(inout half4 color)
            {
                color.rgb = ((color.rgb - 0.5f) * _Contrast + 0.5f) * color.a;
            }

            void ChanelMixer(inout half4 color)
            {
                color.r = color.r * _ChanelRed + color.r + color.r;
            }

            void Exposure(inout half4 color)
            {
                color *= _Exposure;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainTex = tex2D(_MainTex, i.uv);
                half4 emissionTex = Blur(_EmissiveTex, i.uv);

                half4 col = mainTex + emissionTex;
                
                Contrast(col);
                ChanelMixer(col);
                Exposure(col);
                
                return col;
            }
            ENDCG
        }
    }
}