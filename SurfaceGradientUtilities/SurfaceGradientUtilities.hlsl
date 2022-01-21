void SurfaceGradientFromPerturbedNormal_float ( in float3 nrmVertexNormal, in float3 v, out float3 surfaceGradient)
{
	surfaceGradient = SurfaceGradientFromPerturbedNormal( nrmVertexNormal, v );
}

void SurfaceGradientFromPerturbedNormal_half ( in half3 nrmVertexNormal, in half3 v, out half3 surfaceGradient)
{
	surfaceGradient = SurfaceGradientFromPerturbedNormal( nrmVertexNormal, v );
}