// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "2DAvoid/Player"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_OutlineColor("OutlineColor", Color) = (1,0,0,0)
		_InnerColor("InnerColor", Color) = (0.7075472,0.7075472,0.7075472,0)
		_CircleRadius("CircleRadius", Float) = 0.9
		_Thickness("Thickness", Float) = 0.1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _InnerColor;
		uniform float _CircleRadius;
		uniform float4 _OutlineColor;
		uniform float _Thickness;
		uniform float _Cutoff = 0.5;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float temp_output_3_0 = step( distance( i.uv_texcoord , float2( 0.5,0.5 ) ) , ( _CircleRadius * 0.5 ) );
			float2 temp_output_10_0_g2 = i.uv_texcoord;
			float2 temp_output_13_0_g2 = float2( 0.5,0.5 );
			float temp_output_6_0_g2 = _CircleRadius;
			float temp_output_4_0 = ( step( distance( temp_output_10_0_g2 , temp_output_13_0_g2 ) , ( ( _Thickness + temp_output_6_0_g2 ) * 0.5 ) ) - step( distance( temp_output_10_0_g2 , temp_output_13_0_g2 ) , ( temp_output_6_0_g2 * 0.5 ) ) );
			float4 lerpResult10 = lerp( ( _InnerColor * temp_output_3_0 ) , ( _OutlineColor * temp_output_4_0 ) , temp_output_4_0);
			o.Emission = lerpResult10.rgb;
			o.Alpha = 1;
			clip( ( temp_output_3_0 + temp_output_4_0 ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
2567;23;1906;1010;872;305;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1030,283;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-591,517;Float;False;Property;_Thickness;Thickness;4;0;Create;True;0;0;False;0;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1039,482;Float;False;Property;_CircleRadius;CircleRadius;3;0;Create;True;0;0;False;0;0.9;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-772,-313;Float;False;Property;_OutlineColor;OutlineColor;1;0;Create;True;0;0;False;0;1,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-907,-71;Float;False;Property;_InnerColor;InnerColor;2;0;Create;True;0;0;False;0;0.7075472,0.7075472,0.7075472,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;4;-308,369;Float;True;Shape-CircleOutlined;-1;;2;70537716381e1ae45b39693927da8a86;0;4;13;FLOAT2;0.5,0.5;False;10;FLOAT2;0,0;False;6;FLOAT;0.5;False;7;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;3;-583,160;Float;True;Shape-Circle;-1;;1;6c4ee9ad23603c34cad3f76edd5b3a61;0;3;13;FLOAT2;0.5,0.5;False;10;FLOAT2;0,0;False;6;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-235,57;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-84,-140;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;10;124,-33;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;78,394;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;539,-134;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;2DAvoid/Player;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Custom;0.5;True;True;0;False;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;10;5;0
WireConnection;4;6;6;0
WireConnection;4;7;7;0
WireConnection;3;10;5;0
WireConnection;3;6;6;0
WireConnection;9;0;2;0
WireConnection;9;1;3;0
WireConnection;8;0;1;0
WireConnection;8;1;4;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;10;2;4;0
WireConnection;11;0;3;0
WireConnection;11;1;4;0
WireConnection;0;2;10;0
WireConnection;0;10;11;0
ASEEND*/
//CHKSM=9F29C8D6D69FF0A43B5ADB5856A4618DE74AA0E8