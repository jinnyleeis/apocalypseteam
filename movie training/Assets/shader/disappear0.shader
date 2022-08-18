Shader "Custom/disappear0"
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
            #define BOUNCE_GROUND 0.5
#define BOUNCE 0.2
#define MAX_SPEED 800.0
#define GRAVITY float2(0.0, -5.0)
#define ATTRACTION 60.0



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
            float4 fragColor;




float nrand( float2 n )
{
	return frac(sin(dot(n.xy, float2(12.9898, 78.233)))* 43758.5453);
}







            v2f vert (appdata v)
            {
                v2f i;
                i.vertex = UnityObjectToClipPos(v.vertex);
                i.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                //float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                return i;
            }

            fixed4 frag (v2f i) : SV_Target
            {
            
    float2 pixelSize = 1.0 /i.uv.xy;
    
    // Initialize the tex2D on the first frame with random positions and null velocity.
    
		fragColor = float4(float2(nrand(i.uv), nrand(i.uv.yx)), 0.0 , 0.0);
       
    
    // The tex2D stores the particle position in xy and the velocity in zw.
    float4 previousFrameValues = tex2D(_MainTex, i.uv);
    float2 position = previousFrameValues.xy;
    float2 velocity = previousFrameValues.zw;
 
    // Gravity.
    velocity += GRAVITY;
    
    //Mouse attraction.
   // if (iMouse.w>0.01)
    //{
    	//float2 attractionfloattor = (iMouse.xy/iResolution.xy) - position;
    	//velocity += ATTRACTION * (normalize(attractionfloattor));
   // }
    
    float randValue = nrand(i.uv.yx * _Time.y) * 0.5;
    
    // Collisions
    if (position.x < 0.0)
    {	
        velocity = float2(abs(velocity.x) * (BOUNCE+randValue), velocity.y);
    }

    if (position.x > 1.0)
    {	
        velocity = float2(-abs(velocity.x) * (BOUNCE+randValue), velocity.y);
    }

    if (position.y < 0.0)
    {
        velocity = float2(velocity.x, abs(velocity.y) * (BOUNCE_GROUND+randValue));
    }

    if (position.y > 1.0)
    {
        velocity = float2(velocity.x, -abs(velocity.y) * (BOUNCE+randValue));
    }
    
    // Update position.
    position.xy += velocity * _Time.y * 0.001;
    
    if ( length(velocity) > MAX_SPEED)
        velocity = normalize(velocity) * MAX_SPEED;
    
    
    fragColor = float4(position.xy, velocity.xy);
    return fragColor;

            }
            ENDCG
        }
    }
}
