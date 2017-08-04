using System;

namespace Collections
{
	/// <summary>
	/// 比较
	/// </summary>
	public class Compare<T>
	{
		public delegate int NodeCompareDelegate(T n1, T n2);

		/// <summary>
		/// 比较委托
		/// </summary>
		private NodeCompareDelegate _CompareHandler;
		/// <summary>
		/// 比较委托
		/// </summary>
		/// <value>The compare handler.</value>
		public NodeCompareDelegate CompareHandler {
			get { 
				return _CompareHandler;
			}
			set { 
				_CompareHandler = value;
			}
		}

		/// <summary>
		/// 比较
		/// </summary>
		/// <returns>The to.</returns>
		/// <param name="n1">N1.</param>
		/// <param name="n2">N2.</param>
		public int CompareTo(T n1, T n2) {
			if (_CompareHandler != null) {
				return _CompareHandler (n1, n2);
			}
			return 0;
		}
	}
}

