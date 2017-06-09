using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  鼠标左键
/// </summary>
public class MLeftButton : IDeviceComponent 
{
	public bool IsActive { 
		get { 
			return Input.GetMouseButton (0) || Input.GetMouseButtonUp (0) || Input.GetMouseButtonDown (0);
		}
	}

	private OnTouchDelegate _Handler;

	/// <summary>
	/// 触摸事件
	/// </summary>
	/// <value>The touch event.</value>
	public OnTouchDelegate Handler { 
		get { 
			return _Handler;
		} 
		set { 
			_Handler = value;
		} 
	}

	public void Update () {
		if (Handler == null) {
			return;
		}
		if (Input.GetMouseButtonDown (0)) {
 			Handler (TouchPhase.Began, Input.mousePosition);
		} else if (Input.GetMouseButton (0)) {
			Handler (TouchPhase.Moved, Input.mousePosition);
		} else if (Input.GetMouseButtonUp (0)) {
			Handler (TouchPhase.Ended, Input.mousePosition);
		}
	}
}
