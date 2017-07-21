using System;
using UnityEngine;

namespace Common.Utility
{
	public class Tool
	{
		/// <summary>
		/// 创建一个组件
		/// </summary>
		/// <returns>The component.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T CreateComponent<T> () where T : Component
		{
			GameObject go = new GameObject ();
			return go.AddComponent<T> ();
		}


		/// <summary>
		/// 创建一个子节点
		/// </summary>
		/// <returns>The child.</returns>
		/// <param name="parent">Parent.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T CreateChild<T> (Component parent) where T : Component
		{
			if (parent == null) {
				return default(T);
			}
			GameObject go = new GameObject ();
			go.transform.SetParent (parent.transform);
			go.name = typeof(T).ToString ();
			return go.AddComponent<T> ();
		}
	}

}