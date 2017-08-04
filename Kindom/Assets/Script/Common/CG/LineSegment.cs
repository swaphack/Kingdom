using UnityEngine;

namespace Common.CG
{
	/// <summary>
	/// 线段
	/// </summary>
	public class LineSegment
	{
		/// <summary>
		/// 第一个顶点
		/// </summary>
		private Vector2 _P0;
		/// <summary>
		/// 第二个顶点
		/// </summary>
		private Vector2 _P1;
		/// <summary>
		/// 中心点
		/// </summary>
		private Vector2 _Center;

		/// <summary>
		/// 位于上方的顶点
		/// </summary>
		private Vector2 _Up;
		/// <summary>
		/// 位于下方的顶点
		/// </summary>
		private Vector2 _Down;
		/// <summary>
		/// 位于左方的顶点
		/// </summary>
		private Vector2 _Left;
		/// <summary>
		/// 位于右方的顶点
		/// </summary>
		private Vector2 _Right;


		public Vector2 P0 { get { return _P0; }}
		public Vector2 P1 { get { return _P1; }}
		public Vector2 Center { get { return _Center; }}

		public Vector2 Up { get { return _Up; }}
		public Vector2 Down { get { return _Down; }}
		public Vector2 Left { get { return _Left; }}
		public Vector2 Right { get { return _Right; }}

		public LineSegment(Vector2 p0, Vector2 p1) 
		{
			_P0 = p0;
			_P1 = p1;

			_Center = (p0 + p1) * 0.5f;

			if (p0.y < p1.y) {
				_Up = p1;
				_Down = p0;
			} else {
				_Up = p0;
				_Down = p1;
			}

			if (p0.x < p1.x) {
				_Left = p0;
				_Right = p1;
			} else {
				_Left = p1;
				_Right = p0;
			}
		}

		/// <summary>
		/// 判断线段是否相交
		/// </summary>
		/// <param name="line">Line.</param>
		public bool Intersect(LineSegment line) {
			if (Down.y > line.Up.y || Up.y < line.Down.y) {
				return false;
			}

			if (Left.x > line.Right.x || Right.x < line.Left.x) {
				return false;
			}

			if (Tool.Signal(Tool.Area(line.P0, P1, line.P1) * Tool.Area(line.P0, line.P1, P0 )) < 0) {
				return false;
			}

			if (Tool.Signal (Tool.Area (P0, line.P1, P1) * Tool.Area (P0, P1, line.P0)) < 0) {
				return false;
			}

			return true;
		}

		/// <summary>
		/// 求线段的交点
		/// </summary>
		/// <returns><c>true</c>, if intersection was found, <c>false</c> otherwise.</returns>
		/// <param name="line">Line.</param>
		/// <param name="point">Point.</param>
		public bool FindIntersection(LineSegment line, out Vector2 point) {
			point = Vector2.zero;
			float f0 = Mathf.Abs (Tool.Area (P0, P1, line.P0));
			float f1 = Mathf.Abs (Tool.Area (P0, P1, line.P1));
			if (f1 == 0) { // 共线
				return false;
			}

			float k = f0 / f1;

			point.x = (line.P0.x + k * line.P1.x) / (1 + k);
			point.y = (line.P0.y + k * line.P1.y) / (1 + k);

			return true;
		}

		/// <summary>
		/// 判断点是否是顶点
		/// </summary>
		/// <returns><c>true</c> if this instance is vertex the specified point; otherwise, <c>false</c>.</returns>
		/// <param name="point">Point.</param>
		public bool IsVertex(Vector2 point) {
			return point == P0 || point == P1;
		}
	}
}

