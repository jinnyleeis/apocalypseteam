Shader "custom/project1"
{
   Properties {
   
     _TintColor("Test Color", color) = (1, 1, 1, 1)
	
	// _MainTex("Main Texture", 2D) = "white" {}
      	
       	}  

	SubShader
	{  	
	Tags
            {
	    "RenderPipeline"="UniversalPipeline"
                "RenderType"="Opaque"          
                "Queue"="Geometry"
            }
    	Pass
    	{  		
     	 Name "Universal Forward"
            Tags {"LightMode" = "UniversalForward"}

       	HLSLPROGRAM
        	#pragma prefer_hlslcc gles
        	#pragma exclude_renderers d3d11_9x

        	#pragma vertex vert
        	#pragma fragment frag       	       	

       	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

	half4 _TintColor;
	//float _Intensity;
	//sampler2D _MainTex;
	//float4 _MainTex_ST;
	//Texture2D _MainTex;
	//SamplerState sampler_MainTex;	
         	
         	struct VertexInput
         	  {
            	float4 vertex : POSITION;
            	float2 uv       : TEXCOORD0;
          	  };

        	struct VertexOutput
          	  {
           	           float4 vertex  	: SV_POSITION;
                       float2 uv      	: TEXCOORD0;           	
      	  };

      	VertexOutput vert(VertexInput v)
        	  {
          	VertexOutput o;
      
          	             	
          	//o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;	
            
             o.uv=v.uv;	
            // float xmod=tex2Dlod(_MainTex,float4(o.uv.xy,0,1));
             //그냥 tex2d는 버텍스 쉐이더에서 작동 안한다.
             //xmod=xmod*2-1;
            // o.uv=o.uv*2-1;

            //o.uv.x=sin(length(o.uv.xy)*10+_Time.y*5);
            o.uv.x=sin((o.uv.xy)*10+_Time.y*5);
             // o.uv.x=sin(xmod*10-_Time.y);
             float3 vert=v.vertex;

            vert.y=o.uv.x;
          // vert.z=o.uv.x*10;
          // o.uv.x=o.uv.x*0.5+0.5;
           o.vertex = TransformObjectToHClip(vert);    
           

         	return o;
        	  }

        	half4 frag(VertexOutput i) : SV_Target
        	  {             


	//float4 color = _MainTex.Sample(sampler_MainTex, i.uv) * _TintColor * _Intensity; 
    
          	return _TintColor*float4(0,0,i.uv.x,1);
            //return float4(1,1,1,1);
            //여기를 걍 return을 float4(1,1,1,1); 만 해도 작동한다. 
        	  }
	ENDHLSL  
    	  }
     }

}//x값의 위치에따라 아웃풋 y가 사인처럼. 그게 vertexpos의 y로. 
