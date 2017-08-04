using UnityEngine;

namespace Common.CG
{
	/// <summary>
	/// 柏林噪声
	/// </summary>
	public class PerlinNoise
	{
		private PerlinNoise ()
		{
		}

		/// <summary>
		/// 设置种子
		/// </summary>
		/// <value>The seed.</value>
		public static int Seed {
			set { 
				Random.InitState (value);
			}
		}

		#region interpolation

		/// <summary>
		/// 线性插值
		/// interpolation 是0到1之间的值
		/// </summary>
		/// <param name="begin">Begin.</param>
		/// <param name="end">End.</param>
		/// <param name="interpolation">Interpolation.</param>
		public static float LinearInterpolation (float begin, float end, float interpolation)
		{
			interpolation = Mathf.Max (0, interpolation);
			interpolation = Mathf.Min (1, interpolation);

			return begin * (1 - interpolation) + end * interpolation;
		}

		/// <summary>
		/// 余弦插值
		/// interpolation 是0到1之间的值
		/// </summary>
		/// <param name="begin">Begin.</param>
		/// <param name="end">End.</param>
		/// <param name="interpolation">Interpolation.</param>
		public static float CosineInterpolation (float begin, float end, float interpolation)
		{
			interpolation = Mathf.Max (0, interpolation);
			interpolation = Mathf.Min (1, interpolation);

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
		public static float CubicInterpolation (float beforeSrc, float src, float dest, float afterDest, float interpolation)
		{
			interpolation = Mathf.Max (0, interpolation);
			interpolation = Mathf.Min (1, interpolation);

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
		public static float Noise1D (float x)
		{
			int nx = (int)x;

			nx = Random.Range (0, nx);

			nx = (nx << 13) ^ nx;
			return 1.0f - ((nx * (nx * nx * 15731 + 789211) + 1373612589) & 0x7ffffff) / 1073741824.0f;
		}

		/// <summary>
		/// 平滑一维噪声
		/// </summary>
		/// <returns>The noise1 d.</returns>
		/// <param name="x">The x coordinate.</param>
		public static float SmoothNoise1D (float x)
		{
			return Noise1D (x) / 2 + Noise1D (x - 1) / 4 + Noise1D (x + 1) / 4;
		}

		/// <summary>
		/// 插值一维噪声
		/// </summary>
		/// <returns>The noise 1d.</returns>
		/// <param name="x">The x coordinate.</param>
		public static float InterpolatedNoise1D (float x)
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
		public static float PerlinNoise1D (float x, float persistence, int octaves)
		{
			float total = 0;
			for (int i = 0; i < octaves; i++) {
				float amplitude = Mathf.Pow (persistence, i);
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
		public static float Noise2D (float x, float y)
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
		public static float SmoothNoise2D (float x, float y)
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
		public static float InterpolatedNoise2D (float x, float y)
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
		public static float PerlinNoise2D (float x, float y, float persistence, int octaves)
		{
			float total = 0;
			for (int i = 0; i < octaves; i++) {
				float amplitude = Mathf.Pow (persistence, i);
				float frequency = Mathf.Pow (2, i);
				total += InterpolatedNoise2D (x * frequency, y * frequency) * amplitude;
			}
			return total;
		}

		#endregion
	}

}