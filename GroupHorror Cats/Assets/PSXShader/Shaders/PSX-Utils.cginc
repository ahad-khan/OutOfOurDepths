/*
Shader Features
    -PSX_VERTEX_LIT
    -PSX_CUTOUT_VAL [float threshold]
	-PSX_CUBEMAP [texCUBE cubemap]
		-PSX_CUBEMAP_COLOR [float intensity]

Shader Customization
    -PSX_TRIANGLE_SORTING_FUNC [float4 v1, float4 v2, float4 v3]

*/

//Globals set by PSXShaderManager.cs
float _PSX_GridSize;
float _PSX_DepthDebug;

//Math Utils
float3 SnapVertexToGrid(float3 vertex)
{
    return _PSX_GridSize < 0.00001f ? vertex : (floor(vertex * _PSX_GridSize) / _PSX_GridSize);
}

float4 CalculateAffineUV(float3 vertex, float2 uv) 
{
    float4 affineUV;
    affineUV.z = length(vertex);
    affineUV = float4(uv * affineUV.z, affineUV.z, 0);
    return affineUV;
}

//Triangle sorting functions. Input is 3 object-space verts, output is the custom depth to be used by the entire triangle.
float GetTriangleSortingDepth_CenterDepth(float4 v1, float4 v2, float4 v3)
{
    float4 center = UnityObjectToClipPos((v1 + v2 + v3) * 0.3333f);
	//Output clip space vertex z/w to simulate how depth is calculated for a regular depth buffer.
	return center.z / center.w;
}

float GetTriangleSortingDepth_ClosestVertexDepth(float4 v1, float4 v2, float4 v3)
{
	v1 = UnityObjectToClipPos(v1);
	v2 = UnityObjectToClipPos(v2);
	v3 = UnityObjectToClipPos(v3);

	//Clip space w can be negative if the vertex is off-screen and it messes up the calculations.
	//Only consider triangles whose w is positive. lerp and step are a lot cheaper than conditionals.
	float depth = 1;
	depth = lerp(depth, min(depth, v1.z/v1.w), step(0, v1.w));
	depth = lerp(depth, min(depth, v2.z/v2.w), step(0, v2.w));
	depth = lerp(depth, min(depth, v3.z/v3.w), step(0, v3.w));
	return depth;
    //return min(v1.z/v1.w, min(v2.z/v2.w, v3.z/v3.w));
}

//This function doesn't try to mimic the value distribution of a regular depth buffer, but still works
//well if only PSX shaders are used in your scene. Outputs view space triangle center distance divided by the far clip plane value.
float GetTriangleSortingDepth_LinearCenterDistanceDepth(float4 v1, float4 v2, float4 v3)
{
	v1.xyz = UnityObjectToViewPos(v1);
	v2.xyz = UnityObjectToViewPos(v2);
	v3.xyz = UnityObjectToViewPos(v3);

	return 1 - length((v1 + v2 + v3).xyz * 0.333f) / _ProjectionParams.z;
}

//Custom template.
float GetTriangleSortingDepth_Custom(float4 v1, float4 v2, float4 v3)
{
    return 0;
}

//Change this to use the function that's most suitable for your scene, or to GetTriangleSortingDepth_Custom if you want to make your own.
#define PSX_TRIANGLE_SORTING_FUNC(v1, v2, v3) GetTriangleSortingDepth_ClosestVertexDepth(v1, v2, v3)