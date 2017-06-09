using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 鼠标输入
/// </summary>
public class Mouse : Device
{
	/// <summary>
	/// 鼠标左键点击处理
	/// </summary>
	private TouchEvent _LeftTouchEvent;
	/// <summary>
	/// 鼠标右键点击处理
	/// </summary>
	private TouchEvent _RightTouchEvent;
	/// <summary>
	/// 鼠标滚轮滑动处理
	/// </summary>
	private ScrollEvent _MiddleScrollEvent;

	/// <summary>
	/// 鼠标左键点击处理
	/// </summary>
	public TouchEvent LeftTouchEvent { get {  return _LeftTouchEvent; } }
	/// <summary>
	/// 鼠标右键点击处理
	/// </summary>
	public TouchEvent RightTouchEvent { get {  return _RightTouchEvent; } }
	/// <summary>
	/// 鼠标滚轮滑动处理
	/// </summary>
	public ScrollEvent MiddleScrollEvent { get {  return _MiddleScrollEvent; } }
	
	public override bool IsEnable {
		get { 
			return UnityEngine.Input.mousePresent;
		}
	}

	public Mouse() 
	{
		_LeftTouchEvent = new TouchEvent ();
		_RightTouchEvent = new TouchEvent ();
		_MiddleScrollEvent = new ScrollEvent ();
		
		this.AddComponent<MLeftButton> ();
		this.AddComponent<MRightButton> ();
		this.AddComponent<MMiddleScroll> ();

		this.GetComponent<MLeftButton> ().Handler = _LeftTouchEvent.OnClick;
		this.GetComponent<MRightButton> ().Handler = _RightTouchEvent.OnClick;
		this.GetComponent<MMiddleScroll> ().Handler = _MiddleScrollEvent.OnScroll;
	}

	public override void Update ()
	{
		foreach (KeyValuePair<string, IDeviceComponent> item in _DeviceComponents) {
			if (item.Value.IsActive) {
				item.Value.Update ();
			}
		}
	}
}

