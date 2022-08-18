Shader "Unlit/disintegration"
{
    Properties
    {
        tintcolor("tint color",COLOR)=(1,1,1)
        _MainTex ("tex2D", 2D) = "white" {}
       // [Enum(UnityEngine.Rendering.BlendMode)]
       // _SrcFactor("src factor",Float)=0
       // [Enum(UnityEngine.Rendering.BlendMode)]
       // _DstFactor("dst factor",Float)=0
       // [Enum(UnityEngine.Rendering.BlendOp)]
       // _Opp("opp factor",Float)=0

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100


       // Blend [_SrcFactor] [_DstFactor] 
       // BlendOp [_Opp]


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            #define  mod(x,y)  (x-y*floor(x/y))
            #define NB_PARTICLES 40
            #define PARTICLE_SIZE 0.006
            #define OPACITY 2.0

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 p;
            float4 tintcolor;










            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                //float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 pixelSize = 1.0 / i.uv.xy;
    
	float2 uv = i.uv.xy * pixelSize;
    
    float3 finalColor = float3(0,0,0);
    
    int resx = int(i.uv.x);
    int resy = int(i.uv.y);
    
    // Read each pixels of the position tex2D to find if some of them are 
    // close from the current pixel.
    for (int x = 0; x < NB_PARTICLES; x++)
    {
        for (int y = 0; y < NB_PARTICLES; y++)
        {
            // This is the bottleneck of the shader, there might be a
            // better way to read the particle tex2Ds.
            float4 currentParticle = tex2D(_MainTex, float2(x, y) * pixelSize);
            float2 particlePixelfloattor = currentParticle.xy - uv;
            
            // If a particle is close to this pixel, add its color to the final color.
            if (particlePixelfloattor.x * particlePixelfloattor.x + particlePixelfloattor.y * particlePixelfloattor.y  < pixelSize.x * PARTICLE_SIZE)
            {
                float val = (currentParticle.z * currentParticle.z + currentParticle.w * currentParticle.w) *0.000001;
                float3 velocityColor = float3(-0.25 + val, 0.1, 0.25-val) * OPACITY;
                finalColor += velocityColor.xyz;
            } 
        }
    }
    
	return float4(finalColor, 1.0);


            }
            ENDCG
        }
    }
}
