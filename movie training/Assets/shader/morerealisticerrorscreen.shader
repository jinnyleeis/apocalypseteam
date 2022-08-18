Shader "Unlit/morerealisticerrorscreen"
{
    Properties
    {
        _MainTex ("tex2D", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #define  mod(x,y)  (x-y*floor(x/y))


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;


            


            /**
 Just messing around with different types of glitching effects.
*/

// try commenting/uncommenting these to isolate/combine different glitch effects.
#define ANALOG
#define DIGITAL
#define CRT


// amount of seconds for which the glitch loop occurs
#define DURATION 5.0
// percentage of the duration for which the glitch is triggered
#define AMT 0.5 
//uniform int       iFrame;                // shader playback frame
#define SS(a, b, x) (smoothstep(a, b, x) * smoothstep(b, a, x))

#define UI0 1597334673U
#define UI1 3812015801U
#define UI2 float2(UI0, UI1)
#define UI3 float3(UI0, UI1, 2798796415U)
#define UIF (1. / float(0xffffffffU))

// Hash by David_Hoskins
float3 hash33(float3 p)
{
	float3 q = float3(p) * UI3;
	//q = (q.x ^ q.y ^ q.z)*UI3;
	return -1. + 2. * float3(q) * UIF;
}

// Gradient noise by iq
float gnoise(float3 x)
{
    // grid
    float3 p = floor(x);
    float3 w = frac(x);
    
    // quintic interpolant
    float3 u = w * w * w * (w * (w * 6. - 15.) + 10.);
    
    // gradients
    float3 ga = hash33(p + float3(0., 0., 0.));
    float3 gb = hash33(p + float3(1., 0., 0.));
    float3 gc = hash33(p + float3(0., 1., 0.));
    float3 gd = hash33(p + float3(1., 1., 0.));
    float3 ge = hash33(p + float3(0., 0., 1.));
    float3 gf = hash33(p + float3(1., 0., 1.));
    float3 gg = hash33(p + float3(0., 1., 1.));
    float3 gh = hash33(p + float3(1., 1., 1.));
    
    // projections
    float va = dot(ga, w - float3(0., 0., 0.));
    float vb = dot(gb, w - float3(1., 0., 0.));
    float vc = dot(gc, w - float3(0., 1., 0.));
    float vd = dot(gd, w - float3(1., 1., 0.));
    float ve = dot(ge, w - float3(0., 0., 1.));
    float vf = dot(gf, w - float3(1., 0., 1.));
    float vg = dot(gg, w - float3(0., 1., 1.));
    float vh = dot(gh, w - float3(1., 1., 1.));
	
    // interpolation
    float gNoise = va + u.x * (vb - va) + 
           		u.y * (vc - va) + 
           		u.z * (ve - va) + 
           		u.x * u.y * (va - vb - vc + vd) + 
           		u.y * u.z * (va - vc - ve + vg) + 
           		u.z * u.x * (va - vb - ve + vf) + 
           		u.x * u.y * u.z * (-va + vb + vc - vd + ve - vf - vg + vh);
    
    return 2. * gNoise;
}

// gradient noise in range [0, 1]
float gnoise01(float3 x)
{
	return .5 + .5 * gnoise(x);   
}

// warp uvs for the crt effect
float2 crt(float2 uv)
{
    float tht  = atan2(uv.y, uv.x);
    float r = length(uv);
    // curve without distorting the center
    r /= (1. - .1 * r * r);
    uv.x = r * cos(tht);
    uv.y = r * sin(tht);
    return .5 * (uv + 1.);
}

            v2f vert (appdata v)
            {
                v2f i;
                i.vertex = UnityObjectToClipPos(v.vertex);
                i.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(i,i.vertex);
                return i;
            }

            

 


float4 frag (v2f i) : SV_Target
{
    float2 uv = i.uv / i.uv.xy;
    float t = _Time.y;
    
    // smoothed interval for which the glitch gets triggered
    float glitchAmount = SS(DURATION * 0.001, DURATION * AMT, mod(t, DURATION));  
	float displayNoise = 0;
    float3 col=float3(0,0,0);
    float2 eps = float2(5. / i.uv.x, 0.);
    float2 st;

#ifdef CRT
	uv = crt(uv * 2. - 1.); // warped uvs
    ++displayNoise;
#endif
    // analog distortion
    float y = uv.y * i.uv.y;
    float distortion = gnoise(float3(0., y * .01, t * 500.)) * (glitchAmount * 4. + .1);
    distortion *= gnoise(float3(0., y * .02, t * 250.)) * (glitchAmount * 2. + .025);
#ifdef ANALOG
    ++displayNoise;
    distortion += smoothstep(.999, 1., sin((uv.y + t * 1.6) * 2.)) * .02;
    distortion -= smoothstep(.999, 1., sin((uv.y + t) * 2.)) * .02;
    st = uv + float2(distortion, 0.);
    // chromatic aberration
    col.r += tex2Dlod(_MainTex, float4(st + eps + distortion,0,0)).r;
    col.g += tex2Dlod(_MainTex, float4(st,0,0)).g;
    col.b += tex2Dlod(_MainTex, float4(st - eps - distortion,0,0)).b;
#else
    col += tex2D(_MainTex, uv).xyz;
#endif
    
#ifdef DIGITAL
    // blocky distortion
    float bt = floor(t * 30) * 300.;
    float blockGlitch = 0.2 + 0.9 * glitchAmount;
    float blockNoiseX = step(gnoise01(float3(0., uv.x * 3., bt)), blockGlitch);
    float blockNoiseX2 = step(gnoise01(float3(0., uv.x * 1.5, bt * 1.2)), blockGlitch);
    float blockNoiseY = step(gnoise01(float3(0., uv.y * 4, bt)), blockGlitch);
    float blockNoiseY2 = step(gnoise01(float3(0., uv.y * 6., bt * 1.2)), blockGlitch);
    float block = blockNoiseX2 * blockNoiseY2 + blockNoiseX * blockNoiseY;
    st = float2(uv.x + sin(bt) * hash33(float3(uv, .5)).x, uv.y);
    col *= 1. - block;
    block *= 1.15;
    col.r += tex2Dlod(_MainTex, float4(st + eps,0,0 )).r * block;
    col.g += tex2Dlod(_MainTex,float4(st, 0,0)).g * block;
    col.b += tex2Dlod(_MainTex, float4(st - eps, 0,0)).b * block;
#endif
    // white noise + scanlines
    displayNoise = clamp(displayNoise, 0, 1);
    col += (0.15 + 0.65 * glitchAmount) * (hash33(float3(i.uv,1))) * displayNoise;
    	//	1000.))).r) * displayNoise;
    col -= (.25 + .75 * glitchAmount) * (sin(4. * t + uv.y * i.uv.y * 1.75))* displayNoise;
#ifdef CRT
   
    float vig = 8.0 * uv.x * uv.y * (1.-uv.x) * (1.-uv.y);
	col *= (pow(vig,0.25)) * 1.5;
    if(uv.x < 0. || uv.x > 1.) col *= 0.;
#endif
   return float4(col, 1.0);
}
            ENDCG
        }


    }
}

