using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 按键事件接口
/// </summary>
public interface IKeyboardEvent
{
	/// <summary>
	/// 是否可生效
	/// </summary>
	/// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
	bool EnableKeyboard(KeyCode keyCode);
	/// <summary>
	/// 按键事件
	/// </summary>
	/// <param name="touchPhase">Touch phase.</param>
	/// <param name="keyCode">Key code.</param>
	void OnKeyboard (TouchPhase touchPhase, KeyCode keyCode);
}

/// <summary>
/// 按键事件处理
/// </summary>
public class KeyboardEvent
{
	/// <summary>
	/// 滑动处理
	/// </summary>
	private List<IKeyboardEvent> _KeyboardDelegates;

	public KeyboardEvent ()
	{
		_KeyboardDelegates = new List<IKeyboardEvent> ();
	}

	/// <summary>
	/// 按键事件
	/// </summary>
	/// <param name="touchPhase">Touch phase.</param>
	/// <param name="keyCode">Key code.</param>
	public void OnKeyboard (TouchPhase touchPhase, KeyCode keyCode) {
		for (int i = 0; i < _KeyboardDelegates.Count; i++) {
			if (_KeyboardDelegates [i].EnableKeyboard (keyCode)) {
				_KeyboardDelegates [i].OnKeyboard (touchPhase, keyCode);
			}
		}
	}

	/// <summary>
	/// 注册按键处理
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void AddKeyHandler(IKeyboardEvent handler) {
		if (handler == null) {
			return;
		}

		if (!_KeyboardDelegates.Contains (handler)) {
			_KeyboardDelegates.Add (handler);
		} 
	}

	/// <summary>
	/// 移除按键处理
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void RemoveKeyHandler(IKeyboardEvent handler) {
		if (handler == null) {
			return;
		}

		if (!_KeyboardDelegates.Contains (handler)) {
			return;
		}

		_KeyboardDelegates .Remove (handler);
	}
}

