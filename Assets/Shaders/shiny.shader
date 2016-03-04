Shader "Custom/shiny shader" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
//		LOD 200
		
		CGPROGRAM

		#pragma fragment frag
		#pragma vertex vert

		struct VertexInput {
			float4 position : POSITION;
//			float4 normal : NORMAL;
		};

		struct VertexOutput {
			float4 position : SV_POSITION;
//			float4 normal : NORMAL;
		};

		VertexOutput vert(VertexInput input) {
			VertexOutput output;
			output.position = mul(UNITY_MATRIX_MVP, input.position);
			return output;
		}

		float4 frag(VertexOutput output) : COLOR {
			return float4(1, 0, 0, 1);
		}

		ENDCG
	}
	FallBack "Diffuse"
}
