using UnityEngine;
using System;
using Collections;
using System.Collections.Generic;

namespace Common.CG
{	
	/// <summary>
	/// 多边形
	/// </summary>
	public class Polygon
	{
		/// <summary>
		/// 顶点
		/// </summary>
		private DoubleLink<Vector2> _Vertexes;

		public DoubleLink<Vector2> Vertexes {
			get { 
				return _Vertexes;
			}
		}

		public Polygon ()
		{
			_Vertexes = new DoubleLink<Vector2> ();
			_Vertexes.CompareHandler = (Vector2 n0, Vector2 n1)=>{
				return Tool.Compare(n0, n1);
			};
		}

		/// <summary>
		/// 设置顶点
		/// </summary>
		/// <param name="vertexes">Vertexes.</param>
		public void SetVertexes(Vector2[] vertexes) {
			if (vertexes == null) {
				return;
			}

			_Vertexes.Clear ();

			for (int i = 0; i < vertexes.Length; i++) {
				_Vertexes.Add (vertexes [i]);
			}
		}

		/// <summary>
		/// 将多边形划分为多个单调多变形
		/// </summary>
		/// <returns>The monotone.</returns>
		public Polygon[] MakeMonotone() {
			return new PolygonMonotone ().Divide (this);
		}
	}
}

