using System;

namespace Collections
{
	/// <summary>
	/// 平衡二叉树
	/// </summary>
	public class BalanceBinaryTree<T> : Compare<T>
	{
		/// <summary>
		/// 节点委托
		/// </summary>
		public delegate void NodeDelegate(Node node);

		/// <summary>
		/// 节点
		/// </summary>
		public class Node : CNode<T>
		{
			/// <summary>
			/// 左节点
			/// </summary>
			public Node Left;
			/// <summary>
			/// 右节点
			/// </summary>
			public Node Right;

			public Node(T t) : base(t) {}

			/// <summary>
			/// 是否有子节点
			/// </summary>
			/// <returns><c>true</c> if this instance has child; otherwise, <c>false</c>.</returns>
			public bool HasChild() {
				return Left != null || Right != null;
			}

			/// <summary>
			/// 处理
			/// </summary>
			/// <param name="handler">Handler.</param>
			public void Handle(NodeDelegate handler) {
				if (handler == null) {
					return;
				}

				handler (this);

				if (this.Left != null) {
					this.Left.Handle (handler);
				}
				if (this.Right != null) {
					this.Right.Handle (handler);
				}
			}
		}

		/// <summary>
		/// 根节点
		/// </summary>
		private Node _Root;
		/// <summary>
		/// 节点比较
		/// </summary>
		private NodeCompareDelegate _CompareHandler;

		/// <summary>
		/// 第一个节点
		/// </summary>
		/// <value>The first.</value>
		public Node Root {
			get { 
				return _Root;
			}
		}


		public BalanceBinaryTree ()
		{
		}

		/// <summary>
		/// 创建一个节点
		/// </summary>
		/// <param name="t">T.</param>
		private Node Create(T t) {
			return new Node (t);
		}

		/// <summary>
		/// 添加一个节点
		/// </summary>
		/// <param name="t">T.</param>
		public void Add(T t) {
			if (_Root == null) {
				_Root = Create (t);	
				return;
			} 

			Node node = _Root;
			while (true) {
				int result = CompareTo (node.Value, t);
				if (result == 1) { // 走右
					if (node.Right == null) {
						node.Right = Create (t);
						break;
					}
					node = node.Right;
				} else { // 走左
					if (node.Left == null) {
						node.Left = Create (t);
						break;
					}
					node = node.Left;
				}
			}
		}

		/// <summary>
		/// 移除一个节点
		/// </summary>
		/// <param name="t">T.</param>
		public void Remove(T t) {
			Node node = _Root;
			Node last = node;
			while (node != null) {
				int result = CompareTo (node.Value, t);
				if (result == 1) { // 走右
					last = node;
					node = node.Right;
				} else if (result == -1) { // 走左
					last = node;
					node = node.Left;
				} else {
					if (last.Left == node) { // 左节点
						last.Left = node.Left;
						if (node.Left != null) {
							node.Left.Right = node.Right;
						}
					} else if (last.Right == node) { // 右节点
						last.Right = node.Left;
						if (node.Left != null) {
							node.Left.Right = node.Right;
						}
					} else { // 根节点
						last = node.Left;
						if (last != null) {
							last.Right = node.Right;
						}
					}
					break;
				}
			}
		}

		/// <summary>
		/// 遍历节点
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void Foreach(NodeDelegate handler) {
			if (handler == null || _Root == null) {
				return;				
			}

			_Root.Handle(handler);
		}

		public void Clear() {
			_Root = null;
		}
	}
}

