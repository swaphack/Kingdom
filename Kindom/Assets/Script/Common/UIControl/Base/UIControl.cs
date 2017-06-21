using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UIControl : MonoBehaviour
{	
	/// <summary>
	/// 控件处理事件
	/// </summary>
	public delegate void OnControlHandler(UIControl control);

	#region static method

	/// <summary>
	/// 级联查找子节点
	/// </summary>
	/// <returns>The child by cascade.</returns>
	/// <param name="component">Component.</param>
	/// <param name="name">Name.</param>
	public static Transform FindChildByCascade(Component component, string name) {
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

		return child;
	}

	/// <summary>
	/// 为控件添加组件
	/// </summary>
	/// <returns>The component.</returns>
	/// <param name="component">Component.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T AppendComponent<T>(Component component) where T : Component {
		if (component == null) {
			return null;
		}

		T t = component.GetComponent<T> ();
		if (t == null) {
			t = component.gameObject.AddComponent<T> ();
		}

		return t;
	}

	/// <summary>
	/// 为控件添加UI组件
	/// </summary>
	/// <returns>The control.</returns>
	/// <param name="component">Component.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T AppendControl<T>(Component component) where T : UIControl {
		if (component == null) {
			return null;
		}

		T t = AppendComponent<T> (component);
		if (t != null) {
			t.Init ();
		}

		return t;
	}

	/// <summary>
	/// 为子节点添加组件
	/// </summary>
	/// <returns>The child with add control.</returns>
	/// <param name="component">Component.</param>
	/// <param name="name">Name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T FindChildWithAppendComponent<T>(Component component, string name) where T : Component {
		if (component == null || string.IsNullOrEmpty(name)) {
			return null;
		}

		Transform child = FindChildByCascade (component, name);
		if (child == null) {
			return null;
		}

		return AppendComponent<T>(child);
	}

	/// <summary>
	/// 为子节点添加UI组件
	/// </summary>
	/// <returns>The child with append control.</returns>
	/// <param name="component">Component.</param>
	/// <param name="name">Name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T FindChildWithAppendControl<T>(Component component, string name) where T : UIControl {
		if (component == null || string.IsNullOrEmpty(name)) {
			return null;
		}

		Transform child = FindChildByCascade (component, name);
		if (child == null) {
			return null;
		}

		return AppendControl<T>(child);
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

	protected bool IsDirty {
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
	public T FindChildByName<T>(string name) where T : Component {
		return FindChildWithAppendComponent<T> (this, name);
	}


	/// <summary>
	/// 根据名称路径查找控件
	/// name: Scroll View.xx.xx
	/// </summary>
	/// <returns>The control by name.</returns>
	/// <param name="name">Name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T FindControlByName<T>(string name) where T : UIControl {
		return FindChildWithAppendControl<T> (this, name);
	}

	/// <summary>
	/// 获取盒子组件
	/// </summary>
	/// <returns>The box.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T GetBox<T>() where T : UIBox {
		return AppendControl<T> (this);
	}

	/// <summary>
	/// 移除所有子节点
	/// </summary>
	public void RemoveAllChildren() {
		RemoveAllChildren (this);
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
		this.InitControl ();
	}

	void Update() {
		if (IsDirty) {
			this.Flush ();
			IsDirty = false;
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

