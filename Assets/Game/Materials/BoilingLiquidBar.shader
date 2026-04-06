Shader "Custom/BoilingLiquidBar"
{
    Properties
    {
        [Header(Appearance)]
        _BaseColor ("Liquid Color", Color) = (1, 0, 0, 1)
        _FillAmount ("Fill Amount", Range(0, 1)) = 0.5
        
        [Header(Boiling Settings)]
        _NoiseTex ("Noise Texture (Perlin/Simplex)", 2D) = "white" {}
        _BoilIntensity ("Boil Height", Range(0, 0.5)) = 0.1
        _BoilSpeed ("Boil Speed", Range(0, 10)) = 3
        _BoilScale ("Boil Scale", Range(0.1, 5)) = 1.0

        [Header(Rendering)]
        _EdgeSmoothness ("Edge Smoothness", Range(0, 0.1)) = 0.01
        
        [Header(Mask)]
        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilID ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        
        Stencil
        {
            Ref [_StencilID]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        
        ColorMask [_ColorMask]

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _FillAmount;
                float _BoilIntensity;
                float _BoilSpeed;
                float _BoilScale;
                float _EdgeSmoothness;
                float _StencilComp;
                float _StencilID;
                float _StencilOp;
                float _StencilReadMask;
                float _StencilWriteMask;
                float _ColorMask;
            CBUFFER_END

            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            Varyings vert (Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                output.color = input.color;
                return output;
            }

            float4 frag (Varyings input) : SV_Target
            {
                float2 uv = input.uv;
                float time = _Time.y * _BoilSpeed;

                float2 uv1 = uv * _BoilScale + float2(time * 0.2, time * 0.5);
                float noise1 = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, uv1).r;

                float2 uv2 = uv * (_BoilScale * 1.5) + float2(-time * 0.3, time * 0.8);
                float noise2 = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, uv2).g;

                float combinedNoise = (noise1 + noise2) * 0.5;

                float boilingEffect = (combinedNoise - 0.5) * _BoilIntensity;
                float finalLevel = _FillAmount + boilingEffect;

                float mask = smoothstep(finalLevel, finalLevel - _EdgeSmoothness, uv.y);

                float4 color = _BaseColor * input.color;
                color.rgb += smoothstep(0.4, 0.7, combinedNoise) * 0.2;

                float finalAlpha = mask * color.a;
                finalAlpha = clamp(finalAlpha, 0, 1);
                
                return float4(color.rgb, finalAlpha);
            }
            ENDHLSL
        }
    }
}