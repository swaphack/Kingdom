using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRightButton : IDeviceComponent {

	public bool IsActive { 
		get { 
			return Input.GetMouseButton (1) || Input.GetMouseButtonUp (1) || Input.GetMouseButtonDown (1);
		}
	}

	private OnTouchHandler _Handler;

	/// <summary>
	/// 触摸事件
	/// </summary>
	/// <value>The touch event.</value>
	public OnTouchHandler Handler { 
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
		if (Input.GetMouseButtonDown (1)) {
			Handler (TouchPhase.Began, Input.mousePosition);
		} else if (Input.GetMouseButton (1)) {
			Handler (TouchPhase.Moved, Input.mousePosition);
		} else if (Input.GetMouseButtonUp (1)) {
			Handler (TouchPhase.Ended, Input.mousePosition);
		}
	}
}
