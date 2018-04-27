﻿Shader "Unlit/UnlitSkid"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	    _Color("Skid Color", Color) = (1, 1, 1, 1)
		[PerRendererData]_Alpha("Alpha", Range(0.0,1)) = 0.5
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent"  "RenderType" = "Transparent" }
		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			float _Alpha;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
			    col = col * _Color;
				col.a = _Alpha;
				return col;
			}
			ENDCG
		}
	}
}
