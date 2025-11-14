Shader "Custom/CartoonWater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Opacity ("Opacity", Range(0,1)) = 0.5
        _AnimationSpeedX ("Anim Speed X", Range(0,4)) = 1.3 
        _AnimationSpeedY ("Anim Speed Y", Range(0,4)) = 2.7
        _AnimationScale ("Anim Scale", Range(0,1)) = .03
        _AnimationTiling ("Anim Tiling", Range(0,20)) = 8
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"  "Queue" = "Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _Opacity;
            float _AnimationSpeedX;
            float _AnimationSpeedY;
            float _AnimationScale;
            float _AnimationTiling;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //distort the UVs to simulate wave
                i.uv.x+= sin((i.uv.x + i.uv.y) * _AnimationTiling + _Time.y * _AnimationSpeedX) * _AnimationScale;
                 i.uv.y+= cos((i.uv.x - i.uv.y) * _AnimationTiling + _Time.y * _AnimationSpeedY) * _AnimationScale;

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                //apply semi transparent
                col.a = _Opacity;
                return col;
            }
            ENDCG
        }
    }
}
