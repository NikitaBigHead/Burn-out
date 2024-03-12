Shader "Custom/HealthBarHUD"
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
            ZTest Off
            ZWrite Off
            Cull Off
            Blend One OneMinusSrcAlpha

            CGPROGRAM

            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            fixed4 _Color;
            half _Health;

            struct VertIn {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertOut {
                float4 pos : SV_Position;
                float2 uv : TEXCOORD0;
            };

            struct FragOut {
                fixed4 color : SV_Target;
            };

            VertOut vert(VertIn input) {
                VertOut o;

                o.pos = UnityObjectToClipPos(input.pos);
                o.uv = input.uv;
                o.uv.x += 0.5 - _Health;
                return o;
            }

            FragOut frag(VertOut input) {
                FragOut o;
                o.color = tex2D(_MainTex, input.uv) * _Color;
                return o;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
