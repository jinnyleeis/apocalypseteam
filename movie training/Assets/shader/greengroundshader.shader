Shader "ShaderCourse/greengroundShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
       
        _U_Params("U Parameters", Vector) = (0,0,0,0)
        _V_Params("V Parameters", Vector) = (0,0,0,0)
    
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }
      

      
        Pass
        {
            HLSLPROGRAM           
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            
           
            #define PI 3.141592653589793

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uv1_uv2 : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;


            
            float2 _U_Params, _V_Params;
          
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex =  TransformObjectToHClip(v.vertex);
                o.uv1_uv2.xy =v.uv.xy*_MainTex_ST.xy+_MainTex_ST.zw;
               
               
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {

                float2 U_Vector = float2(0.5,0.5) - i.uv1_uv2.xy;
                float U = length(U_Vector);
                U = frac(U * _U_Params.x + _Time.x * _U_Params.y) ;
                float2 V_Remap = i.uv1_uv2 * 2 - 1;
                float V = (atan2(V_Remap.x, V_Remap.y) / (2 * PI)) + 0.5; 
                V = frac(V * _V_Params.x + _Time.x * _V_Params.y);
                float2 radialUV = float2(U,V);
                float4 mainTex = tex2D(_MainTex, radialUV);
                         
                return float4(mainTex.rgb,1);

               
            }
            ENDHLSL  
           
        }
    }
}



