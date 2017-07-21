using System;
using System.Collections.Generic;

namespace Common.Document
{
	/// <summary>
	/// 元素
	/// </summary>
	public interface IElement
	{
		/// <summary>
		/// 元素名
		/// </summary>
		/// <value>The key.</value>
		string Key { get; set; }
		/// <summary>
		/// 父节点
		/// </summary>
		/// <value>The parent.</value>
		IElement Parent { get; set; } 
		/// <summary>
		/// 子节点
		/// </summary>
		/// <value>The children.</value>
		List<IElement> Children { get; }
		/// <summary>
		/// 获取自己子节点
		/// </summary>
		/// <param name="index">Index.</param>
		IElement this[int index] { get; }
		/// <summary>
		/// 添加子节点
		/// </summary>
		/// <param name="child">Child.</param>
		void AddChild (IElement child);
		/// <summary>
		/// 移除自己节点
		/// </summary>
		/// <param name="child">Child.</param>
		void RemoveChild(IElement child);
		/// <summary>
		/// 获取子节点
		/// </summary>
		/// <returns>The child.</returns>
		/// <param name="name">Name.</param>
		IElement GetChild (string name);
		/// <summary>
		/// 移除所有子节点
		/// </summary>
		void RemoveAllChildren();
	}
}

