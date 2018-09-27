// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "2DAvoid/Obstacle"
{
	Properties
	{
		_Surfacecolor("Surfacecolor", Color) = (0.1981132,0.1981132,0.1981132,0)
		_LineColor("LineColor", Color) = (0.7830189,0.1317237,0,0)
		_LineNumber("LineNumber", Float) = 10
		_LineHeight("LineHeight", Float) = 0.5
		_MotionSpeed("MotionSpeed", Float) = 1
		_GlowFactor("GlowFactor", Float) = 1
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
			float3 worldPos;
		};

		uniform float4 _Surfacecolor;
		uniform float4 _LineColor;
		uniform float _MotionSpeed;
		uniform float _LineNumber;
		uniform float _LineHeight;
		uniform float _GlowFactor;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Surfacecolor.rgb;
			float3 ase_worldPos = i.worldPos;
			float mulTime14 = _Time.y * _MotionSpeed;
			float4 lerpResult12 = lerp( _LineColor , float4( 0,0,0,0 ) , step( frac( ( ( ase_worldPos.y + mulTime14 ) * _LineNumber ) ) , _LineHeight ));
			o.Emission = ( lerpResult12 * _GlowFactor ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
2567;29;1906;1004;1018.493;467.5133;1.3;True;True
Node;AmplifyShaderEditor.RangedFloatNode;15;-1906.394,487.9866;Float;False;Property;_MotionSpeed;MotionSpeed;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-1643.7,262.5001;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleTimeNode;14;-1656.793,459.3867;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1037.7,463.3001;Float;False;Property;_LineNumber;LineNumber;2;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-1416.294,361.8866;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-875.6998,405.3001;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;6;-682.6998,318.3001;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-661.6998,561.3001;Float;False;Property;_LineHeight;LineHeight;3;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-579,-67;Float;False;Property;_LineColor;LineColor;1;0;Create;True;0;0;False;0;0.7830189,0.1317237,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;8;-416.9999,350.6;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;12;-166.9939,65.48661;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;46.20657,313.7865;Float;False;Property;_GlowFactor;GlowFactor;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-563,-285;Float;False;Property;_Surfacecolor;Surfacecolor;0;0;Create;True;0;0;False;0;0.1981132,0.1981132,0.1981132,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;260.7066,138.2867;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;629,31;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;2DAvoid/Obstacle;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;15;0
WireConnection;13;0;3;2
WireConnection;13;1;14;0
WireConnection;4;0;13;0
WireConnection;4;1;5;0
WireConnection;6;0;4;0
WireConnection;8;0;6;0
WireConnection;8;1;9;0
WireConnection;12;0;2;0
WireConnection;12;2;8;0
WireConnection;16;0;12;0
WireConnection;16;1;17;0
WireConnection;0;0;1;0
WireConnection;0;2;16;0
ASEEND*/
//CHKSM=A843921A5268ECAF839BAB329FD8098B784D77E3