using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ScrollListener : Singleton<ScrollListener>, ITouchEvent
{
	public delegate void OnScrollHandler(Vector3 direction);

	private Dictionary<GameObject, OnScrollHandler> _Dispatchers;

	/// <summary>
	/// 最近一次触摸的对象
	/// </summary>
	private GameObject _LastTouchGO;
	private Vector3 _LastTouchPoint;

	public ScrollListener()
	{
		_Dispatchers = new Dictionary<GameObject, OnScrollHandler> ();
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
			_LastTouchGO = go;	
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
			_LastTouchPoint = touchPoint;
		} else if (touchPhase == TouchPhase.Moved) {
			Vector3 currentTouchPoint = touchPoint;
			OnScrollEvent (currentTouchPoint - _LastTouchPoint);
			_LastTouchPoint = currentTouchPoint;
		} else if (touchPhase == TouchPhase.Ended) {
			_LastTouchGO = null;
		}
	}

	private void OnScrollEvent(Vector3 direction) {
		if (_LastTouchGO == null) {
			return;
		}
		if (_Dispatchers.ContainsKey (_LastTouchGO)) {
			_Dispatchers [_LastTouchGO] (direction);
		}
	}

	public void AddDispatch(GameObject collider, OnScrollHandler handler)
	{
		if (collider == null || handler == null) {
			return;
		}

		if (_Dispatchers.ContainsKey (collider)) {
			_Dispatchers [collider] = handler;
		} else {
			_Dispatchers.Add (collider, handler);
		}
	}

	public void RemoveDispatch(GameObject collider)
	{
		if (collider == null) {
			return;
		}

		if (_Dispatchers.ContainsKey (collider)) {
			_Dispatchers.Remove (collider);
		}
	}
}

