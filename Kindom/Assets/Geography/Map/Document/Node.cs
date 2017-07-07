using System;
using System.Text;
using Document;
using System.Collections.Generic;

namespace Geography.Map.Document
{
	/// <summary>
	/// 节点
	/// </summary>
	public class Node : Element
	{
		/// <summary>
		/// 单条数据
		/// </summary>
		private string _Value;
		/// <summary>
		/// 多条数据
		/// </summary>
		private string[] _ValueAry;

		/// <summary>
		/// 单条数据
		/// </summary>
		/// <value>The value.</value>
		public string Value {
			get { 
				return _Value;
			}
		}

		/// <summary>
		/// 多条数据
		/// </summary>
		/// <value>The value ary.</value>
		public string[] ValueAry { 
			get { 
				return _ValueAry;
			}
		}

		public Node() 
		{
		}

		public void SetValue(string value) {
			_Value = value;
		}

		public void SetValue(string[] value)
		{
			_ValueAry = value;
		}

		/// <summary>
		/// 获取子节点
		/// </summary>
		/// <returns>The child.</returns>
		/// <param name="name">Name.</param>
		public T GetChild<T> (string name) where T : IElement
		{
			IElement e = GetChild (name);
			if (e == null) {
				return default(T);
			}

			return (T) e;
		}

		/// <summary>
		/// 转为字符串
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Geography.Map.Document.Node"/>.</returns>
		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (Key).Append ("=");
			if (!string.IsNullOrEmpty (_Value)) {
				sb.Append (_Value);
			} else if (_ValueAry != null) {
				sb.Append ("{").Append (string.Join (" ", _ValueAry)).Append (" }");
			}

			if (Children != null) {
				for (int i = 0; i < Children.Count; i++) {
					sb.Append (" ");
					sb.Append (Children [i].ToString ());
				}
			}

			sb.Append (" ");

			return sb.ToString();
		}
	}
}

