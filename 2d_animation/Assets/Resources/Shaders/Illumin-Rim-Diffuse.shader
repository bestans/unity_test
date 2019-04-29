Shader "Custom/Illumin-Rim-Diffuse" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_Illum ("Illumin (A)", 2D) = "white" {}
		_EmissionLM ("Emission (Lightmapper)", Float) = 1
		_RimColor ("Rim Color", Color) = (0,0,0,0)
	    _RimPower ("Rim Power", float) = 2.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Cull off
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _Color;
		sampler2D _Illum;
		float _EmissionLM;
		float4 _RimColor;
		float _RimPower;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Illum;
			float3 viewDir;
			float3 worldNormal; 
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 c = tex * _Color;
			o.Albedo = c.rgb;
			
			half rim = 1 - saturate(dot (normalize(IN.viewDir), IN.worldNormal));
			o.Emission = o.Albedo.rgb * tex2D(_Illum, IN.uv_Illum).a * _EmissionLM + _RimColor.rgb * pow (rim, _RimPower);
			o.Alpha = c.a;
		}
		ENDCG
	
	} 
	
	FallBack "Self-Illumin/VertexLit"
}
