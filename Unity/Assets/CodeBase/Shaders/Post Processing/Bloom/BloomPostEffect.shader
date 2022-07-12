Shader "Custom/Post Effects/Bloom"
{
    Properties
    {
        [HideInInspector] _MainTex ("Main Texture", 2D) = "white" {}
        [HideInInspector] _EmissiveTex ("Emmisive Texture", 2D) = "white" {}
        
        [HideInInspector] _Offset ("Offset", float) = 0
        [HideInInspector] _Threshold ("_Treshold", float) = 1.0
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
            
            sampler2D _MainTex;
            sampler2D _EmissiveTex;

            float _Offset;
            
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

            half4 frag (v2f i) : SV_Target
            {
                half4 mainTex = tex2D(_MainTex, i.uv);
                half4 emmissionTex = Blur(_EmissiveTex, i.uv);

                return mainTex + emmissionTex;
            }
            ENDCG
        }
    }
}