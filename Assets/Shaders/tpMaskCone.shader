Shader"ShadowLight/tpMaskCone"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="transparent" }
        LOD 100
        
        ZWrite Off
        Blend Zero One
        
        Stencil
        {
            Ref 1
            Comp Always
            Pass Replace
        }

        Pass
        {
        }
    }
}
