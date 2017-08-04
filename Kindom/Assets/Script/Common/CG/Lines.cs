using UnityEngine;
using System.Collections.Generic;
using System;

namespace Common.CG
{
	/// <summary>
	/// 线段集合
	/// </summary>
	public class Lines
	{
		/// <summary>
		/// 顶点类型
		/// </summary>
		internal enum VertexType
		{
			/// <summary>
			/// 上端点
			/// </summary>
			UP,
			/// <summary>
			/// 下端点
			/// </summary>
			DOWN,
			/// <summary>
			/// 交叉点
			/// </summary>
			INTERSECT,
		}

		/// <summary>
		/// 顶点信息
		/// </summary>
		internal struct VertexInfo
		{
			/// <summary>
			/// 类型
			/// </summary>
			public VertexType Type;
			/// <summary>
			/// 顶点
			/// </summary>
			public Vector2 Vertex;
			/// <summary>
			/// 关联的线段
			/// </summary>
			public LineSegment Line;

			public VertexInfo(Vector2 vertex, VertexType type) {
				Vertex = vertex;
				Type = type;
				Line = null;
			}
		}

		/// <summary>
		/// 线段排序
		/// </summary>
		internal class LineSegComparer : IComparer<LineSegment>
		{
			public int Compare (LineSegment l0, LineSegment l1)
			{
				Vector2 v0 = l0.Up;
				Vector2 v1 = l1.Up;
				return Tool.Compare (v0, v1);
			}
		}

		/// <summary>
		/// 关联线段
		/// </summary>
		internal class LinkLines
		{
			/// <summary>
			/// 关联的线段
			/// </summary>
			private Dictionary<Vector2, List<LineSegment>> _LinkLines;

			public LinkLines()
			{
				_LinkLines = new Dictionary<Vector2, List<LineSegment>>();
			}

			/// <summary>
			/// 添加端点关联的线段
			/// </summary>
			/// <param name="point">Point.</param>
			/// <param name="line">Line.</param>
			public void AddLinkLine(Vector2 point, LineSegment line) {
				if (!_LinkLines.ContainsKey (point)) {
					_LinkLines [point] = new List<LineSegment> ();
				}

				if (line != null) {
					_LinkLines [point].Add (line);
				}
			}

			/// <summary>
			/// 获取端点关联的线段
			/// </summary>
			/// <returns>The link lines.</returns>
			/// <param name="point">Point.</param>
			public List<LineSegment> GetLinkLines(Vector2 point) {
				if (_LinkLines.ContainsKey (point)) {
					return _LinkLines [point];
				}
				return null;
			}
		}


		/// <summary>
		/// 上端点关联的线段
		/// </summary>
		private LinkLines _UpLinkLines;
		/// <summary>
		/// 下端点关联的线段
		/// </summary>
		private LinkLines _LowLinkLines;
		/// <summary>
		/// 内部点关联的线段
		/// </summary>
		private LinkLines _ContainLinkLines;

		public Lines ()
		{
			_UpLinkLines = new LinkLines ();
			_LowLinkLines = new LinkLines ();
			_ContainLinkLines = new LinkLines ();
		}



		private void HandleEventPoint(VertexInfo info) {
			
		}

		/// <summary>
		/// 求平面中所有线段的交点
		/// </summary>
		/// <returns>The intersect points.</returns>
		/// <param name="lines">Lines.</param>
		public Vector2[] FindIntersectPoints(LineSegment[] lineAry) {
			if (lineAry == null || lineAry.Length == 0) {
				return null;
			}

			// 排序
			Array.Sort (lineAry, new LineSegComparer ());
				
			// 拥有相同上端点的线段
			Queue<VertexInfo> infoQueue = new Queue<VertexInfo> ();
			for (int i = 0; i < lineAry.Length; i++) {
				VertexInfo topInfo = new VertexInfo(lineAry [i].Up, VertexType.UP);
				topInfo.Line = lineAry [i];
				infoQueue.Enqueue (topInfo);
				_UpLinkLines.AddLinkLine (topInfo.Vertex, topInfo.Line);

				VertexInfo downInfo = new VertexInfo(lineAry [i].Down, VertexType.DOWN);
				infoQueue.Enqueue (downInfo);
				_LowLinkLines.AddLinkLine (downInfo.Vertex, null);

			}

			while (infoQueue.Count != 0) {
				VertexInfo info = infoQueue.Dequeue ();
				HandleEventPoint (info);
			}

			return null;
		}
	}
}

