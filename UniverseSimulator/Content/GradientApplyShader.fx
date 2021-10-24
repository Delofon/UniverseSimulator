#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

texture2D gradient;
sampler2D grad_sampler = sampler_state{Texture = <gradient>;};
int width;

float4 PixelShaderFunction(float4 pos : SV_Position, float2 coords : TEXCOORD0) : COLOR0
{
	float4 gradient_color = tex2D(grad_sampler, coords);
	return gradient_color;
}

technique ParticleDrawing
{
	pass Pass1
	{
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
};