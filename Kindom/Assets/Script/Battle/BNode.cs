using UnityEngine;
using System.Collections;

/// <summary>
/// 战斗节点
/// </summary>
public class BNode : MonoBehaviour
{
	/// <summary>
	/// 遍历
	/// </summary>
	public delegate void OnForeachHandler(int index, Component child);

	/// <summary>
	/// 添加到父节点
	/// </summary>
	/// <param name="parent">Parent.</param>
	public void AddTo(Component parent) {
		if (parent == null) {
			return;
		}
		this.transform.SetParent (parent.transform);
	}

	/// <summary>
	/// 从父节点移除
	/// </summary>
	public void RemoveFromParent()
	{
		this.transform.SetParent (null);
	}

	/// <summary>
	/// 添加节点
	/// </summary>
	/// <param name="t">T.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public void Add<T>(T t) where T : Component
	{
		if (t == null) {
			return;
		}

		t.transform.SetParent (this.transform);
	}

	/// <summary>
	/// 移除节点
	/// </summary>
	/// <param name="t">T.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public void Remove<T>(T t) where T : Component
	{
		if (t == null) {
			return;
		}

		if (t.transform.IsChildOf (this.transform)) {
			t.transform.SetParent (null);
			Destroy (t.gameObject);
		}
	}

	/// <summary>
	/// 移除所有子节点
	/// </summary>
	/// <param name="clean">If set to <c>true</c> clean.</param>
	public void RemoveAll(bool clean = false) {
		int childCount = this.transform.childCount;
		for (int i = 0; i < childCount; i++) {
			GameObject.Destroy (this.transform.GetChild (i).gameObject);
		}
	}

	/// <summary>
	/// 遍历每个子节点
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void ForeachChildren(OnForeachHandler handler)
	{
		if (handler == null) {
			return;
		}

		int childCount = this.transform.childCount;
		for (int i = 0; i < childCount; i++) {
			handler (i, this.transform.GetChild (i));
		}
	}
}

