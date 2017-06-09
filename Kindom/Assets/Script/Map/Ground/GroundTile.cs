using UnityEngine;
using System.Collections;

/// <summary>
/// 地图元素
/// </summary>
public class GroundTile : HighlightBehaviour
{
	/// <summary>
	/// 原点坐标
	/// </summary>
	private Vector3 _OriginPoint;
	/// <summary>
	/// 块大小
	/// </summary>
	private Size _TileSize;
	/// <summary>
	/// 地皮总数
	/// </summary>
	private Size _TileCount;
	/// <summary>
	/// 是否可点击
	/// </summary>
	private bool _TouchEnabled;
	/// <summary>
	/// 是否点击过
	/// </summary>
	private bool _IsTouched;

	/// <summary>
	/// 块大小
	/// </summary>
	public Size TileSize {
		get  { 
			return _TileSize;
		}
		set { 
			_TileSize = value;
			InitOriginPoint ();
		}
	}
	/// <summary>
	/// 地皮总数
	/// </summary>
	public Size TileCount {
		get {
			return _TileCount;
		}
		set { 
			_TileCount = value;
			InitOriginPoint ();
		}
	}

	/// <summary>
	/// 原点坐标
	/// </summary>
	public Vector3 OriginPoint {
		get { 
			return _OriginPoint;
		}
	}

	/// <summary>
	/// 当前坐标
	/// </summary>
	public Vector3 Position {
		get { 
			return this.transform.position;
		}
		set { 
			this.transform.position = value;
			InitOriginPoint ();
		}
	}

	/// <summary>
	/// 是否可触摸
	/// </summary>
	/// <value><c>true</c> if touch enable; otherwise, <c>false</c>.</value>
	public bool TouchEnable {
		get { 
			return _TouchEnabled;
		}
		set { 
			if (value == false) {
				TouchListener.Instance.RemoveDispatch (this.gameObject);
			} else {
				TouchListener.Instance.AddDispatch (this.gameObject, this.OnTouchEvent);
			}
			_TouchEnabled = value;

			Collider collider = GetComponent<Collider> ();
			if (collider != null) {
				collider.enabled = value;
			}
		}
	}

	/// <summary>
	/// 是否点击过
	/// </summary>
	/// <value><c>true</c> if this instance is touched; otherwise, <c>false</c>.</value>
	public bool IsTouched {
		get { 
			return _IsTouched;
		} 
		set { 
			_IsTouched = value;
		}
	}

	/// <summary>
	/// 点击事件
	/// </summary>
	protected virtual void OnTouchEvent(Vector3 hitInfo) {
		
	}

	/// <summary>
	/// 是否包含某点
	/// </summary>
	/// <returns><c>true</c>, if point was contained, <c>false</c> otherwise.</returns>
	/// <param name="pos">Position.</param>
	public bool ContainPoint(Vector3 pos) {
		if (Position == pos) {
			return true;
		}
		Vector3 originPos = OriginPoint;
		Rect rect = new Rect (originPos.x, originPos.z, TileSize.Width, TileSize.Height);
		return rect.Contains (pos);
	}

	/// <summary>
	/// 初始化原点坐标
	/// </summary>
	protected void InitOriginPoint() {
		Vector3 pos;
		pos.x = this.transform.position.x + (-0.5f * TileSize.Width * TileCount.Width);
		pos.y = this.transform.position.y;
		pos.z = this.transform.position.z + (-0.5f * TileSize.Height * TileCount.Width);
		_OriginPoint = pos;
	}

	/// <summary>
	/// 替换材质纹理
	/// </summary>
	/// <param name="texture">Texture.</param>
	public void ReplaceMatTexture(Texture2D texture) {
		if (texture == null) {
			return;
		}

		Renderer meshRender = GetComponent<Renderer> ();
		if (meshRender != null) {
			meshRender.material.mainTexture = texture;
		}
	}

	public GroundTile() {
		_TileSize = new Size(1,1);
		_TileCount = new Size(1,1);
	}

	private void OnDestroy ()
	{
		TouchEnable = false;
	}
}

