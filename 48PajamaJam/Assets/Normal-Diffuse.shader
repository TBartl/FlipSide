Shader "Howdy/DiffuseSpecial" {
Properties {
	_Color("Main Color", Color) = (1,1,1,1)
	_OverColor("Override Color", Color) = (1,1,1,1)

	_MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200

CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
fixed4 _Color;
fixed4 _OverColor;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * (_Color * (1.0-_OverColor.a) + _OverColor * _OverColor.a );
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}
ENDCG
}

Fallback "Legacy Shaders/VertexLit"
}
