Shader "Unlit/S_Sprite"
{
    Properties
    {
        _Tex1("Tex 1", 2D) = "white" {}
        _Tex2("Tex 2", 2D) = "white" {}
        _Fade("Fade", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        LOD 100
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _Tex1;
            float4 _Tex1_ST;
            sampler2D _Tex2;
            float _Fade;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Tex1);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col1 = tex2D(_Tex1, i.uv);
                fixed4 col2 = tex2D(_Tex2, i.uv);

                float fade = step(i.uv.y, (_Fade - 0.03) + sin(i.uv.x * 12.0 + i.uv.y * 31.16) * 0.05);
                fixed4 col = lerp(col1, col2, fade);
                return col;
            }
            ENDCG
        }
    }
}
