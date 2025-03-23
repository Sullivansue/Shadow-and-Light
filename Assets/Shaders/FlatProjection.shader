Shader "ShadowLight/FlatProjection"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off // 关闭剔除确保所有面都被渲染

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                // 将顶点转换到裁剪空间
                float4 clipPos = UnityObjectToClipPos(v.vertex);
                
                // 压平Z轴到近裁剪面（根据不同投影矩阵调整）
                #if UNITY_UV_STARTS_AT_TOP
                clipPos.z = clipPos.w; // 适配正交投影的裁剪空间
                #else
                clipPos.z = 0; // 或根据实际情况调整
                #endif
                
                o.vertex = clipPos;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(0,0,0,1); // 纯黑色输出
            }
            ENDCG
        }
    }
}