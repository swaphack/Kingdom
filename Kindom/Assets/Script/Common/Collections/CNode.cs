using System;

namespace Collections
{
	/// <summary>
	/// 顶点节点
	/// </summary>
	public class CNode<T>
	{
		/// <summary>
		/// 当前值
		/// </summary>
		private T _Value;
		/// <summary>
		/// 当前值
		/// </summary>
		/// <value>The value.</value>
		public T Value { 
			get {
				return _Value; 
			} 
		}

		public CNode(T t) {
			_Value = t;
		}
	}
}

