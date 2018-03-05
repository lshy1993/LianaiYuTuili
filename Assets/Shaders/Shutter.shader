// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Shutter" {
    Properties {
        _MainTex ("Old Texture", 2D) = "white" {}
        //_NewTex ("New Texture", 2D) = "white" {}
        _TexSize ("Texture Size", vector) = (256, 256, 0, 0)
        _FenceWidth ("Fence Width", Range(10, 50)) = 30.0
    }

    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off
            Fog { Mode off }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            sampler2D _NewTex;
            float4 _TexSize;
            float _FenceWidth;
			float currentT;
            
            struct v2f {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            v2f vert(appdata_img v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = v.texcoord;
                return o;
            }
            
            float4 frag (v2f i) : COLOR
            {
                //当前进度(0-1)
				float progress = currentT;
				//底部右边界
                float fence_rate = fmod(i.uv.x * _TexSize.x, _FenceWidth) / _FenceWidth;
                if (progress < fence_rate)
                    return tex2D(_MainTex, i.uv);
                else
                    return tex2D(_NewTex, i.uv);
            }
            ENDCG
        }
    }

    Fallback off
}