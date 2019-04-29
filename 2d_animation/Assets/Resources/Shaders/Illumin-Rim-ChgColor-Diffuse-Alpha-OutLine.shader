// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Illumin-Rim-ChgColor-Diffuse-Aplha-OutLine" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_Illum ("Illumin (A)", 2D) = "white" {}
		_EmissionLM ("Emission (Lightmapper)", Float) = 1
		_ControlTex("Control Tex",2d) = ""{}
		_Color2("Color",Color) = (1,0,0,0)
		_Intensity("Intensity",float) = 0
		_RimColor ("Rim Color", Color) = (0,0,0,0)
	    _RimPower ("Rim Power", float) = 2.0
		_Alpha("Alpha", float) = 1
		
		//OUTLINE
		_OutlineColor ("Outline Color", Color) = (0.0, 0.0, 0.0, 1)
		_Outline ("Outline Width", float) = 0.05
	}
	
	SubShader {
		//Tags { "RenderType"="Opaque" }
		Tags {"Queue" = "Transparent"  "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		
		Cull off
		//Blend SrcAlpha OneMinusSrcAlpha
		
		Pass {
		    ZWrite On
		    ColorMask 0

		    CGPROGRAM
		    #pragma vertex vert
		    #pragma fragment frag
		    #include "UnityCG.cginc"

		    struct v2f {
		        float4 pos : SV_POSITION;
		    };

		    v2f vert (appdata_base v)
		    {
		        v2f o;
		        o.pos = UnityObjectToClipPos (v.vertex);
		        return o;
		    }

		    half4 frag (v2f i) : COLOR
		    {
		        return half4 (0, 0, 0, 0);
		    }
		    ENDCG  
		}
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		fixed4 _Color;
		sampler2D _Illum;
		float _EmissionLM;
		sampler2D _ControlTex;
		float _Intensity;
		fixed4 _Color2;
		float4 _RimColor;
		float _RimPower;
		float _Alpha;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Illum;
			float3 viewDir;
			float3 worldNormal; 
		};
		

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			half4 controlTex = tex2D(_ControlTex,IN.uv_MainTex);
			
			fixed4 c = tex * _Color;

			o.Albedo = lerp(c.rgb, tex.rgb * _Color2.rgb * _Intensity, controlTex.r);
			
			half rim = 1 - saturate(dot (normalize(IN.viewDir), IN.worldNormal));
			o.Emission = o.Albedo.rgb * tex2D(_Illum, IN.uv_Illum).a * _EmissionLM + _RimColor.rgb * pow (rim, _RimPower);
			
			o.Alpha = c.a * _Alpha;

		}
		ENDCG
		
		UsePass "Hidden/ToonyColors-Outline/OUTLINE"
	
	} 
	
	FallBack "Self-Illumin/VertexLit"
}
