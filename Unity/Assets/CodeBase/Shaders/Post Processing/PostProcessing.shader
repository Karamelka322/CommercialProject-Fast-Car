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
            
            half4 GetEmmisionTexture(half4 mainTex)
            {
                half brightness = max(mainTex.r, max(mainTex.g, mainTex.b));
			    half contribution = max(0, brightness - _Threshold);

                contribution /= max(brightness, 0.00001);

                return mainTex * contribution;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainTex = tex2D(_MainTex, i.uv);
                return GetEmmisionTexture(mainTex);
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

            const float e = 1.0e-10;
            
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
                float alpha = 0;
                
                half4 tex1 = tex2D(tex, uv + half2(_Offset, 0));
                half4 tex2 = tex2D(tex, uv + half2(0, _Offset));
                half4 tex3 = tex2D(tex, uv + half2(-_Offset, 0));
                half4 tex4 = tex2D(tex, uv + half2(0, -_Offset));

                half4 tex5 = tex2D(tex, uv + half2(-_Offset, _Offset));
                half4 tex6 = tex2D(tex, uv + half2(_Offset, _Offset));
                half4 tex7 = tex2D(tex, uv + half2(_Offset, -_Offset));
                half4 tex8 = tex2D(tex, uv + half2(-_Offset, -_Offset));

                half4 col = half4(tex1.rgb > tex2.rgb ? tex1.rgb : tex2.rgb, alpha);
                col = half4(col.rgb > tex3.rgb ? col.rgb : tex3.rgb, alpha);
                col = half4(col.rgb > tex4.rgb ? col.rgb : tex4.rgb, alpha);
                
                col = half4(col.rgb > tex5.rgb ? col.rgb : tex5.rgb, alpha);
                col = half4(col.rgb > tex6.rgb ? col.rgb : tex6.rgb, alpha);
                col = half4(col.rgb > tex7.rgb ? col.rgb : tex7.rgb, alpha);
                col = half4(col.rgb > tex8.rgb ? col.rgb : tex8.rgb, alpha);
                
                return col;
            }

            float3 rgb2hsv(float3 c)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = c.g < c.b ? float4(c.bg, K.wz) : float4(c.gb, K.xy);
                float4 q = c.r < p.x ? float4(p.xyw, c.r) : float4(c.r, p.yzx);

                float d = q.x - min(q.w, q.y);

                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            float3 hsv2rgb(float3 c)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
            }

            void Saturation(inout half4 color, float intensity)
            {
                half3 hsvCol = rgb2hsv(color);
                hsvCol.y += intensity;
                color = fixed4(hsv2rgb(hsvCol), 1.0);
            }

            void Contrast(inout half4 color, float intensity)
            {
                color.rgb = (color.rgb - 0.5f) * intensity + 0.5f;
                color.rgb *= color.a;
            }

            void Exposure(inout half4 color, float intensity)
            {
                color *= intensity;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainTex = tex2D(_MainTex, i.uv);
                half4 emissionTex = Blur(_EmissiveTex, i.uv);

                half4 col = mainTex + emissionTex;
                
                Saturation(col, _Saturation);
                Contrast(col, _Contrast);

                col.r = col.r * _ChanelRed + col.r * _ChanelGreen + col.r * _ChanelBlue;

                Exposure(col, _Exposure);
                
                return col;
            }
            ENDCG
        }
    }
}