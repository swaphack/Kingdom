using UnityEngine;
using System;
using Collections;
using System.Collections.Generic;
namespace Common.CG
{
	/// <summary>
	/// 多边形划分
	/// </summary>
	internal class PolygonMonotone
	{
		/// <summary>
		/// 顶点类型
		/// </summary>
		internal enum VertexType
		{
			/// <summary>
			/// 无效顶点
			/// </summary>
			None,
			/// <summary>
			/// 开始顶点
			/// </summary>
			Start,
			/// <summary>
			/// 结束顶点
			/// </summary>
			End,
			/// <summary>
			/// 普通顶点
			/// </summary>
			Regular,
			/// <summary>
			/// 分裂顶点
			/// </summary>
			Split,
			/// <summary>
			/// 汇合顶点
			/// </summary>
			Merge,
		};

		internal class LowVertexComparer : IComparer<LineSegment>
		{
			public int Compare (LineSegment lo, LineSegment l1)
			{
				return Tool.Signal(lo.Down.y - l1.Down.y);
			}
		}

		/// <summary>
		/// 顶点类型
		/// </summary>
		/// <returns>The of vertex.</returns>
		/// <param name="node">Node.</param>
		internal static VertexType TypeOfVertex(DoubleLink<Vector2>.Node node)
		{
			if (node == null) {
				return VertexType.None;
			}
			Vector2 current = node.Value;
			Vector2 previous = node.Previous.Value;
			Vector2 next = node.Next.Value;
			if (Tool.Lower (previous, current) && Tool.Lower (next, current)) {
				float angle = Tool.Angle (current, previous, next);
				if (angle < 180) {
					return VertexType.Start;
				} else {
					return VertexType.Split;
				}
			} else if (Tool.Upper(previous, current) && Tool.Upper(next, current)) {
				float angle = Tool.Angle (current, previous, next);
				if (angle < 180) {
					return VertexType.End;
				} else {
					return VertexType.Merge;
				}
			}

			return VertexType.Regular;
		}

		/// <summary>
		/// 多边形顶点
		/// </summary>
		private DoubleLink<Vector2> _Vertexes;
		/// <summary>
		/// 线段辅助信息
		/// </summary>
		private Dictionary<LineSegment, Vector2> _Helper;
		/// <summary>
		/// 顶点类型
		/// </summary>
		private Dictionary<Vector2, VertexType> _VertexType;
		/// <summary>
		/// 二分查找树
		/// </summary>
		private BalanceBinaryTree<LineSegment> _Tree;
		/// <summary>
		/// 额外辅助边
		/// </summary>
		private List<LineSegment> _ExtSeg;

		public PolygonMonotone() {
			_Helper = new Dictionary<LineSegment, Vector2> ();
			_VertexType = new Dictionary<Vector2, VertexType> ();
			_Tree = new BalanceBinaryTree<LineSegment> ();
			_ExtSeg = new List<LineSegment> ();

			_Tree.CompareHandler = (LineSegment n1, LineSegment n2) => {
				return Tool.Compare (n1.Up, n2.Up);
			};
		}

		/// <summary>
		/// 设置线段辅助信息
		/// </summary>
		/// <param name="seg">Seg.</param>
		/// <param name="point">Point.</param>
		private void SetHelpVertex(LineSegment seg, Vector2 point) {
			if (seg == null) {
				return;
			}

			_Helper [seg] = point;
		}

		/// <summary>
		/// 获取辅助线段信息
		/// </summary>
		/// <returns><c>true</c>, if help vertex was gotten, <c>false</c> otherwise.</returns>
		/// <param name="seg">Seg.</param>
		/// <param name="point">Point.</param>
		private bool GetHelpVertex(LineSegment seg, out Vector2 point) {
			point = Vector2.zero;
			if (seg == null || !_Helper.ContainsKey(seg)) {
				return false;
			}

			point = _Helper [seg];
			return true;
		}

		/// <summary>
		/// 设置顶点类型
		/// </summary>
		/// <param name="point">Point.</param>
		/// <param name="type">Type.</param>
		private void SetVertexType(Vector2 point, VertexType type) {
			_VertexType [point] = type;
		}

		/// <summary>
		/// 获取顶点类型
		/// </summary>
		/// <returns>The vertex type.</returns>
		/// <param name="point">Point.</param>
		private VertexType GetVertexType(Vector2 point) {
			if (_VertexType.ContainsKey (point)) {
				return _VertexType [point];
			} else {
				return VertexType.None;
			}
		}

		/// <summary>
		/// 添加额外边
		/// </summary>
		/// <param name="seg">Seg.</param>
		private void AddExtSeg(LineSegment seg) {
			if (seg == null) {
				return;
			}

			_ExtSeg.Add (seg);
		}

