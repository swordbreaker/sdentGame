// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "Hidden/HambbesShake"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ShakeStrength("", Float) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float rand()
			{
				return frac(sin(dot(_Time.xy, float2(12.9898, 78.233))) * 43758.5453);
			}

			uniform half _ShakeStrength;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float2 uv = v.uv;
				if (_ShakeStrength > 0)
				{
					float2 r = rand() * 0.05 * _ShakeStrength;
					uv += r;
				}
				o.uv = uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f input) : SV_Target
			{
				return tex2D(_MainTex, input.uv);
			}
			ENDCG
		}
	}
}
