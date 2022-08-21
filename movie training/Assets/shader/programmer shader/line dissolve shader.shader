Shader "AdultLink/SphereDissolve"
{
	Properties
	{
		_Position("Position", Vector) = (0,0,0,0)
		_Radius("Radius", Float) = 2.16
		[Toggle]_Invert("Invert", Float) = 0
		_Borderradius("Border radius", Range( 0 , 2)) = 0
		[HDR]_Bordercolor("Border color", Color) = (0.8602941,0.2087478,0.2087478,0)
		_Bordernoisescale("Border noise scale", Range( 0 , 20)) = 0
		_Noisespeed("Noise speed", Vector) = (0,0,0,0)
		[NoScaleOffset]_Set1_albedo("Set1_albedo", 2D) = "white" {}
		[HDR]_Set1_albedo_tint("Set1_albedo_tint", Color) = (1,1,1,1)
		[NoScaleOffset][Normal]_Set1_normal("Set1_normal", 2D) = "bump" {}
		[NoScaleOffset]_Set1_emission("Set1_emission", 2D) = "black" {}
		[HDR]_Set1_emission_tint("Set1_emission_tint", Color) = (1,1,1,1)
		[NoScaleOffset]_Set1_metallic("Set1_metallic", 2D) = "white" {}
		_Set1_metallic_multiplier("Set1_metallic_multiplier", Range( 0 , 1)) = 0
		_Set1_smoothness("Set1_smoothness", Range( 0 , 1)) = 0
		_Set1_tiling("Set1_tiling", Vector) = (1,1,0,0)
		_Set1_offset("Set1_offset", Vector) = (0,0,0,0)
		[NoScaleOffset]_Set2_albedo("Set2_albedo", 2D) = "white" {}
		[HDR]_Set2_albedo_tint("Set2_albedo_tint", Color) = (1,1,1,1)
		[NoScaleOffset][Normal]_Set2_normal("Set2_normal", 2D) = "bump" {}
		[NoScaleOffset]_Set2_emission("Set2_emission", 2D) = "black" {}
		[HDR]_Set2_emission_tint("Set2_emission_tint", Color) = (1,1,1,1)
		[NoScaleOffset]_Set2_metallic("Set2_metallic", 2D) = "white" {}
		_Set2_metallic_multiplier("Set2_metallic_multiplier", Range( 0 , 1)) = 0
		_Set2_smoothness("Set2_smoothness", Range( 0 , 1)) = 0
		_Set2_tiling("Set2_tiling", Vector) = (1,1,0,0)
		_Set2_offset("Set2_offset", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _Set1_normal;
		uniform float2 _Set1_tiling;
		uniform float2 _Set1_offset;
		uniform float3 _Position;
		uniform float _Bordernoisescale;
		uniform float3 _Noisespeed;
		uniform float _Radius;
		uniform float _Invert;
		uniform sampler2D _Set2_normal;
		uniform float2 _Set2_tiling;
		uniform float2 _Set2_offset;
		uniform float4 _Set1_albedo_tint;
		uniform sampler2D _Set1_albedo;
		uniform sampler2D _Set2_albedo;
		uniform float4 _Set2_albedo_tint;
		uniform float4 _Set1_emission_tint;
		uniform sampler2D _Set1_emission;
		uniform sampler2D _Set2_emission;
		uniform float4 _Set2_emission_tint;
		uniform float4 _Bordercolor;
		uniform float _Borderradius;
		uniform float _Set1_metallic_multiplier;
		uniform sampler2D _Set1_metallic;
		uniform sampler2D _Set2_metallic;
		uniform float _Set2_metallic_multiplier;
		uniform float _Set1_smoothness;
		uniform float _Set2_smoothness;


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord90 = i.uv_texcoord * _Set1_tiling + _Set1_offset;
			float2 Set1_UVs150 = uv_TexCoord90;
			float3 ase_worldPos = i.worldPos;
			float temp_output_15_0 = distance( _Position , ase_worldPos );
			float simplePerlin3D26 = snoise( ( _Bordernoisescale * ( ase_worldPos + ( _Noisespeed * _Time.y ) ) ) );
			float temp_output_39_0 = ( simplePerlin3D26 + _Radius );
			float temp_output_14_0 = ( 1.0 - saturate( ( temp_output_15_0 / temp_output_39_0 ) ) );
			float temp_output_5_0 = step( temp_output_14_0 , 0.5 );
			float Inverting152 = lerp(0.0,1.0,_Invert);
			float temp_output_4_0 = step( 0.5 , temp_output_14_0 );
			float Set1Mask51 = ( ( temp_output_5_0 * ( 1.0 - Inverting152 ) ) + ( Inverting152 * temp_output_4_0 ) );
			float Set2Mask52 = ( ( temp_output_5_0 * Inverting152 ) + ( ( 1.0 - Inverting152 ) * temp_output_4_0 ) );
			float2 uv_TexCoord96 = i.uv_texcoord * _Set2_tiling + _Set2_offset;
			float2 Set2_UVs136 = uv_TexCoord96;
			float3 Normal146 = ( ( UnpackNormal( tex2D( _Set1_normal, Set1_UVs150 ) ) * Set1Mask51 ) + ( Set2Mask52 * UnpackNormal( tex2D( _Set2_normal, Set2_UVs136 ) ) ) );
			o.Normal = Normal146;
			float4 Albedo145 = ( ( _Set1_albedo_tint * tex2D( _Set1_albedo, Set1_UVs150 ) * Set1Mask51 ) + ( Set2Mask52 * tex2D( _Set2_albedo, Set2_UVs136 ) * _Set2_albedo_tint ) );
			o.Albedo = Albedo145.rgb;
			float Border49 = ( temp_output_5_0 - step( ( 1.0 - saturate( ( temp_output_15_0 / ( _Borderradius + temp_output_39_0 ) ) ) ) , 0.5 ) );
			float4 Emission147 = ( ( _Set1_emission_tint * tex2D( _Set1_emission, Set1_UVs150 ) * Set1Mask51 ) + ( Set2Mask52 * tex2D( _Set2_emission, Set2_UVs136 ) * _Set2_emission_tint ) + ( _Bordercolor * Border49 ) );
			o.Emission = Emission147.rgb;
			float4 Metallic148 = ( ( _Set1_metallic_multiplier * tex2D( _Set1_metallic, Set1_UVs150 ) * Set1Mask51 ) + ( Set2Mask52 * tex2D( _Set2_metallic, Set2_UVs136 ) * _Set2_metallic_multiplier ) );
			o.Metallic = Metallic148.r;
			float Smoothness149 = ( ( _Set1_smoothness * Set1Mask51 ) + ( Set2Mask52 * _Set2_smoothness ) );
			o.Smoothness = Smoothness149;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "AdultLink.SphereDissolveEditor"}