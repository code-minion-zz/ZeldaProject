Shader "Custom/SpriteShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull Off
		CGPROGRAM
		#pragma surface surf Lambert alpha
		//Cull Off
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;			
		}
		ENDCG
		
//		Pass
//		{
//			SetTexture[_MainTex]
//		}
	} 
	FallBack "Diffuse"
}
