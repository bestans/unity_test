Shader "Custom/ChangeColor"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Diffuse (RGB), Color Mask (A)", 2D) = "white" {}
		_MaskTex ("MaskTex", 2D) = "black" {}
	}

	
	// Simple quality settings -- drop the bump map
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _MaskTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 tex 	= tex2D(_MainTex, IN.uv_MainTex);	
			half4 mask = tex2D(_MaskTex, IN.uv_MainTex);	
			o.Albedo 	= lerp(tex.rgb, tex.rgb * _Color.rgb, mask.r);
			o.Alpha 	= _Color.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}