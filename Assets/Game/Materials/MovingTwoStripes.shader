Shader "Custom/MovingTwoStrips"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.1, 0.1, 0.1, 1) // Цвет фона
        _StripColor ("Strip Color", Color) = (0, 1, 0.5, 1)    // Цвет полосок
        _StripWidth ("Strip Width", Range(0, 0.5)) = 0.1      // Ширина каждой полоски
        _Speed ("Movement Speed", Float) = 1.0               // Скорость движения
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 100

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
                float4 _StripColor;
                float _StripWidth;
                float _Speed;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Рассчитываем движение по времени. 
                // Используем frac, чтобы значение всегда было от 0 до 1.
                float timeOffset = _Time.y * _Speed;
                
                // Полоска 1 (основная)
                float pos1 = frac(IN.uv.y + timeOffset);
                // Полоска 2 (смещенная на полцикла)
                float pos2 = frac(IN.uv.y + timeOffset + 0.5);

                // Проверяем, попадает ли текущая UV координата в диапазон ширины полосок
                bool isStrip1 = pos1 < _StripWidth;
                bool isStrip2 = pos2 < _StripWidth;

                // Если мы в зоне любой из полосок, рисуем цвет полоски, иначе — фон
                float4 finalColor = (isStrip1 || isStrip2) ? _StripColor : _BaseColor;

                return finalColor;
            }
            ENDHLSL
        }
    }
}