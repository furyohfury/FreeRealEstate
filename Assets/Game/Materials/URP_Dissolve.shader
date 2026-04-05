Shader "Custom/URP_Dissolve"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.5, 0.5, 0.5, 1)
        [MainTexture] _BaseTex ("Base Texture", 2D) = "white" {}
        _NoiseScale("Noise Scale", Float) = 20
        _NoiseStrength("Noise Strength", Range(0,1)) = 1.0
        // Теперь это основной ползунок растворения от 0 до 1
        _DissolveAmount("Dissolve Amount", Range(-1, 1)) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "AlphaTest"
            "RenderPipeline" = "UniversalPipeline"
        }
        Pass
        {
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _BaseTex;
            CBUFFER_START(UnityPerMaterial)
                float4 _BaseTex_ST;
                float4 _BaseColor;
                float _DissolveAmount;
                float _NoiseScale;
                float _NoiseStrength;
            CBUFFER_END

            v2f vert(appdata v)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(v.positionOS);
                o.uv = TRANSFORM_TEX(v.uv, _BaseTex);
                return o;
            }

            float2 generateDir(float2 p)
            {
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }

            float generateNoise(float2 p)
            {
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(generateDir(ip), fp);
                float d01 = dot(generateDir(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(generateDir(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(generateDir(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
            }

            float gradientNoise(float2 UV, float Scale)
            {
                return generateNoise(UV * Scale) * 2.0f;
            }

            float4 frag(v2f i) : SV_Target
            {                
                // 1. Генерируем шум на основе UV координат
                float noiseSample = gradientNoise(i.uv, _NoiseScale);
                
                // 2. Умножаем на силу шума (влияет на контрастность дыр)
                float finalNoise = noiseSample * _NoiseStrength;

                // 3. Отсекаем пиксели. 
                // Теперь мы просто сравниваем шум с порогом _DissolveAmount.
                // Если шум меньше порога — пиксель исчезает.
                clip(finalNoise - _DissolveAmount);

                float4 textureSample = tex2D(_BaseTex, i.uv);
                return textureSample * _BaseColor;
            }
            ENDHLSL
        }
    }
}