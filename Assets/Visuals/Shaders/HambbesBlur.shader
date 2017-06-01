// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "Hidden/HambbesBlur"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_BlurStrength("", Float) = 7
		_BlurWidth("", Float) = 0.3
		//_ShakeStrength("", Float) = 1
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			// float rand()
			// {
			// 	return frac(sin(dot(_Time.xy, float2(12.9898, 78.233))) * 43758.5453);
			// }
			
			sampler2D _MainTex;
			uniform half _BlurStrength;
			uniform half _BlurWidth;
			//uniform half _ShakeStrength;

			static half samples[16] =
			{
				-0.13, -0.11, -0.09, -0.07, -0.05, -0.03, -0.02, -0.01, 0.01, 0.02, 0.03, 0.05, 0.07, 0.09, 0.11, 0.13
			};

			fixed4 frag (v2f input) : SV_Target
			{
				half2 uv = input.uv;

				// if (_ShakeStrength > 0)
				// {
				// 	float2 r = rand() * 0.005 * _BlurStrength;
				// 	uv += r;
				// }

				half4 color = tex2D(_MainTex, uv);

				if (_BlurWidth == 0 || _BlurStrength == 0)
				{
					return color;
				}

				half2 dir = input.uv - 0.5 * half2(1, 1);
				half dist = sqrt(dir.x*dir.x + dir.y*dir.y);
				dir /= dist;

				half4 sum = color;
				for (int n = 0; n < 16; n++)
				{
					sum += tex2D(_MainTex, uv + dir * samples[n] * _BlurWidth);
				}
				sum *= 1.0 / 17.0;

				half t = clamp(dist * _BlurStrength, 0.0, 1.0);

				return lerp(color, sum, t);
			}
			ENDCG
		}
	}
}
