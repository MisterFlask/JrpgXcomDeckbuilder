Shader "Skybox/Cosmos Static"{
	Properties {
		[Hdr]_Color ("Color", Color) = (3, 5, 6, 3)
		[Header(Main Texture)]
		[NoScaleOffset]_MainTex ("", Cube) = "white" {}
		_Colorize ("Colorize", Range(0,2)) = 0.6

		[Space(20)]
		[Header(Detail 1)]
		[NoScaleOffset]_Detail1 ("", 2D) = "white" {}
		_D1I ("Intensity", Range(0,2)) = 1
		_D1Scale ("Scale", FLOAT) = 2
		[Space(20)]
		[Header(Detail 2)]
		[NoScaleOffset]_Detail2 ("", 2D) = "white" {}
		_D2I ("Intensity", Range(0,2)) = 0.8
		_D2Scale ("Scale", FLOAT) = 5
	}

	SubShader {
		Tags { "Queue"="Background" "RenderType"="Background" }
		Cull Off ZWrite Off Fog { Mode Off }

		Pass {
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct appdata_t {
				float4 vertex : POSITION;
				float3 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				float3 texcoord : TEXCOORD0;
				float3 texcoord1 : TEXCOORD1;
				float3 texcoord2 : TEXCOORD2;
			};
			
			half _D1I;
			half _D1Scale;
			half _D2I;
			half _D2Scale;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.texcoord1 = v.texcoord*_D1Scale;
				o.texcoord2 = v.texcoord*_D2Scale;
				return o;
			}
			
			fixed4 _Color;
			samplerCUBE _MainTex;
			half _Colorize;
			sampler2D _Detail2;
			sampler2D _Detail1;

			fixed4 triplanar(sampler2D tex,float3 coord){
				fixed4 c = 
				     tex2D(tex, coord.xy) * abs(coord.z);
				c += tex2D(tex, coord.yz) * abs(coord.x);
				c += tex2D(tex, coord.zx) * abs(coord.y);
				return c/(abs(coord.x) + abs(coord.y) + abs(coord.z));
			}

			fixed4 saturate(fixed4 col,half sat){
				return lerp(
					(col.r + col.g + col.b)/3,
					col, sat
				);
			}

			fixed4 frag (v2f i) : COLOR
			{
				fixed4 cosmos = texCUBE(_MainTex, i.texcoord); 

				fixed detail1 = triplanar(_Detail1,i.texcoord1);
				fixed detail2 = triplanar(_Detail2,i.texcoord2);

				return fixed4(
					saturate(cosmos,_Colorize) * 
					_Color.rgb * 
					lerp(1,detail1,_D1I)*
					lerp(1,detail2,_D2I),1);
			}
			ENDCG 
		}
	} 	
	Fallback Off
}

