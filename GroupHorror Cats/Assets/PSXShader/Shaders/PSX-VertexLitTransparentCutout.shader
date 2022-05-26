Shader "PSX/Vertex Lit Cutout"
{
	Properties
	{
		_Color("Color (RGBA)", Color) = (1, 1, 1, 1)
        _EmissionColor("Emission Color (RGBA)", Color) = (0,0,0,0)
        _CubemapColor("Cubemap Color (RGBA)", Color) = (0,0,0,0)
        _MainTex("Texture", 2D) = "white" {}
        _EmissiveTex("Emissive", 2D) = "black" {}
        _Cubemap("Cubemap", Cube) = "" {}
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.1
	}
	SubShader
	{
		Tags {"RenderType" = "Opaque" }
		ZWrite On
		LOD 100

		Pass
		{
			Tags { "LightMode" = "VertexLM" }
			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile __ PSX_ENABLE_TRIANGLE_SORTING

			#include "UnityCG.cginc"
			#include "PSX-Utils.cginc"

			samplerCUBE _Cubemap;
			float4 _CubemapColor;
			float _Cutoff;
			
            #define PSX_VERTEX_LIT
			#define PSX_CUBEMAP _Cubemap
			#define PSX_CUBEMAP_COLOR _CubemapColor
			#define PSX_CUTOUT_VAL _Cutoff
			
			#include "PSX-ShaderSrc.cginc"

		ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "Vertex" }
			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile __ PSX_ENABLE_TRIANGLE_SORTING

			#include "UnityCG.cginc"
			#include "PSX-Utils.cginc"

			samplerCUBE _Cubemap;
			float4 _CubemapColor;
			float _Cutoff;
			
            #define PSX_VERTEX_LIT
			#define PSX_CUBEMAP _Cubemap
			#define PSX_CUBEMAP_COLOR _CubemapColor
			#define PSX_CUTOUT_VAL _Cutoff
			
			#include "PSX-ShaderSrc.cginc"

			ENDCG
		}
	}
		Fallback "PSX/Unlit Transparent"
}