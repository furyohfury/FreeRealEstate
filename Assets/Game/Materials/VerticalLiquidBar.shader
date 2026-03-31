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
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _FillAmount;
                float _BoilIntensity;
                float _BoilSpeed;
                float _BoilScale;
                float _EdgeSmoothness;
            CBUFFER_END

            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            Varyings vert (Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            float4 frag (Varyings input) : SV_Target
            {
                float2 uv = input.uv;
                float time = _Time.y * _BoilSpeed;

                // Слой 1: Движется вправо и вверх
                float2 uv1 = uv * _BoilScale + float2(time * 0.2, time * 0.5);
                float noise1 = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, uv1).r;

                // Слой 2: Движется влево и чуть быстрее вверх (для хаоса)
                float2 uv2 = uv * (_BoilScale * 1.5) + float2(-time * 0.3, time * 0.8);
                float noise2 = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, uv2).g;

                // Смешиваем шумы для получения нелинейных всплесков
                float combinedNoise = (noise1 + noise2) * 0.5;

                // Вычисляем текущий порог. 
                // Мы используем шум только для изменения высоты _FillAmount
                float boilingEffect = (combinedNoise - 0.5) * _BoilIntensity;
                float finalLevel = _FillAmount + boilingEffect;

                // Создаем маску заполнения
                float mask = smoothstep(finalLevel, finalLevel - _EdgeSmoothness, uv.y);

                // Опционально: добавляем небольшое "свечение" на пиках кипения
                float4 color = _BaseColor;
                color.rgb += smoothstep(0.4, 0.7, combinedNoise) * 0.2; // Блик на пузырях

                return float4(color.rgb, mask * color.a);
            }
            ENDHLSL
        }
    }
}