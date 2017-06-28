using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// 属性面板编辑器
/// </summary>
public class BaseEditor : Editor
{
	/// <summary>
	/// 获取第一个选中对象
	/// </summary>
	/// <returns>The first select child.</returns>
	public static Transform GetFirstSelectChild()
	{
		Transform[] transforms = Selection.transforms;
		if (transforms == null || transforms.Length != 1) {
			return null;
		}

		return transforms [0];
	}

	/// <summary>
	/// 添加组件
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T AppendComponent<T>(Transform transform) where T : Component
	{
		if (transform == null) {
			return null;
		}

		T t = transform.GetComponent<T> ();
		if (t == null) {
			t = transform.gameObject.AddComponent<T> ();
		}

		return t;
	}

	/// <summary>
	/// 应用更改
	/// </summary>
	public void Flush() {
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}

