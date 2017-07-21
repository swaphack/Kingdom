using System;
using UnityEngine;

namespace Common.CG
{
	/// <summary>
	/// 多边形
	/// </summary>
	public class CGPolygon
	{
		internal struct TriangleVertex
		{
			public int V0;
			public int V1;
			public int V2;
		}
		
		private CGPolygon ()
		{
		}

		/// <summary>
		/// 多变形划分为三角形的索引
		/// </summary>
		/// <returns>The of triangles.</returns>
		/// <param name="Points">Points.</param>
		public static int[] IndicesOfTriangles(Vector2[] points)
		{
			if (points == null) {
				return null;
			}

			int triangleCount = points.Length - 2;
			int[] indices = new int[triangleCount * 3];

			for (int i = 0; i < points.Length; i++) {
				
			}

			return null;
		}
	}
}

