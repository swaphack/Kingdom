using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// 摄像机行为
/// </summary>
public class CameraBehaviour : MonoBehaviour, ITouchEvent, IScrollEvent
{
	/// <summary>
	/// 触摸响应类型
	/// </summary>
	public enum TouchRespondType
	{
		/// <summary>
		/// 移动
		/// </summary>
		Transfer,
		/// <summary>
		/// 旋转
		/// </summary>
		Rotation,
	}

	/// <summary>
	/// 旋转类型
	/// </summary>
	public enum RotationType
	{
		/// <summary>
		/// 绕自身旋转
		/// </summary>
		Self,
		/// <summary>
		/// 绕世界旋转
		/// </summary>
		World,
	}
	
	private Vector3 _InitTouchPoint = Vector3.zero;
	private Vector3 _lastTouchPoint = Vector3.zero;
	private bool _EnableScroll = true;
	private bool _EnableRotate = true;

	/// <summary>
	/// 触摸响应类型
	/// </summary>
	public TouchRespondType RespondType;

	/// <summary>
	/// 旋转类型
	/// </summary>
	public RotationType RotateType;

	/// <summary>
	/// 水平旋转是否固定
	/// </summary>
	public bool FixedHorizontalRotation;
	/// <summary>
	/// 垂直旋转是否固定
	/// </summary>
	public bool FixedVerticalRotation;

	/// <summary>
	/// 滚动缩放比例
	/// </summary>
	public float ScrollRate = 10;
	/// <summary>
	/// 移动缩放比例
	/// </summary>
	public float TransferRate = 3.5f;

	/// <summary>
	/// 是否必须需要对象
	/// </summary>
	/// <value><c>true</c> if need touch target; otherwise, <c>false</c>.</value>
	public bool NeedTarget { 
		get {
			return false;
		} 
	}

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
			OnResponseTouch (diff);
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

	void OnDestory() {
		InputManager.Instance.GetDevice<Mouse> ().RightTouchEvent.RemoveTouchHandler (this);
		InputManager.Instance.GetDevice<Mouse> ().MiddleScrollEvent.RemoveScrollHandler (this);
	}

	/// <summary>
	/// 响应触摸
	/// </summary>
	/// <param name="direction">Direction.</param>
	void OnResponseTouch(Vector3 direction) {
		if (RespondType == TouchRespondType.Rotation) {
			OnRotateView (direction);
		} else {
			Camera camera = this.GetComponent<Camera> ();
			if (direction.x != 0) {
				camera.transform.Translate (this.transform.right * TransferRate * (-direction.x / Mathf.Abs(direction.x)));
			}

			if (direction.y != 0) {
				camera.transform.Translate (this.transform.forward * TransferRate * (direction.y / Mathf.Abs(direction.y)));
			}
		}
	}

	/// <summary>
	/// 滑动视角
	/// </summary>
	void OnRotateView(Vector3 direction) {
		Camera camera = this.GetComponent<Camera> ();
		Vector3 up = Vector3.up;
		Vector3 right = Vector3.right;

		if (RotateType == RotationType.Self) {
			up = this.transform.up;
			right = this.transform.right;
		}

		if (!FixedHorizontalRotation) {
			camera.transform.RotateAround (_InitTouchPoint, up, direction.x);
		}
		if (!FixedVerticalRotation) {
			camera.transform.RotateAround (_InitTouchPoint, right, -direction.y);
		}
	}

	void OnMoveView (float scrollRate) {
		Vector3 offset = this.transform.forward * scrollRate * ScrollRate;
		this.transform.localPosition += offset ;
	}
}

