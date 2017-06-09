using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输入接口
/// </summary>
public abstract class Device
{
	/// <summary>
	/// 输入接口是否可用
	/// </summary>
	/// <returns><c>true</c> if this instance is enable; otherwise, <c>false</c>.</returns>
	public abstract bool IsEnable { get; }
	/// <summary>
	/// 更新
	/// </summary>
	public abstract void Update ();

	protected Dictionary<string, IDeviceComponent> _DeviceComponents;

	public Device() {
		_DeviceComponents = new Dictionary<string, IDeviceComponent> ();
	}

	/// <summary>
	/// 添加组件
	/// </summary>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public void AddComponent<T>() where T : IDeviceComponent, new()
	{
		string name = typeof(T).ToString ();
		if (_DeviceComponents.ContainsKey (name)) {
			return;
		}
		T t = new T ();
		_DeviceComponents.Add (name, t);
	}

	/// <summary>
	/// 获取组件
	/// </summary>
	/// <returns>The component.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T GetComponent<T>() where T : IDeviceComponent
	{
		string name = typeof(T).ToString ();
		if (_DeviceComponents.ContainsKey (name)) {
			return (T)_DeviceComponents[name];
		}

		return default(T);
	}
}

