Shader "Unlit/SimpleShader"
{
    Properties //inputs in to shader, color
    {
        // _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            //mesh data: vertex position, vertex normal, UV's, tangents, vertex colours
            //UV's are the 2d mapping of a texture on a 3d object
            struct VertexInput
            {
                float4 vertex : POSITION;
                float4 colors : COLOR;
                float4 normal : NORMAL;
                float4 tangent : TANGENT;
                
                
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (VertexInput v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return float4(1,1,1,0);
            }
            ENDCG
        }
    }
}
