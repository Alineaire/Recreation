// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Timer"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 0)
		_Angle("Angle", Float) = 0
		_AspectRatio("Aspect ratio", Float) = 1
		_InactiveRatio("Inactive ratio", Float) = 0.5
	}
	SubShader
	{
		Cull Back
		ZWrite On

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
			
			uniform float4 _Color;
			uniform float _Angle;
			uniform float _AspectRatio;
			uniform float _InactiveRatio;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 offset = i.uv - float2(0.5, 0.5);
				float angle = -atan2(offset.x * _AspectRatio, -offset.y);
				return _Color * lerp(_InactiveRatio, 1, step(_Angle, angle));
			}
			ENDCG
		}
	}
}
