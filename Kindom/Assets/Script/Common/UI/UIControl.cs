using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UIControl : MonoBehaviour
{	

	#region static method

	/// <summary>
	/// Finds the control by Name.
	/// </summary>
	/// <returns>The control by name.</returns>
	/// <param name="component">Component.</param>
	/// <param name="name">Name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T FindControlByName<T>(Component component, string name) where T : Component {
		if (component == null || string.IsNullOrEmpty(name)) {
			return null;
		}
		string[] nameNodes = name.Split ('.');
		if (nameNodes == null || nameNodes.Length == 0) {
			return null;
		}

		int i = 0;
		Transform last = component.transform;
		Transform child;
		do {
			child = last.Find(nameNodes[i]);
			if (child == null) {
				return null;
			}
			last = child;
			i++;
		} while (i < nameNodes.Length);

		if (child == null) {
			return null;
		}

		return child.GetComponent<T> ();
	}


	/// <summary>
	/// 移除子节点
	/// </summary>
	/// <param name="parent">Parent.</param>
	/// <param name="child">Child.</param>
	public static void RemoveChild(Component parent, Component child) {
		if (parent == null || child == null) {
			return;
		}
		if (child.transform.IsChildOf (parent.transform)) {
			Destroy (child.gameObject);
		}
	}

	/// <summary>
	/// 移除所有子节点
	/// </summary>
	/// <param name="parent">Parent.</param>
	public static void RemoveAllChildren(Component parent) {
		if (parent == null) {
			return;
		}
		int count = parent.transform.childCount;
		for (int i = count - 1; i >= 0; i--) {
			Transform child = parent.transform.GetChild (i);
			RemoveChild(parent, child);
		}
	}

	/// <summary>
	/// 添加子节点
	/// </summary>
	/// <param name="parent">Parent.</param>
	/// <param name="child">Child.</param>
	public static void AddChild(Component parent, Component child) {
		if (parent == null || child == null) {
			return;
		}
		child.transform.SetParent (parent.transform);
	}

	/// <summary>
	/// 添加组件
	/// </summary>
	/// <param name="componet">Componet.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T AppendComponent<T>(Component componet) where T : Component
	{
		if (componet == null) {
			return null;
		}

		T t = componet.gameObject.GetComponent<T> ();
		if (t == null) {
			t = componet.gameObject.AddComponent<T> ();
		} 

		return t;
	}

	/// <summary>
	/// 移除组件
	/// </summary>
	/// <param name="componet">Componet.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void RemoveComponent<T>(Component componet) where T : Component
	{
		if (componet == null) {
			return;
		}
		T t = componet.gameObject.GetComponent<T> ();
		if (t == null) {
			return;
		} else {
			DestroyImmediate (t);
		}
	}

	#endregion

	/// <summary>
	/// 是否是脏数据
	/// </summary>
	private bool _bDirty;

	private RectTransform _RectTransform;

	protected bool Dirty {
		get { 
			return _bDirty;
		}
		set {
			_bDirty = value;
		}
	}


	/// <summary>
	/// 根据名称路径查找控件
	/// name: Scroll View.xx.xx
	/// </summary>
	/// <returns>The control by name.</returns>
	/// <param name="name">Name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T FindControlByName<T>(string name) where T : Component {
		return FindControlByName<T> (this, name);
	}

	/// <summary>
	/// 移除子节点
	/// </summary>
	/// <param name="child">Child.</param>
	public void RemoveChild(Component child) {
		if (child == null) {
			return;
		}
		Destroy (child.gameObject);
	}

	/// <summary>
	/// 添加子节点
	/// </summary>
	/// <param name="child">Child.</param>
	public void AddChild(Component child) {
		if (child == null) {
			return;
		}
		AddChild (this, child);
	}

	public void Init() {
		this.InitRectTransform();
		this.InitControl ();
	}

	void Update() {
		if (Dirty) {
			this.Flush ();
			Dirty = false;
		}
	}

	private void InitRectTransform() {
		_RectTransform = GetComponent<RectTransform> ();
		if (_RectTransform == null) {
			return;
		}
	}

	/// <summary>
	/// 初始化控件
	/// </summary>
	protected virtual void InitControl() {
		
	}

	/// <summary>
	/// 刷新数据
	/// </summary>
	protected virtual void Flush() {
		
	}
}

