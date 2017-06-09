using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 滑动事件接口
/// </summary>
public interface IScrollEvent
{
	/// <summary>
	/// 是否可滚动
	/// </summary>
	/// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
	bool EnableScroll { get; set; }
	/// <summary>
	/// 滑动
	/// </summary>
	/// <param name="scrollDelta">Scroll delta.</param>
	void OnScroll (Vector2 scrollDelta);
}

/// <summary>
/// 滑动事件处理
/// </summary>
public class ScrollEvent
{
	/// <summary>
	/// 滑动处理
	/// </summary>
	private List<IScrollEvent> _ScrollDelegates;

	public ScrollEvent ()
	{
		_ScrollDelegates = new List<IScrollEvent> ();
	}

	/// <summary>
	/// 滑动处理
	/// </summary>
	/// <param name="scrollDelta">Scroll delta.</param>
	public void OnScroll (Vector2 scrollDelta)
	{
		for (int i = 0; i < _ScrollDelegates.Count; i++) {
			if (_ScrollDelegates [i].EnableScroll) {
				_ScrollDelegates [i].OnScroll (scrollDelta);
			}
		}
	}

	/// <summary>
	/// 注册点击处理
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void AddScrollHandler(IScrollEvent handler) {
		if (handler == null) {
			return;
		}

		if (!_ScrollDelegates.Contains (handler)) {
			_ScrollDelegates.Add (handler);
		}
	}

	/// <summary>
	/// 移除点击处理
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void RemoveScrollHandler(IScrollEvent handler) {
		if (handler == null) {
			return;
		}

		if (_ScrollDelegates.Contains (handler)) {
			_ScrollDelegates.Remove (handler);
		}
	}
}

