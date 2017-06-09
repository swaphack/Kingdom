using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 点击事件接口
/// </summary>
public interface ITouchEvent
{
	/// <summary>
	/// 是否点击到目标
	/// </summary>
	/// <returns><c>true</c> if this instance is touch the specified go; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	bool IsTouch (GameObject go);
	/// <summary>
	/// 点击
	/// </summary>
	/// <param name="touchPhase">Touch phase.</param>
	/// <param name="touchScreenPoint">Touch screen point.</param>
	/// <param name="hitInfo">Hit info.</param>
	void OnClick (TouchPhase touchPhase, Vector3 touchScreenPoint, RaycastHit hitInfo);
}

/// <summary>
/// 点击事件处理
/// </summary>
public class TouchEvent
{		
	/// <summary>
	/// 触摸处理
	/// </summary>
	private List<ITouchEvent> _TouchDelegates;
	/// <summary>
	/// 最近一次触摸的对象
	/// </summary>
	private RaycastHit _LastRaycastHit;

	public TouchEvent ()
	{
		_TouchDelegates = new List<ITouchEvent> ();
	}

	/// <summary>
	/// 点击处理
	/// </summary>
	/// <param name="touchPhase">Touch phase.</param>
	/// <param name="touchPoint">Touch point.</param>
	public void OnClick (TouchPhase touchPhase, Vector3 touchPoint)
	{
		if (touchPhase == TouchPhase.Began) {
			Ray ray = Camera.main.ScreenPointToRay (touchPoint);
			if (!Physics.Raycast (ray, out _LastRaycastHit)) {
				return;
			}
			Dispatch (touchPhase, touchPoint, _LastRaycastHit);
		} else if (touchPhase == TouchPhase.Moved) {
			if (_LastRaycastHit.collider == null) {
				return;
			}
			Dispatch (touchPhase, touchPoint, _LastRaycastHit);
		} else if (touchPhase == TouchPhase.Ended) {
			Dispatch (touchPhase, touchPoint, _LastRaycastHit);
		}
	}

	/// <summary>
	/// 注册点击处理
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void AddTouchHandler(ITouchEvent handler) {
		if (handler == null) {
			return;
		}

		if (!_TouchDelegates.Contains (handler)) {
			_TouchDelegates.Add (handler);
		}
	}

	/// <summary>
	/// 移除点击处理
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void RemoveTouchHandler(ITouchEvent handler) {
		if (handler == null) {
			return;
		}

		if (_TouchDelegates.Contains (handler)) {
			_TouchDelegates.Remove (handler);
		}
	}

	/// <summary>
	/// 派发事件
	/// </summary>
	/// <param name="touchPhase">Touch phase.</param>
	/// <param name="touchPoint">Touch point.</param>
	/// <param name="hitInfo">Hit info.</param>
	private void Dispatch(TouchPhase touchPhase, Vector3 touchPoint, RaycastHit hitInfo) {
		if (_LastRaycastHit.collider == null) {
			return;
		}

		for (int i = 0; i < _TouchDelegates.Count; i++) {
			if (_TouchDelegates [i].IsTouch (_LastRaycastHit.collider.gameObject)) {
				_TouchDelegates [i].OnClick (touchPhase, touchPoint, hitInfo);
			}
		}
	}
}