		/// <summary>
		/// 查找近邻的线段
		/// </summary>
		/// <returns>The near line.</returns>
		/// <param name="node">Node.</param>
		private LineSegment FindNearLine(Vector2 node) {
			LineSegment nearSeg = null;
			_Tree.Foreach ((BalanceBinaryTree<LineSegment>.Node temp) => {
				if (temp == null) {
					return;
				}

				if (temp.Value == null) {
					return;
				}

				LineSegment tempSeg = temp.Value;
				if (tempSeg.Up.y >= node.y && tempSeg.Down.y <=  node.y) {
					float v0 = Vector2.Dot(node - tempSeg.Up, tempSeg.Down - tempSeg.Up);
					if (v0 > 0) {
						if (nearSeg == null) {
							nearSeg = tempSeg;
						} else {
							float v1 = Vector2.Dot(node - nearSeg.Up, nearSeg.Down - nearSeg.Up);
							if (v0 < v1) {
								nearSeg = tempSeg;
							}
						}	
					}
				}
			});

			return nearSeg;
		}

		/// <summary>
		/// 点是否在左边
		/// </summary>
		/// <returns><c>true</c>, if left side was ined, <c>false</c> otherwise.</returns>
		/// <param name="point">Point.</param>
		private bool InLeftSide(Vector2 point) {
			if (_Vertexes == null) {
				return false;
			}
			DoubleLink<Vector2>.Node node = _Vertexes.First;
			if (node == null) {
				return false;
			}
			do {
				if (node.Value.x > point.x) {
					return true;
				}
				node = node.Next;
			} while(node != _Vertexes.First);

			return false;
		}

		/// <summary>
		/// 将简单多边形划分为y-单调多边形
		/// 逆时针
		/// </summary>
		/// <param name="polygon">Polygon.</param>
		public Polygon[] Divide(Polygon polygon) {
			if (polygon == null) {
				return null;
			}

			_Vertexes = polygon.Vertexes;
			_Helper.Clear ();
			_VertexType.Clear ();
			_Tree.Clear ();
			_ExtSeg.Clear ();

			Vector2[] points = _Vertexes.ToArray ();
			if (points == null) {
				return null;
			}
			Array.Sort(points, new Vector2Comparer());
			/*
			Vector2 first = points [0];
			DoubleLink<Vector2>.Node firstNode = _Vertexes.Find (first);
			DoubleLink<Vector2>.Node node = firstNode;
			if (firstNode.Value.x < firstNode.Next.Value.x) { // 反向
				do {
					HandleVertex (node.Value);
					node = node.Previous;
				} while(node != firstNode);
			} else { // 同向
				do {
					HandleVertex (node.Value);
					node = node.Next;
				} while(node != firstNode);
			}
			*/

			for (int i = 0; i < points.Length; i++) {
				HandleVertex (points [i]);
			}

			if (_ExtSeg.Count == 0) {
				return new Polygon[] { polygon };
			} else {
				return Monotone (points[0]);
			}
		}

		/// <summary>
		/// 处理顶点
		/// </summary>
		/// <param name="point">Point.</param>
		private void HandleVertex(Vector2 point) {
			DoubleLink<Vector2>.Node node = _Vertexes.Find (point);
			if (node == null) {
				return;
			}

			VertexType type = TypeOfVertex (node);

			Debug.Log (type);

			switch (type) {
			case VertexType.Start: HandleStartVertex (node); break;
			case VertexType.End: HandleEndVertex (node); break;
			case VertexType.Split: HandleSpiltVertex (node); break;
			case VertexType.Merge: HandleMergeVertex (node); break;
			case VertexType.Regular: HandleRegularVertex (node); break;
			default:
				break;
			}
		}

		/// <summary>
		/// 处理开始顶点
		/// </summary>
		/// <param name="point">Point.</param>
		private void HandleStartVertex(DoubleLink<Vector2>.Node node) {
			Vector2 current = node.Value;
			Vector2 next = node.Next.Value;
			LineSegment seg = new LineSegment (current, next);

			_Tree.Add (seg);
			SetHelpVertex (seg, current);
		}

		/// <summary>
		/// 处理结束顶点
		/// </summary>
		/// <param name="point">Point.</param>
		private void HandleEndVertex(DoubleLink<Vector2>.Node node) {
			Vector2 current = node.Value;
			Vector2 pre = node.Previous.Value;
			LineSegment preSeg = new LineSegment (pre, current);
			Vector2 helpVertex;
			if (GetHelpVertex (preSeg, out helpVertex)) {
				if (GetVertexType (helpVertex) == VertexType.Merge) {
					LineSegment newSeg = new LineSegment (current, helpVertex);
					AddExtSeg (newSeg);
				}
			}

			_Tree.Remove (preSeg);

		}
		/// <summary>
		/// 处理分裂顶点
		/// </summary>
		/// <param name="point">Point.</param>
		private void HandleSpiltVertex(DoubleLink<Vector2>.Node node) {
			Vector2 current = node.Value;
			LineSegment nearSeg = FindNearLine (current);
			if (nearSeg != null) {
				Vector2 helpVertex;
				if (GetHelpVertex (nearSeg, out helpVertex)) {
					LineSegment newSeg = new LineSegment (current, helpVertex);
					AddExtSeg (newSeg);
					SetHelpVertex (newSeg, current);
				}
			}

			Vector2 next = node.Next.Value;
			LineSegment seg = new LineSegment (current, next);

			_Tree.Add (seg);
			SetHelpVertex (seg, current);
		}

