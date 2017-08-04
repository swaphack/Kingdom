using UnityEngine;
using System.Collections.Generic;

namespace Common.CG
{
	/// <summary>
	/// 图形
	/// </summary>
	public class Shape
	{
		/// <summary>
		/// 顶点
		/// </summary>
		private List<Vector2> _VertexList;

		/// <summary>
		/// 顶点
		/// </summary>
		/// <value>The vertexes.</value>
		public Vector2[] Vertexes {
			get { 
				return _VertexList.ToArray();
			}
		}

		public Shape ()
		{
			_VertexList = new List<Vector2> ();
		}

		public void Add(Vector2 vertex) {
			_VertexList.Add (vertex);
		}

		public void Remove(Vector2 vertex) {
			_VertexList.Remove (vertex);
		}

		public void Clear() {
			_VertexList.Clear ();
		}
	}
}

