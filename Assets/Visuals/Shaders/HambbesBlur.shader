// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "Hidden/HambbesBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BlurStrength("", Float) = 7
		_BlurWidth("", Float) = 0.3
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
			
			sampler2D _MainTex;
			uniform half _BlurStrength;
			uniform half _BlurWidth;

			fixed4 frag (v2f i) : SV_Target
			{
				half4 color = tex2D(_MainTex, i.uv);

				if (_BlurWidth == 0 || _BlurStrength == 0)
				{
					return color;
				}
				
				// some sample positions
				static half samples[16] =
				{
					-0.13, -0.11, -0.09, -0.07, -0.05, -0.03, -0.02, -0.01, 0.01, 0.02, 0.03, 0.05, 0.07, 0.09, 0.11, 0.13
				};
				
				half2 dir = i.uv - 0.5 * half2(1, 1);
				
				//distance to center
				half dist = sqrt(dir.x*dir.x + dir.y*dir.y);
				
				//normalize direction
				dir = dir / dist;
				
				half4 sum = color;
				for (int n = 0; n < 16; n++)
				{
					sum += tex2D(_MainTex, i.uv + dir * samples[n] * _BlurWidth);
				}
				sum *= 1.0 / 17.0;
				
				half t = clamp(dist * _BlurStrength, 0.0, 1.0);
				
				//blend original with blur
				return lerp(color, sum, t);

			}
			ENDCG
		}
	}
}
