using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Keyboard : Device
{
	/// <summary>
	/// 按钮点击处理
	/// </summary>
	private KeyboardEvent _KeyboardEvent;
	/// <summary>
	/// 按钮点击处理
	/// </summary>
	public KeyboardEvent KeyboardEvent { get {  return _KeyboardEvent; } }

	public override bool IsEnable {
		get { 
			return UnityEngine.Input.anyKey || UnityEngine.Input.anyKeyDown;
		}
	}

	public Keyboard() {
		_KeyboardEvent = new KeyboardEvent ();

		this.AddComponent<KeyButton> ();

		this.GetComponent<KeyButton> ().Handler = _KeyboardEvent.OnKeyboard;
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		foreach (KeyValuePair<string, IDeviceComponent> item in _DeviceComponents) {
			if (item.Value.IsActive) {
				item.Value.Update ();
			}
		}
	}
}

