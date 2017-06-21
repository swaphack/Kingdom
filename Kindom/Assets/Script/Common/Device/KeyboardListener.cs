using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardListener : SingletonBehaviour<KeyboardListener>, IKeyboardEvent
{
	public delegate void OnKeyDownHandler(TouchPhase touchPhase);

	private Dictionary<KeyCode, Dictionary<GameObject, OnKeyDownHandler>> _Dispatchers;

	public KeyboardListener()
	{
		_Dispatchers = new Dictionary<KeyCode, Dictionary<GameObject, OnKeyDownHandler>> ();
	}

	/// <summary>
	/// 是否可生效
	/// </summary>
	/// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
	public bool EnableKeyboard(KeyCode keyCode)
	{
		if (_Dispatchers.ContainsKey (keyCode)) {
			return true;
		}

		return false;
	}
	/// <summary>
	/// 按键事件
	/// </summary>
	/// <param name="touchPhase">Touch phase.</param>
	/// <param name="keyCode">Key code.</param>
	public void OnKeyboard (TouchPhase touchPhase, KeyCode keyCode)
	{
		if (!_Dispatchers.ContainsKey (keyCode)) {
			return;
		}

		foreach (KeyValuePair<GameObject, OnKeyDownHandler> pair in _Dispatchers[keyCode]) {
			pair.Value (touchPhase);
		}
	}

	public void AddDispatch(GameObject collider, KeyCode key, OnKeyDownHandler handler)
	{
		if (!_Dispatchers.ContainsKey (key)) {
			_Dispatchers.Add(key, new Dictionary<GameObject, OnKeyDownHandler>());
			InputManager.Instance.GetDevice<Keyboard> ().GetComponent<KeyButton> ().AddKeyCode (key);
		}

		if (_Dispatchers[key].ContainsKey (collider)) {
			_Dispatchers[key] [collider] = handler;
		} else {
			_Dispatchers[key].Add (collider, handler);
		}
	}

	public void RemoveDispatch(GameObject collider, KeyCode key)
	{
		if (collider == null) {
			return;
		}
		if (!_Dispatchers.ContainsKey (key)) {
			return;
		}

		if (_Dispatchers[key].ContainsKey (collider)) {
			_Dispatchers[key].Remove (collider);
		}

		if (_Dispatchers [key].Count == 0) {
			_Dispatchers.Remove (key);
			InputManager.Instance.GetDevice<Keyboard> ().GetComponent<KeyButton> ().RemoveKeyCode (key);
		}
	}
}

