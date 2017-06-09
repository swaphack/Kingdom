using UnityEngine;
using System.Collections;

public class DeviceRoot : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
	{
		AddGameObject<TouchListener>();
		AddGameObject<ScrollListener>();
		AddGameObject<KeyboardListener>();

		InputManager.Instance.GetDevice<Mouse> ().LeftTouchEvent.AddTouchHandler (this.GetComponentInChildren<TouchListener>());
		InputManager.Instance.GetDevice<Mouse> ().RightTouchEvent.AddTouchHandler (this.GetComponentInChildren<ScrollListener>());
		InputManager.Instance.GetDevice<Keyboard> ().KeyboardEvent.AddKeyHandler (this.GetComponentInChildren<KeyboardListener>());
	}

	void AddGameObject<T>() where T : Component
	{
		GameObject go = new GameObject ();
		go.AddComponent<T> ();
		go.name = typeof(T).ToString();
		go.transform.SetParent (this.transform);
	}
}

