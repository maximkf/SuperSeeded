// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SpecialFX/NoiseDisplace"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color("Main Color", Color) = (1,1,1,1)
        _Transparency("Transparency", Range(0.0,1.0)) = 0.25
      //_CutoutThresh("Cutout Threshold", Range(0.0,1.0)) = 0.2
        _Distance("Distance", Float) = 1
        _Amplitude("Amplitude", Float) = 1
        _Speed ("Speed", Float) = 1
        _Amount("Amount", Range(0.0,1.0)) = 1
    }

    SubShader
    {
        Tags {"Queue"="Geometry" "RenderType"="Opaque" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
          Tags {"LightMode" = "ForwardBase"}
          CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma fragmentoption ARB_fog_exp2
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            half4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                half4 color : COLOR;
                LIGHTING_COORDS(1,2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Transparency;
            float _CutoutThresh;
            float _Distance;
            float _Amplitude;
            float _Speed;
            float _Amount;

						float hash( float n )
						{
								return frac(sin(n)*43758.5453);
						}

						float noise( float3 x )
						{
								// The noise function returns a value in the range -1.0f -> 1.0f

								float3 p = floor(x);
								float3 f = frac(x);

								f       = f*f*(3.0-2.0*f);
								float n = p.x + p.y*57.0 + 113.0*p.z;

								return lerp(lerp(lerp( hash(n+0.0), hash(n+1.0),f.x),
															 lerp( hash(n+57.0), hash(n+58.0),f.x),f.y),
													 lerp(lerp( hash(n+113.0), hash(n+114.0),f.x),
															 lerp( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
						}


						v2f vert (appdata_tan v)
						{
              v2f o;
              v.vertex.x += noise(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
              o.vertex = UnityObjectToClipPos(v.vertex);
              o.uv = TRANSFORM_TEX (v.texcoord, _MainTex).xy;
              return o;
						}

            fixed4 frag (v2f i) : SV_TARGET
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
                col.a = _Transparency;
                fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
      					return col * atten;
            }
            ENDCG
        }
    }

}
