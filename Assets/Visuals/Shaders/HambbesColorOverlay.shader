// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "Hidden/HambbesColorOverlay"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Vector) = (0,0,0,0)
		_ColorStrength("ColorStrength", Float) = 1
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
			uniform half4 _Color;
			uniform half _ColorStrength;

			fixed4 frag (v2f input) : SV_Target
			{
				half4 color = tex2D(_MainTex, input.uv);

				if (_ColorStrength == 0)
				{
					return color;
				}

				return lerp(color, _Color, clamp(_ColorStrength, 0, 1));
			}
			ENDCG
		}
	}
}
