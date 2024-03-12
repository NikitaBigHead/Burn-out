Shader "Custom/HealthBar"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Health ("Health", Range(0,1)) = 1.0
    }
    SubShader
    {
        LOD 200
        Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }


        Pass
        {
            ZWrite Off
            Cull Off
            Blend One OneMinusSrcAlpha

            CGPROGRAM

            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;

            half _Health;
            fixed4 _Color;

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

            struct FragOut {
                fixed4 color : SV_Target;
            };

            VertOut vert(VertIn input) {
                VertOut o;
                o.worldPos = input.pos;
                o.pos = UnityObjectToClipPos(input.pos);
                o.uv = input.uv;
                return o;
            }

            FragOut frag(VertOut input) {
                FragOut o;
                if (((input.worldPos.x + 1) * 0.5) > _Health) {
                    o.color = tex2D(_MainTex, input.uv) * fixed4(0, 0, 0, 1.0);
                }
                else {
                    o.color = tex2D(_MainTex, input.uv) * _Color;// _Color;
                }
                return o;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
