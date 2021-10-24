#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

texture gradient;
sampler gradient_sampler = sampler_state { Texture = (gradient); Filter = POINT; };
int width;

float4 PixelShaderFunction(float4 pos : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
	float4 gradient_color = tex2D(gradient_sampler, float2(0, 0));
	return gradient_color;
}

technique ParticleDrawing
{
	pass Pass1
	{
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
};