Shader "Custom/Decal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags{ "RenderType"="Transparent" "Queue"="Transparent" "DisableBatching"="True"}
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite off
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct VertexData
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float4 screenPosition : TEXCOORD0;
                float3 ray : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            sampler2D_float _CameraDepthTexture;
            
            v2f vert (VertexData v)
            {
                v2f o;
                
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.position = UnityWorldToClipPos(worldPos);
                o.ray = worldPos - _WorldSpaceCameraPos;
                o.screenPosition = ComputeScreenPos(o.position);

                return o;
            }

            float3 GetProgectedPbjectPosition(float2 screenPos, float3 worldRay)
            {
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenPos);
                depth = Linear01Depth(depth) * _ProjectionParams.z;

                worldRay = normalize(worldRay);
                worldRay /= dot(worldRay, -UNITY_MATRIX_V[2].xyz);
                
                float3 worldPos = _WorldSpaceCameraPos + worldRay * depth;
	            float3 objectPos =  mul (unity_WorldToObject, float4(worldPos, 1)).xyz;

                clip(0.5 - abs(objectPos));
                
                objectPos += 0.5f;

                return objectPos;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float2 screenUV = i.screenPosition.xy / i.screenPosition.w;
                float2 uv = GetProgectedPbjectPosition(screenUV, i.ray).xz;

                float4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
