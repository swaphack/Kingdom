using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CameraBehaviour : MonoBehaviour, ITouchEvent, IScrollEvent
{
	private Vector3 _InitTouchPoint = Vector3.zero;
	private Vector3 _lastTouchPoint = Vector3.zero;
	private bool _EnableScroll = true;
	private bool _EnableRotate = true;

	/// <summary>
	/// 是否点击到目标
	/// </summary>
	/// <returns><c>true</c> if this instance is touch the specified go; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	public bool IsTouch (GameObject go) {
		if (!_EnableRotate) {
			return false;
		}
		return go != null;
	}
	/// <summary>
	/// 点击
	/// </summary>
	/// <param name="touchPhase">Touch phase.</param>
	/// <param name="touchScreenPoint">Touch screen point.</param>
	/// <param name="hitInfo">Hit info.</param>
	public void OnClick (TouchPhase touchPhase, Vector3 touchScreenPoint, RaycastHit hitInfo)
	{
		if (touchPhase == TouchPhase.Began) {
			_InitTouchPoint = hitInfo.point;
			_lastTouchPoint = Input.mousePosition;
		} else if (touchPhase == TouchPhase.Moved) {
			Vector3 diff = Input.mousePosition - _lastTouchPoint;
			OnRotateView (diff);
			_lastTouchPoint = Input.mousePosition;
		} else if (touchPhase == TouchPhase.Ended) {
			_lastTouchPoint = Vector3.zero;
			_InitTouchPoint = Vector3.zero;
		}
	}

	/// <summary>
	/// 是否可滚动
	/// </summary>
	/// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
	public bool EnableRotate { 
		get { 
			return _EnableRotate;
		}
		set {
			_EnableRotate = value;
		}
	}

	/// <summary>
	/// 是否可缩放
	/// </summary>
	/// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
	public bool EnableScroll { 
		get { 
			return _EnableScroll;
		}
		set {
			_EnableScroll = value;
		}
	}
	/// <summary>
	/// 滑动
	/// </summary>
	/// <param name="scrollDelta">Scroll delta.</param>
	public void OnScroll (Vector2 scrollDelta) {
		OnMoveView (scrollDelta.y);
	}

	// Use this for initialization
	void Start ()
	{
		InputManager.Instance.GetDevice<Mouse> ().RightTouchEvent.AddTouchHandler (this);
		InputManager.Instance.GetDevice<Mouse> ().MiddleScrollEvent.AddScrollHandler (this);
	}

	/// <summary>
	/// 滑动视角
	/// </summary>
	void OnRotateView(Vector3 direction) {
		Camera camera = this.GetComponent<Camera> ();
		camera.transform.RotateAround (_InitTouchPoint, Vector3.up, direction.x);
	}

	/// <summary>
	/// 滚动视角
	/// </summary>
	/// <param name="scrollRate">Scroll rate.</param>
	void OnScrollView(float scrollRate) {
		Camera camera = this.GetComponent<Camera> ();
		float value = camera.fieldOfView;
		value -= scrollRate;
		if (value < 1)
			value = 1;
		if (value > 179)
			value = 179;
		camera.fieldOfView = value;
	}

	void OnMoveView (float scrollRate) {
		Vector3 offset = this.transform.forward * scrollRate;
		this.transform.localPosition += offset ;
	}
}

