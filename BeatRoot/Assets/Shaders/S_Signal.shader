Shader "Unlit/S_Signal"
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
//        Blend One One

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Tint;

            v2f vert (appdata v)
            {
                v2f o;

                float3 vertex = v.vertex;

                float angle = atan2(vertex.x, vertex.z);
                float s = sin(_Time.y * 3.19 + angle * 2.19 + vertex.y * 38.1837);

                vertex = vertex * (1.0 + s * 0.38);
                
                o.vertex = UnityObjectToClipPos(vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                
                half4 col = half4(0, 0, 0, 1);

                float3 noise = tex2D(_MainTex, i.uv * 10.0).rgb;
                float3 noise2 = tex2D(_MainTex, i.uv + noise.rg * 1.71 + _Time.y).rgb;

                float3 V = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 N = normalize(i.normal);
                float VdotN = max(dot(V, N), 0.0);

                noise *= step(noise2, noise * 0.4);

                col.rgb = (noise.r + noise.g + noise.b).xxx * pow(1.0 - VdotN, 5.0) * 5.0;
                col.rgb += noise.g * pow(VdotN, 25.0) * 5.0;
                col.rgb += 0.05;
                col.rgb += (1.0 - VdotN) * 0.1;
                return col * _Tint;
            }
            ENDCG
        }
    }
}
