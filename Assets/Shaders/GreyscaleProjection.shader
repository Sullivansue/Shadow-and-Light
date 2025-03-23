Shader "ShadowLight/FlatProjection"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off // �ر��޳�ȷ�������涼����Ⱦ

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
                // ������ת�����ü��ռ�
                float4 clipPos = UnityObjectToClipPos(v.vertex);
                
                // ѹƽZ�ᵽ���ü��棨���ݲ�ͬͶӰ���������
                #if UNITY_UV_STARTS_AT_TOP
                clipPos.z = clipPos.w; // ��������ͶӰ�Ĳü��ռ�
                #else
                clipPos.z = 0; // �����ʵ���������
                #endif
                
                o.vertex = clipPos;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(0,0,0,1); // ����ɫ���
            }
            ENDCG
        }
    }
}