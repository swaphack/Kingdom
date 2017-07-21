using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common.Utility;

public class TouchListener : SingletonBehaviour<TouchListener>, ITouchEvent
{
	/// <summary>
	/// 点击回调
	/// </summary>
	public delegate void OnTouchHandler(Vector3 hitPos);
	/// <summary>
	/// 派发器
	/// </summary>
	private Dictionary<GameObject, OnTouchHandler> _Dispatchers;

	private TouchListener()
	{
		_Dispatchers = new Dictionary<GameObject, OnTouchHandler> ();
	}

	/// <summary>
	/// 是否必须需要对象
	/// </summary>
	/// <value><c>true</c> if need touch target; otherwise, <c>false</c>.</value>
	public bool NeedTarget { 
		get {
			return true;
		} 
	}

	/// <summary>
	/// 是否点击到目标
	/// </summary>
	/// <returns><c>true</c> if this instance is touch the specified go; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	public bool IsTouch (GameObject go)
	{
		if (go == null) {
			return false;
		}

		if (_Dispatchers.ContainsKey (go)) {
			return true;
		}

		return false;
	}
	/// <summary>
	/// 点击
	/// </summary>
	/// <param name="touchPhase">Touch phase.</param>
	/// <param name="touchScreenPoint">Touch screen point.</param>
	/// <param name="hitInfo">Hit info.</param>
	public void OnClick (TouchPhase touchPhase, Vector3 touchPoint, RaycastHit hitInfo)
	{
		if (touchPhase == TouchPhase.Began) {
			OnTouchEvent (hitInfo.collider.gameObject, hitInfo.point);
		}
	}

	/// <summary>
	/// 处理碰撞
	/// </summary>
	private void OnTouchEvent(GameObject go, Vector3 hitPos) {
		if (go == null) {
			return;
		}
		if (_Dispatchers.ContainsKey (go)) {
			_Dispatchers [go] (hitPos);
		}
	}

	public void AddDispatch(GameObject go, OnTouchHandler handler)
	{
		if (go == null || handler == null) {
			return;
		}
		if (_Dispatchers.ContainsKey (go)) {
			_Dispatchers [go] = handler;
		} else {
			_Dispatchers.Add (go, handler);
		}
	}

	public void RemoveDispatch(GameObject go)
	{
		if (go == null) {
			return;
		}
		if (_Dispatchers.ContainsKey (go)) {
			_Dispatchers.Remove (go);
		}
	}
}