		/// <summary>
		/// 处理汇合顶点
		/// </summary>
		/// <param name="point">Point.</param>
		private void HandleMergeVertex(DoubleLink<Vector2>.Node node) {
			Vector2 current = node.Value;

			Vector2 pre = node.Previous.Value;
			LineSegment preSeg = new LineSegment (pre, current);
			Vector2 helpVertex;
			if (GetHelpVertex (preSeg, out helpVertex)) {
				if (GetVertexType (helpVertex) == VertexType.Merge) {
					LineSegment newSeg = new LineSegment (current, helpVertex);
					AddExtSeg (newSeg);
				}
			}

			_Tree.Remove (preSeg);

			LineSegment nearSeg = FindNearLine (current);
			if (nearSeg != null) {
				if (GetHelpVertex (nearSeg, out helpVertex)) {
					if (GetVertexType (helpVertex) == VertexType.Merge) {
						LineSegment newSeg = new LineSegment (current, helpVertex);
						AddExtSeg (newSeg);
					}
				}
			}

			Vector2 next = node.Next.Value;
			LineSegment seg = new LineSegment (current, next);
			SetHelpVertex (seg, current);
		}

		/// <summary>
		/// 处理正常节点
		/// </summary>
		/// <param name="point">Point.</param>
		private void HandleRegularVertex(DoubleLink<Vector2>.Node node) {
			Vector2 current = node.Value;
			if (!InLeftSide (current)) {
				return;
			}

			Vector2 pre = node.Previous.Value;
			LineSegment preSeg = new LineSegment (pre, current);
			Vector2 helpVertex;
			if (GetHelpVertex (preSeg, out helpVertex)) {
				if (GetVertexType (helpVertex) == VertexType.Merge) {
					LineSegment newSeg = new LineSegment (current, helpVertex);
					AddExtSeg (newSeg);

					_Tree.Remove (preSeg);

					Vector2 next = node.Next.Value;
					LineSegment seg = new LineSegment (current, next);
					_Tree.Add (seg);
					SetHelpVertex (seg, current);
				} else {
					LineSegment nearSeg = FindNearLine (current);
					if (nearSeg != null) {
						if (GetHelpVertex (nearSeg, out helpVertex)) {
							if (GetVertexType (helpVertex) == VertexType.Merge) {
								LineSegment newSeg = new LineSegment (current, helpVertex);
								AddExtSeg (newSeg);
								SetHelpVertex (nearSeg, current);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 多边形划分
		/// 从上到下划分
		/// </summary>
		private Polygon[] Monotone(Vector2 up) {
			LineSegment[] segs = _ExtSeg.ToArray ();
			// 排序，从上到下开始分割多边形
			Array.Sort(segs, new LowVertexComparer());

			DoubleLink<Vector2>.Node node = _Vertexes.Find (up);
			if (node == null) {
				return null;
			}

			int divideCount = _ExtSeg.Count + 1;
			Polygon[] ps = new Polygon[divideCount];
			DoubleLink<Vector2>.Node last = null;
			for (int i = 0; i < divideCount; i++) {
				List<Vector2> lst = new List<Vector2> ();
				DoubleLink<Vector2>.Node temp = null;
				LineSegment seg = _ExtSeg [i];
				while (true) {
					lst.Add (node.Value);
					if (seg.IsVertex (node.Value)) { // 是否是划分线段
						last = node;
						if (seg.P0 == node.Value) {
							temp = _Vertexes.Find (seg.P1);
							node = temp;
						} else {
							temp = _Vertexes.Find (seg.P0);
							node = temp;
						}
					} else {
						node = node.Next;
						if (node.Value == lst[0]) { // 产生闭环
							// 生成多边形
							Vector2[] vertexes = lst.ToArray ();
							ps[i] = new Polygon();
							ps[i].SetVertexes(vertexes);
							lst.Clear();
							// 剔除已规划的顶点
							for (int j = 0; j < vertexes.Length; j++) {
								if (!seg.IsVertex (vertexes [i])) {
									_Vertexes.Remove (vertexes [i]);
								}
							}

							// 重置顶点，回到改变前的节点
							if (last != null) {
								node = last;
								last = null;
							}
							break;
						}
					}
				}
			}

			return ps;
		}
	}
}

