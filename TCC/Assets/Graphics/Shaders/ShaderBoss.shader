Shader "Custom/ShaderBoss"
{
    Properties {
	//DECLARANDO MAPAS UTILIZADO NO SHADER.
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_FlowMap ("Flow (RG, A noise)", 2D) = "black" {}
		_Cube ("Cubemap", CUBE) = "" {}
	//VARIAVEIS USADAS NO SHADER.
		_OffSetX ("Offset X", Range(-0.25, 0.25)) = 0.25
		_OffSety ("Offset Y", Range(-0.25, 0.25)) = 0.25
		_Tiling ("Escala dos mapas", Float) = 1
		_Velocidade ("Velocidade das animações", Float) = 1
		_FlowStrength ("Força mudança", Float) = 1
		_FlowOffset ("Offset", Float) = 0

	// VARIAVEIS DO STANDARD.
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}


	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		
		//DECLARAÇÃO DAS VARIAVEIS.
		sampler2D _MainTex, _FlowMap;
		sampler2D _Cube;
		float _OffSetX, _OffSety, _Tiling,_Velocidade, _FlowStrength, _FlowOffset;

		struct Input
		{
			float2 uv_MainTex;
			float3 worldRefl;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		//Este método foi criado para tratar as animações no shader. Nele temos parametros para controlar melhor o shader como por exemplo:
		//o tratamento das fases de cada parte do shader, para que a imagem do nosso shader não apareça quando estiver
		//no inicio da sua deformação, isso nos garante uma "fluides" no shader, nós podemos ainda controlar a força dessa "troca" entre as duas fases do shader.
		float3 FlowUVW (float2 uv, float2 flowVector, float2 jump,float flowOffset,float tiling, float time, bool flowB) 
		{
		//Combinação das duas fases
			float phaseOffset = flowB ? 0.5 : 0;
			float progress = frac(time + phaseOffset);
			float3 uvTrab;

			uvTrab.xy = uv - flowVector * (progress + flowOffset);
			uvTrab.xy *= tiling;
			uvTrab.xy += phaseOffset;
			uvTrab.xy += (time - progress) * jump;
			uvTrab.z = 1 - abs(1 - 2 * progress);
			return uvTrab;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
		//Nessa primeira parte do shader temos o tratamento dos mapas de ruidos, que são usados para criar as pulsações do shader.
			float2 flowVector = tex2D (_FlowMap, IN.uv_MainTex). rg  * 2 -1;
			flowVector *= _FlowStrength;
			float noise = tex2D(_FlowMap, IN.uv_MainTex).a;
			float time = _Time.y* _Velocidade + noise;
			float2 jump = float2(_OffSetX, _OffSety);
		//Aqui temos as duas fases do shader sitados no comentario do método: FlowUVW.
			float3 uvTrabA = FlowUVW(IN.uv_MainTex, flowVector, jump,_FlowOffset, _Tiling, time, false);
			float3 uvTrabB = FlowUVW(IN.uv_MainTex, flowVector, jump,_FlowOffset, _Tiling, time, true);
		//Declaramos dois fixed4 para guardar a transformação das texturas (Textura principal, return do método FlowUVW em X e Y) e multiplicamos pelo resultado do método em Z.
			fixed4 texA = tex2D(_MainTex, uvTrabA.xy) * uvTrabA.z;
			fixed4 texB = tex2D(_MainTex, uvTrabB.xy) * uvTrabB.z;

			fixed4 c = (texA + texB) * _Color;
			o.Albedo = c.rgb ;
			o.Emission = c.rgb * uvTrabB.rgb;
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			//o.Alpha = c.a;
		}
		ENDCG
	}

	FallBack "Diffuse"
}
