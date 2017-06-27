using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 按钮
/// </summary>
public class KeyButton : IDeviceComponent
{
	public bool IsActive { 
		get { 
			return true;
		}
	}

	/// <summary>
	/// 键盘事件
	/// </summary>
	/// <value>The keyboard delegate.</value>
	private OnKeyboardHandler _Handler;
	/// <summary>
	/// 键盘事件
	/// </summary>
	/// <value>The keyboard delegate.</value>
	public OnKeyboardHandler Handler {
		get { 
			return _Handler;
		}
		set { 
			_Handler = value;
		}
	}

	private List<KeyCode> _KeyCodes;

	public KeyButton()
	{
		_KeyCodes = new List<KeyCode> ();
	}

	public void Update ()
	{
		if (Handler == null) {
			return;
		}
		for (int i = 0; i < _KeyCodes.Count; i++) {
			if (Input.GetKeyDown (_KeyCodes[i])) {
				Handler (TouchPhase.Began, _KeyCodes[i]);
			} else if (Input.GetKey (_KeyCodes[i])) {
				Handler (TouchPhase.Moved, _KeyCodes[i]);
			} else if (Input.GetKeyUp (_KeyCodes[i])) {
				Handler (TouchPhase.Ended, _KeyCodes[i]);
			}
		}

	}

	public void AddKeyCode(KeyCode keyCode)
	{
		if (_KeyCodes.Contains (keyCode)) {
			return;
		}

		_KeyCodes.Add (keyCode);
	}

	public void RemoveKeyCode(KeyCode keyCode)
	{
		if (!_KeyCodes.Contains (keyCode)) {
			return;
		}

		_KeyCodes.Remove (keyCode);
	}
}

