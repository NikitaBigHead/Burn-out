Shader "Custom/HealthBarInstanced"
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
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha

            CGPROGRAM

            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            fixed4 _Color;

            struct VertIn {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(float, _Health)
            UNITY_INSTANCING_BUFFER_END(Props)

            struct VertOut {
                float4 pos : SV_Position;
                float2 uv : TEXCOORD0;
            };

            struct FragOut {
                fixed4 color : SV_Target;
            };

            VertOut vert(VertIn input) {
                VertOut o;
                UNITY_SETUP_INSTANCE_ID(input);
                float fill = UNITY_ACCESS_INSTANCED_PROP(Props, _Health);

                o.pos = UnityObjectToClipPos(input.pos);
                o.uv = input.uv;
                o.uv.x += 0.5 - fill;
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
