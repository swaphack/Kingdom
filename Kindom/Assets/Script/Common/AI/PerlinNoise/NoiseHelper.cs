using System;
using UnityEngine;

/// <summary>
/// 复制
/// </summary>
public class NoiseHelper
{
	private NoiseHelper ()
	{
	}

	#region interpolation

	/// <summary>
	/// 线性插值
	/// interpolation 是0到1之间的值
	/// </summary>
	/// <param name="begin">Begin.</param>
	/// <param name="end">End.</param>
	/// <param name="interpolation">Interpolation.</param>
	public static float LinearInterpolation(float begin, float end, float interpolation)
	{
		interpolation = Mathf.Max(0, interpolation);
		interpolation = Mathf.Min(1, interpolation);

		return begin * (1 - interpolation) + end * interpolation;
	}

	/// <summary>
	/// 余弦插值
	/// interpolation 是0到1之间的值
	/// </summary>
	/// <param name="begin">Begin.</param>
	/// <param name="end">End.</param>
	/// <param name="interpolation">Interpolation.</param>
	public static float CosineInterpolation(float begin, float end, float interpolation)
	{
		interpolation = Mathf.Max(0, interpolation);
		interpolation = Mathf.Min(1, interpolation);

		float ft = interpolation * Mathf.PI;
		float f = (1 - Mathf.Cos (ft)) * 0.5f;
		return begin * (1 - f) + end * f;
	}

	/// <summary>
	/// 立方插值
	/// </summary>
	/// <param name="beforeSrc">Before source.</param>
	/// <param name="src">Source.</param>
	/// <param name="dest">Destination.</param>
	/// <param name="afterDest">After destination.</param>
	/// <param name="interpolation">Interpolation.</param>
	public static float CubicInterpolation(float beforeSrc, float src, float dest, float afterDest, float interpolation)
	{
		interpolation = Mathf.Max(0, interpolation);
		interpolation = Mathf.Min(1, interpolation);

		float p = (afterDest - dest) - (beforeSrc - src);
		float q = (beforeSrc - src) - p;
		float r = afterDest - beforeSrc;
		float s = src;

		return p * Mathf.Pow (interpolation, 3) + q * Mathf.Pow (interpolation, 2) + r * interpolation + s;
	}

	#endregion


	#region noise 1D
	/// <summary>
	/// 一维噪声函数
	/// 随机一个-1到1之间的数
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	public static float Noise1D(float x)
	{
		int nx = (int)x;
		nx = (nx << 13) ^ nx;
		return 1.0f - ((nx * (nx * nx * 15731 + 789211) + 1373612589) & 0x7ffffff) / 1073741824.0f;
	}

	/// <summary>
	/// 平滑一维噪声
	/// </summary>
	/// <returns>The noise1 d.</returns>
	/// <param name="x">The x coordinate.</param>
	public static float SmoothNoise1D(float x)
	{
		return Noise1D (x) / 2 + Noise1D (x - 1) / 4 + Noise1D (x + 1) / 4;
	}

	/// <summary>
	/// 插值一维噪声
	/// </summary>
	/// <returns>The noise 1d.</returns>
	/// <param name="x">The x coordinate.</param>
	public static float InterpolatedNoise1D(float x)
	{
		int nx = (int)x;
		float fx = x - nx;

		float v1 = SmoothNoise1D (nx);
		float v2 = SmoothNoise1D (nx + 1);

		return CosineInterpolation (v1, v2, fx);
	}

	/// <summary>
	/// 柏林噪声
	/// </summary>
	/// <returns>The noise1 d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="persistence">Persistence.</param>
	/// <param name="octaves">Count.</param>
	public static float PerlinNoise1D(float x, float persistence, int octaves)
	{
		float total = 0;
		for (int i = 0; i < octaves; i++) {
			float amplitude = Mathf.Pow(persistence, i);
			float frequency = Mathf.Pow (2, i);
			total += InterpolatedNoise1D (x * frequency) * amplitude;
		}
		return total;
	}

	#endregion

	#region noise 2D
	/// <summary>
	/// 二维噪声函数
	/// </summary>
	/// <returns>The d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static float Noise2D(float x, float y)
	{
		float n = x + y * 57;
		return Noise1D (n);
	}


	/// <summary>
	/// 平滑二维噪声
	/// </summary>
	/// <returns>The noise2 d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static float SmoothNoise2D(float x, float y)
	{
		float corners = (Noise2D (x - 1, y - 1)
		                + Noise2D (x + 1, y + 1)
		                + Noise2D (x - 1, y + 1)
		                + Noise2D (x + 1, y + 1)) / 16.0f;
		
		float sides = (Noise2D (x - 1, y)
		              + Noise2D (x + 1, y)
		              + Noise2D (x, y - 1)
		              + Noise2D (x, y + 1)) / 8;
		
		float center = Noise2D (x, y) / 4;
		return corners + sides + center;
	}

