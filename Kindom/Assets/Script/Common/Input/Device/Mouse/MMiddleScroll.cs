using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMiddleScroll : IDeviceComponent {
	
	public bool IsActive { 
		get { 
			return true;
		}
	}

	private OnScrollHandler _Handler;
	/// <summary>
	/// 滑动事件
	/// </summary>
	/// <value>The touch event.</value>
	public OnScrollHandler Handler { 
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

		Handler (Input.mouseScrollDelta);
	}
}
