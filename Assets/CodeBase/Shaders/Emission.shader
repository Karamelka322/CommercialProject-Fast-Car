Shader "Custom/Emission"
{
    Properties
    {
        [HDR] _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Cull Back
        
        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Color;

            struct VertexData
            {
                float4 pos : POSITION;
            };

            struct v2f
            {   
                float4 pos : SV_POSITION;
            };

            v2f vert(VertexData v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos);
                return o;
            }

            fixed4 frag() : SV_Target
            {
                return _Color;
            }
            
            ENDCG
        }
    }
}