using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class InputManager : Singleton<InputManager>
{
	private List<Device> _Devices;

	public InputManager() {
		_Devices = new List<Device> ();

		AddDevice<Mouse>();
		AddDevice<Keyboard>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// 点击到UI界面
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}

		for (int i = 0; i < _Devices.Count; i++) {
			if (_Devices [i].IsEnable) {
				_Devices [i].Update ();
			}
		}
	}

	/// <summary>
	/// 添加设备
	/// </summary>
	/// <returns>The device.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T AddDevice<T>() where T : Device, new()
	{
		T t = new T ();
		_Devices.Add (t);
		return t;
	}

	/// <summary>
	/// 获取设备
	/// </summary>
	/// <returns>The device.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T GetDevice<T>() where T : Device
	{
		for (int i = 0; i < _Devices.Count; i++) {
			if (_Devices [i] is T) {
				return _Devices [i] as T;
			}
		}

		return null;
	}
}

