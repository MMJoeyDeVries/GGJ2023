Shader "Unlit/S_Backdrop"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
            half4 _Tint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 col = half4(0,0,0,1);
                float3 noise = tex2D(_MainTex, i.uv * float2(1.0, 1.0) + float2(_Time.y * -0.06, 0.0)).rgb;
                noise *= tex2D(_MainTex, i.uv * noise * 4.0).rgb;
                noise *= tex2D(_MainTex, i.uv * noise + noise * 1.0).rgb;
                
                

                col.rgb = noise.r * _Tint.rgb + noise.g * _Tint.rgb;
                return col;
            }
            ENDCG
        }
    }
}
