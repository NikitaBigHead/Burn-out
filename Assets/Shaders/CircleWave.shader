Shader "Custom/CircleWave"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Thickness("Thickness", Float) = 0.65
        _Size("Size", Float) = 1
    }
    SubShader
    {
        LOD 200
        Tags { "Queue" = "Transparent" }
        Pass
        {

            Blend SrcAlpha OneMinusSrcAlpha // Traditional transparency
            CGPROGRAM

            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;

            struct Input
            {
                float2 uv_MainTex;
            };

            struct VertIn {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertOut {
                float4 pos : SV_Position;
                float2 uv : TEXCOORD0;
                float4 worldPos : DADYA;
            };

            VertOut vert(VertIn input) {
                VertOut o;
                o.worldPos = input.pos;
                o.pos = UnityObjectToClipPos(input.pos);
                o.uv = input.uv;
                return o;
            }

            struct FragOut {
                fixed4 color : SV_Target;
            };

            fixed4 _Color;
            float _Thickness;
            float _Size;

            FragOut frag(VertOut input) {
                FragOut o;
                float a = length(input.worldPos.xy);
                if (a > 0.5) {
                    o.color = fixed4(0, 0, 0, 0);
                }
                else if (a > 0.5 - _Thickness / _Size) {
                    o.color = tex2D(_MainTex, input.uv) * _Color;// _Color;
                }
                else {
                    o.color = fixed4(0, 0, 0, 0);
                }
                //o.color = fixed4(a, a, a, 0.0f);
                return o;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}