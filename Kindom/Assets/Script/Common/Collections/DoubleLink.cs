using System;
using System.Collections.Generic;
using System.Text;

namespace Collections
{
	/// <summary>
	/// 双向链表
	/// </summary>
	public class DoubleLink<T> : Compare<T>
	{
		public class Node : CNode<T>
		{
			private Node _Previous;
			private Node _Next;
			/// <summary>
			/// 前一个节点
			/// </summary>
			public Node Previous {
				get { 
					return _Previous;
				}
				set { 
					_Previous = value;
				}
			}
			/// <summary>
			/// 后一个节点
			/// </summary>
			public Node Next {
				get { 
					return _Next;
				}
				set { 
					_Next = value;
				}
			}

			public Node(T t) : base(t) {}

			/// <summary>
			/// 设置前置节点
			/// </summary>
			/// <param name="node">Node.</param>
			public void SetPre(Node node) {
				if (this.Previous != null) {
					this.Previous.Next = node;
				}
				if (node != null) {
					node.Previous = this.Previous;
					node.Next = this;
				}
				this.Previous = node;
			}

			/// <summary>
			/// 设置后置节点
			/// </summary>
			/// <param name="node">Node.</param>
			public void SetNext(Node node) {
				if (this.Next != null) {
					this.Next.Previous = node;
				}

				if (node != null) {
					node.Previous = this;
					node.Next = this.Next;
				}

				this.Next = node;
			}
		}

		/// <summary>
		/// 开始节点
		/// </summary>
		private Node _First;

		/// <summary>
		/// 第一个节点
		/// </summary>
		/// <value>The first.</value>
		public Node First {
			get { 
				return _First;
			}
		}

		public DoubleLink ()
		{
		}

		/// <summary>
		/// 创建一个节点
		/// </summary>
		/// <param name="t">T.</param>
		private Node Create(T t) {
			return new Node (t);
		}

		public void Insert(int index, T t) {
			Node node = _First;
			while (index > 0) {
				node = node.Next;
				index--;
			}

			Node newNode = Create (t);
			node.SetPre (newNode);
		}

		/// <summary>
		/// 添加一个节点
		/// </summary>
		/// <param name="t">T.</param>
		public void Add(T t) {
			if (_First == null) {
				_First = Create (t);
				_First.Previous = _First;
				_First.Next = _First;
				return;
			}

			Node node = _First;
			while (node.Next != _First) {
				node = node.Next;
			}

			Node newNode = Create (t);
			node.SetNext (newNode);
		}

		/// <summary>
		/// 移除一个节点
		/// </summary>
		/// <param name="t">T.</param>
		public void Remove(T t) {
			Node node = _First;
			while (node != null) {
				if (CompareTo(node.Value, t) == 0) {
					node.Next.SetPre (node.Previous);
					break;
				}
				node = node.Next;
			}
		}

		/// <summary>
		/// 查找
		/// </summary>
		/// <param name="t">T.</param>
		public Node Find(T t) {
			Node node = _First;
			while (node != null) {
				if (CompareTo(node.Value, t) == 0) {
					return node;
				}
				node = node.Next;
			}

			return null;
		}

		/// <summary>
		/// 前一个节点
		/// </summary>
		/// <param name="t">T.</param>
		public T Previous(T t) {
			Node node = _First;
			while (node != null) {
				if (CompareTo(node.Value, t) == 0) {
					return node.Previous.Value;
				}
				node = node.Next;
			}

			return default(T);
		}

		/// <summary>
		/// 后一个节点
		/// </summary>
		/// <param name="t">T.</param>
		public T Next(T t) {
			Node node = _First;
			while (node != null) {
				if (CompareTo(node.Value, t) == 0) {
					return node.Next.Value;
				}
				node = node.Next;
			}

			return default(T);
		}

		/// <summary>
		/// 节点数
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get { 
				int count = 0;
				Node node = _First;
				do {
					count++;
					node = node.Next;
				} while (node != _First);

				return count;
			}
		} 

		/// <summary>
		/// 转为数组
		/// </summary>
		/// <returns>The array.</returns>
		public T[] ToArray() {
			if (_First == null) {
				return null;
			}
			List<T> lst = new List<T> ();
			Node node = _First;
			do {
				lst.Add(node.Value);
				node = node.Next;
			} while (node != _First);

			return lst.ToArray();
		}

		/// <summary>
		/// 清空
		/// </summary>
		public void Clear() {
			_First = null;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder ();
			Node node = _First;
			do {
				sb.AppendLine (node.Value.ToString ());
				node = node.Next;
			} while (node != _First);

			return sb.ToString ();
		}
	}
}

