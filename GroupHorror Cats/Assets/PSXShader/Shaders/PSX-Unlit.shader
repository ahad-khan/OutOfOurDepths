Shader "PSX/Unlit"
{
    Properties
    {
        _Color("Color (RGBA)", Color) = (1, 1, 1, 1)
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags {"RenderType" = "Opaque" }
        ZWrite On
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile __ PSX_ENABLE_TRIANGLE_SORTING

            #include "UnityCG.cginc"
            #include "PSX-Utils.cginc"

            #include "PSX-ShaderSrc.cginc"

        ENDCG
        }
    }
        Fallback "Unlit/Color"
}