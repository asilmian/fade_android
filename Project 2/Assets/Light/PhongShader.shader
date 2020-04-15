//Lamp Shader, Assumes object has a texture for color
//Edited version of Phong illumination from COMP30019 Lab

//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/phongShader"
{
	Properties
	{
		_PointLightColor("Point Light Color", Color) = (0, 0, 0)
		_PointLightPosition("Point Light Position", Vector) = (0.0, 0.0, 0.0)
		_MainTex("Pattern", 2D) = "white" {}
	}
		SubShader
	{
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		uniform float3 _PointLightColor;
		uniform float3 _PointLightPosition;
		uniform sampler2D _MainTex;

		//range needed to decrease the effect of light
		uniform float _Range;

	struct vertIn
	{
		float4 vertex : POSITION;
		float4 normal : NORMAL;
		float4 uv:TEXCOORD0;
	};

	struct vertOut
	{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
		float3 worldVertex : TEXCOORD1;
		float3 normal : NORMAL;
	};


	//calculate co-ordinates and send them to fragment shader
	vertOut vert(vertIn v)
	{
		vertOut o;


		o.worldVertex = mul(unity_ObjectToWorld, v.vertex);

		//get normal of the vertex in worldspace
		o.normal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));


		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.uv;

		return o;
	}

	// Implementation of the fragment shader
	fixed4 frag(vertOut v) : SV_TARGET
	{
		// Our interpolated normal might not be of length 1
		float3 interpNormal = normalize(v.normal);

		// Calculate ambient RGB intensities
		float Ka = 1;
		float3 amb =  UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

		// find diffuse parts of the illumination
		float fAtt = 1;
		float Kd = 1;
		float3 L = normalize(_PointLightPosition - v.worldVertex.xyz);
		float LdotN = dot(L, interpNormal);

		//finds attenuation depending on the distance of the light source from the vertex
		//component depends on distance from light source
		float attenuation = (1 / length(_PointLightPosition - v.worldVertex.xyz));
		float3 dif = fAtt * _PointLightColor.rgb * Kd * saturate(LdotN);

		//add amb and dif compents to the color of the texture at the current uv. multiply with range to decrease vision over time
		float3 returnColor = (amb + dif) * tex2D(_MainTex, v.uv) * attenuation * _Range;

		return fixed4(returnColor, 1);
	}
		ENDCG
	}
	}
}