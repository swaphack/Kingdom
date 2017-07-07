using System;
using System.Collections.Generic;

namespace Document
{
	/// <summary>
	/// 元素
	/// </summary>
	public abstract class Element : IElement
	{
		private string _Key;

		private List<IElement> _Children;
		private IElement _Parent;

		public override string ToString () {
			return _Key;
		}
		/// <summary>
		/// 元素名
		/// </summary>
		/// <value>The key.</value>
		public string Key { 
			get { 
				return _Key;
			} 
			set { 
				_Key = value;
			}
		}
		/// <summary>
		/// 父节点
		/// </summary>
		/// <value>The parent.</value>
		public IElement Parent  { 
			get { 
				return _Parent;
			} 
			set { 
				_Parent = value;
			}
		}
		/// <summary>
		/// 子节点
		/// </summary>
		/// <value>The children.</value>
		public List<IElement> Children { 
			get { 
				return _Children;
			} 
		}

		public Element()
		{
			_Children = new List<IElement> ();
		}

		public IElement this[int index] {
			get { 
				if (index < 0 || index >= _Children.Count) {
					return null;
				}
				return _Children [index];
			}
		}

		/// <summary>
		/// 添加子节点
		/// </summary>
		/// <param name="child">Child.</param>
		public void AddChild (IElement child)
		{
			if (child == null) {
				return;
			}

			if (!_Children.Contains (child)) {
				_Children.Add (child);
			}
		}
		/// <summary>
		/// 移除自己节点
		/// </summary>
		/// <param name="child">Child.</param>
		public void RemoveChild(IElement child) 
		{
			if (child == null) {
				return;
			}

			if (_Children.Contains (child)) {
				_Children.Remove (child);
			}
		}


		/// <summary>
		/// 获取子节点
		/// </summary>
		/// <returns>The child.</returns>
		/// <param name="name">Name.</param>
		public IElement GetChild (string name)
		{
			for (int i = 0; i < _Children.Count; i++) {
				if (_Children [i].Key == name) {
					return _Children [i];
				}
			}

			return null;
		}

		/// <summary>
		/// 移除所有子节点
		/// </summary>
		public void RemoveAllChildren()
		{
			_Children.Clear ();
		}
	}
}