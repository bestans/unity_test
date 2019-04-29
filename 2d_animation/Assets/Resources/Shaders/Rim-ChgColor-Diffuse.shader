Shader "Custom/Rim-ChgColor-Diffuse" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_ControlTex("Control Tex",2d) = ""{}
		_Color2("Color",Color) = (1,0,0,0)
		_Intensity("Intensity",float) = 0
		_RimColor ("Rim Color", Color) = (0,0,0,0)
	    _RimPower ("Rim Power", float) = 2.0
		//SPECULAR
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", float) = 0.078125
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Cull off
		
		CGPROGRAM
		//#pragma surface surf Lambert
		#include "TGP_Include.cginc"

		//nolightmap nodirlightmap		LIGHTMAP
		//approxview halfasview			SPECULAR/VIEW DIR
		//noforwardadd					ONLY 1 DIR LIGHT (OTHER LIGHTS AS VERTEX-LIT)
		#pragma surface surf ToonyColorsSpec nolightmap nodirlightmap noforwardadd approxview halfasview 

		sampler2D _MainTex;
		sampler2D _ControlTex;
		//fixed4 _Color;
		fixed _Shininess;
		float _Intensity;
		fixed4 _Color2;
		float4 _RimColor;
		float _RimPower;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float3 worldNormal; 
		};
		

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			half4 controlTex = tex2D(_ControlTex,IN.uv_MainTex);
			
			fixed4 c = tex * _Color;

			o.Albedo = lerp(c.rgb, tex.rgb * _Color2.rgb * _Intensity, controlTex.r);

			//Specular
			o.Gloss = c.a;
			o.Specular = _Shininess;
			
			half rim = 1 - saturate(dot (normalize(IN.viewDir), IN.worldNormal));
		    o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		    
			o.Alpha = c.a;

		}
		ENDCG
	
	} 
	
	FallBack "Diffuse"
}
