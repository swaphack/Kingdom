using System;
using UnityEngine;

namespace Common.CG
{
	/// <summary>
	/// 凸包
	/// </summary>
	public struct CGConvex
	{
		/// <summary>
		/// 是否是凸包
		/// </summary>
		/// <returns><c>true</c>, if convex was ised, <c>false</c> otherwise.</returns>
		/// <param name="points">Points.</param>
		public static bool isConvex(Vector2[] points)
		{
			if (points == null || points.Length < 3) {
				return false;
			}

			int count = points.Length;

			float lastDirection = -1;

			for (int i = 0; i < count; i++)
			{
				Vector2 p0 = points[(i + count) % count];
				Vector2 p1 = points[(i + 1 + count) % count];
				Vector2 p2 = points[(i + 2 + count) % count];

				Vector2 v0 = p1 - p0;
				Vector2 v1 = p2 - p1;

				float vd = Vector2.Dot (v0, v1);
				vd = vd > 0 ? 1 : vd == 0 ? 0 : 2;
				if (vd == 0)
				{
					return false;
				}

				if (lastDirection == -1)
				{
					lastDirection = vd;
				}
				else if (lastDirection != vd)
				{
					return false;
				}
			}

			return true;
		}
	}
}