	/// <summary>
	/// 插值二维噪声
	/// </summary>
	/// <returns>The noise2 d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static float InterpolatedNoise2D(float x, float y)
	{
		int nx = (int)x;
		float fx = x - nx;
		int ny = (int)y;
		float fy = y - ny;

		float v1 = SmoothNoise2D (nx, ny);
		float v2 = SmoothNoise2D (nx + 1, ny);
		float v3 = SmoothNoise2D (nx, ny + 1);
		float v4 = SmoothNoise2D (nx + 1, ny + 1);

		float i1 = CosineInterpolation (v1, v2, fx);
		float i2 = CosineInterpolation (v3, v4, fx);

		return CosineInterpolation (i1, i2, fy);
	}

	/// <summary>
	/// Perlins the noise2 d.
	/// </summary>
	/// <returns>The noise2 d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="persistence">Persistence.</param>
	/// <param name="octaves">Count.</param>
	public static float PerlinNoise2D(float x, float y, float persistence, int octaves)
	{
		float total = 0;
		for (int i = 0; i < octaves; i++) {
			float amplitude = Mathf.Pow(persistence, i);
			float frequency = Mathf.Pow (2, i);
			total += InterpolatedNoise2D (x * frequency, y * frequency) * amplitude;
		}
		return total;
	}

	#endregion

	#region noise 3D
	/// <summary>
	/// 三维噪声函数
	/// </summary>
	/// <returns>The d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static float Noise3D(float x, float y, float z)
	{
		float n = x + y * 33;
		return Noise2D (n, z);
	}


	/// <summary>
	/// 平滑三维噪声
	/// </summary>
	/// <returns>The noise3 d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static float SmoothNoise3D(float x, float y, float z)
	{
		float corners = (Noise3D (x - 1, y - 1, z - 1)
		                + Noise3D (x - 1, y - 1, z + 1)
						+ Noise3D (x - 1, y + 1, z - 1)
						+ Noise3D (x - 1, y + 1, z + 1)
		                + Noise3D (x + 1, y - 1, z - 1)
		                + Noise3D (x + 1, y - 1, z + 1)
		                + Noise3D (x + 1, y + 1, z - 1)
		                + Noise3D (x + 1, y + 1, z + 1)) / 32.0f;
		
		float sides = (Noise3D (x - 1, y, z)
		              + Noise3D (x + 1, y, z)
		              + Noise3D (x, y - 1, z)
		              + Noise3D (x, y + 1, z)
		              + Noise3D (x, y, z - 1)
		              + Noise3D (x, y, z + 1)) / 12;
		
		float center = Noise3D (x, y, z) / 4;

		return corners + sides + center;
	}

	/// <summary>
	/// 插值三维噪声
	/// </summary>
	/// <returns>The noise3 d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static float InterpolatedNoise3D(float x, float y, float z)
	{
		int nx = (int)x;
		float fx = x - nx;
		int ny = (int)y;
		float fy = y - ny;
		int nz = (int)z;
		float fz = z - nz;

		float v1 = SmoothNoise3D (nx, ny, nz);
		float v2 = SmoothNoise3D (nx + 1, ny, nz);

		float v3 = SmoothNoise3D (nx, ny + 1, nz);
		float v4 = SmoothNoise3D (nx + 1, ny + 1, nz);

		float v5 = SmoothNoise3D (nx, ny, nz + 1);
		float v6 = SmoothNoise3D (nx + 1, ny, nz + 1);

		float v7 = SmoothNoise3D (nx, ny + 1, nz + 1);
		float v8 = SmoothNoise3D (nx + 1, ny + 1, nz + 1);

		float i12 = CosineInterpolation (v1, v2, fx);
		float i34 = CosineInterpolation (v3, v4, fx);

		float c1234 = CosineInterpolation (i12, i34, fy);

		float i56 = CosineInterpolation (v5, v6, fx);
		float i78 = CosineInterpolation (v7, v8, fx);
		float c5678 = CosineInterpolation (i56, i78, fy);

		return CosineInterpolation (c1234, c5678, fz);
	}

	/// <summary>
	/// Perlins the noise 3d.
	/// </summary>
	/// <returns>The noise3 d.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="persistence">Persistence.</param>
	/// <param name="count">Count.</param>
	public static float PerlinNoise3D(float x, float y, float z, float persistence, int octaves)
	{
		float total = 0;
		for (int i = 0; i < octaves; i++) {
			float amplitude = Mathf.Pow(persistence, i);
			float frequency = Mathf.Pow (2, i);
			total += InterpolatedNoise3D (x * frequency, y * frequency, z * frequency) * amplitude;
		}
		return total;
	}

	#endregion
}

