Shader "Custom/glassBroken"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _UvOffsetTex("uv offset tex",2D) = "white" {}
        _Edge("edge",2D) = "white" {}
        _EdgeColor("edge color",COLOR) = (1,1,1,1)
        _OffsetIntensity("offset intensity",Range(0,1)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            
            sampler2D _UvOffsetTex,_MainTex,_Edge;
            SAMPLER(sampler_MainTex);

            float4 _EdgeColor;
            float _OffsetIntensity;

            struct Attributes
            {
                float4 position : POSITION;
                float2 texcoord : TEXCOORD0;
                uint vertexID : VERTEXID_SEMANTIC;
            };

            struct Varyings
            {
                float4 position : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            Varyings Vert (Attributes v)
            {
                Varyings o;
                o.position = TransformObjectToHClip(v.position);
                o.texcoord = v.texcoord;
                return o;
            }

            

            float4 Frag (Varyings i) : SV_TARGET
            {
                float EdgeMask = tex2D(_Edge,i.texcoord).r;
                float4 finalEdgeColor = lerp(_EdgeColor,float4(1,1,1,1),1 - EdgeMask);
                float uvOffset = tex2D(_UvOffsetTex,i.texcoord).r;
                float4 color = tex2D(_MainTex,i.texcoord + uvOffset * _OffsetIntensity);
                color = lerp(color,finalEdgeColor,1 - EdgeMask);
                return color;
            }
            ENDHLSL

            // 设置通过 URP 编译
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            ENDHLSL
        }
    }
}