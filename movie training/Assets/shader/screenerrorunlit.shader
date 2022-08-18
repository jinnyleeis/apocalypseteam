Shader "Unlit/screenerrorunlit"
{
    Properties
    {
        tintcolor("tint color",COLOR)=(1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
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



    float hash(float2 p) {
	float h = dot(p,float2(127.1,311.7));
   //return -h + 2.0*frac(sin(h)*43758.5453123);
	return -1.0 + 2.0*frac(sin(h)*43758.5453123);
   // return frac(sin(h)*43758.5453123);
}

float noise1(float2 p) {
	float2 i = floor(p);
	float2 f = frac(p);

	float2 u = f*f*(3.0-2.0*f);

	return lerp(lerp(hash( i + float2(0.0,0.0) ), 
		hash( i + float2(1.0,0.0) ), u.x),
        lerp( hash( i + float2(0.0,1.0) ), 
		hash( i + float2(1.0,1.0) ), u.x), u.y);
}

float noise2(float2 p, int oct) {
	float m = float2x2( 1.6,  1.2, -1.2,  1.6 );
	float f  = 0.0;

	for(int i = 1; i < 3; i++){
		float mul = 1.0/pow(2.0, float(i));
		f += mul*noise1(p); 
        //f = mul*noise1(p); 
		p = m*p;
	}

	return f;
}




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
               float2 uv = i.uv;

    float glitch = pow(cos(_Time.y*0.5)*1.2+1.0, 1.2);
    //float glitch = pow(sin(_Time.y*0.5)*1.2+1.0, 1.2);
    
    
    if(noise1(_Time.y+float2(0, 0))*glitch > 0.62){
		uv.y = mod(uv.y+noise1(float2(_Time.y*4.0, 0)), 1.0);
       // uv.x = mod(uv.x+noise1(float2(_Time.y*4.0, 0)), 1.0);
	}


	float2 hp = float2(0.0, uv.y);
	float nh = noise2(hp*7.0+_Time.y*10.0, 3) * (noise1(hp+_Time.y*0.3)*0.8);
	nh += noise2(hp*100.0+_Time.y*10.0, 3)*0.02;
	float rnd = 0.0;
	if(glitch > 0.0){
		rnd = hash(uv);
		if(glitch < 1.0){
			rnd *= glitch;
		}
	}
	nh *= glitch + rnd;
	float r = tex2D(_MainTex, uv+float2(nh, 0.08)*nh).r;
	float g = tex2D(_MainTex, uv+float2(nh-0.07, 0.0)*nh).g;
	float b = tex2D(_MainTex, uv+float2(nh, 0.0)*nh).b;

    //float r = tex2D(_MainTex, uv+float2(nh, 0.08)*nh).r;
	//float g = tex2D(_MainTex, uv+float2(nh, 0.0)*nh).g;
	//float b = tex2D(_MainTex, uv+float2(nh, 0.0)*nh).b;

	float3 col = float3(r, g, b);
	return float4(col.rgb, 1.0)*tintcolor;
            }
            ENDCG
        }
    }
}
