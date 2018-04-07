// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "FX/Moving Line Fast" {
	Properties {
		_MainTex ("Base", 2D) = "white" {}
		_TintColor ("TintColor", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	
	CGINCLUDE

		#include "UnityCG.cginc"

		sampler2D _MainTex;
		fixed4 _TintColor;
		
		half4 _MainTex_ST;
						
		struct v2f {
			half4 pos : SV_POSITION;
			half2 uv : TEXCOORD0;
		};

		v2f vert(appdata_full v) {
			v2f o;
			
			o.pos = UnityObjectToClipPos (v.vertex);
			//float x2 = v.texcoord.x - 0.5f;
			//float y2 = v.texcoord.y - 0.5f;
			//float ang = atan2(y2, x2);
			//float dist = sqrt(x2*x2+y2*y2);
			//ang += _Time.x * 2;
			//float2 tc = float2(sin(ang) * dist + 0.5f, cos(ang) * dist + 0.5f);
			//o.uv.xy = TRANSFORM_TEX(tc, _MainTex);
			o.uv.xy = TRANSFORM_TEX(float2(v.texcoord.x, v.texcoord.y), _MainTex);
			//o.uv.xy = TRANSFORM_TEX(v.texcoord, MainTex);
					
			return o; 
		}
		
		fixed4 frag( v2f i ) : COLOR {	
			//return tex2D (_MainTex, i.uv.xy) * _TintColor;
			float val = 0;
			float x2 = i.uv.x - .9f;
			float y2 = 0.0f;
			float dist = sqrt(x2*x2+y2*y2); // ?
			float lerpSpeed = 20.0f;
			float totalLength = 20.0f; // how much spacing between donts
			float lengthOfEmpty = 15.5f; // closer to 0 for solid line
			val = (int)(dist * 1800 + _Time.z * lerpSpeed) / 2 % totalLength > lengthOfEmpty;
			float4 spiral = float4(val, val, val, .5f);
			return spiral * _TintColor;
//			float nearness = 1.0f;
//	o.Emission = glow * glow.a * (sin(_Time.z * 3.0f + length(IN.worldPos)) * 0.25f + 0.75f) * nearness;
		}
	
	ENDCG
	
	SubShader {
		Tags { "RenderType" = "Transparent" "Reflection" = "RenderReflectionTransparentAdd" "Queue" = "Transparent"}
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
//		Blend One One
//		Blend SrcAlpha OneMinusSrcAlpha
		Blend SrcAlpha One
	Pass {
	
		CGPROGRAM
		
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest 
		
		ENDCG
		 
		}
				
	} 
	FallBack Off
}
