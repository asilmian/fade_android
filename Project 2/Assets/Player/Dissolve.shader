Shader "Unlit/Dissolve"
{

	//Shader written by Asil Mian, gives the effect of object dissolving.
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DissolveTex("Dissolve Texture", 2D) = "white" {}
		_DissolveY("Current Y", Float) = 0
		_DissolveSize("Size of effect", Float) = 1
		_StartY("Starting point of effect", Float) = -3
		_Intensity("intesity of light", Float) = 0
		_VectorOne("Full color", Float) = 1
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

			#include "UnityCG.cginc"

			struct vertin
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct vertout
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DissolveTex;
			float _DissolveY;
			float _DissolveSize;
			float _StartY;
			float _Intensity;
			float _VectorOne;
			
			vertout vert (vertin v)
			{

				//find position of vertex in worldspace on the screen
				vertout o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				//get texture uv
				o.uv = v.uv;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (vertout i) : SV_Target
			{
				//difference of currentY and iY
				float difference = _DissolveY - i.worldPos.y;

				//color at noise
				fixed4 noise_value = _VectorOne - tex2D(_DissolveTex, i.uv);

				//remove or leave pixel based on y difference. noise_value adds pattern to remove
				clip(_StartY + (difference + (noise_value) * _DissolveSize));
				fixed4 col = tex2D(_MainTex, i.uv);
				return col * _Intensity;
			}
			ENDCG
		}
	}
}
