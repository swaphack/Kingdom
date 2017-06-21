using UnityEngine;
using System.Collections;

/// <summary>
/// 方块
/// </summary>
public class Cube : ModelBehaviour
{
	/// <summary>
	/// 点击面回调
	/// </summary>
	public delegate void OnTouchSideHandler(Cube target, Vector3 position);

	/// <summary>
	/// 纹理文件路径
	/// </summary>
	private string _TextureFilePath;
	/// <summary>
	/// 碰撞体
	/// </summary>
	private Collider _Collider;
	/// <summary>
	/// 点击面回调
	/// </summary>
	private OnTouchSideHandler _OnTouchSideHandler;

	/// <summary>
	/// 纹理文件路径
	/// </summary>
	public string TextureFilepath;

	/// <summary>
	/// 体积大小
	/// </summary>
	/// <value>The size.</value>
	public Vector3 Size {
		get { 
			if (_Collider == null) {
				return Vector3.one;
			}
			return _Collider.bounds.size;
		}
	}

	/// <summary>
	/// 点击面回调
	/// </summary>
	public OnTouchSideHandler OnTouchHandler {
		set { 
			_OnTouchSideHandler = value;
		}
	}

	/// <summary>
	/// 位置
	/// </summary>
	/// <value>The position.</value>
	public Vector3 Position {
		get { 
			return this.transform.position;
		}
	}

	/// <summary>
	/// 初始化
	/// </summary>
	public override void Init() {
		base.Init ();

		_Collider = this.GetComponent<Collider> ();
	}
	
	/// <summary>
	/// 点击事件
	/// </summary>
	public override bool OnTouchModel(Vector3 hitInfo) 
	{ 
		Vector3 position = this.Position;
		Vector3 size = this.Size;

		Vector3 sidePos = position;


		if (hitInfo.y == position.y + size.y * 0.5f) {// 上方
			sidePos.y += size.y;
		} else if (hitInfo.y == position.y - size.y * 0.5f) { // 下方
			sidePos.y -= size.y;
		} else if (hitInfo.x == position.x + size.x * 0.5f) { // 右边
			sidePos.x += size.x;
		} else if (hitInfo.x == position.x - size.x * 0.5f) { // 左边
			sidePos.x -= size.x;
		} else if (hitInfo.z == position.z + size.z * 0.5f) { // 前面
			sidePos.z += size.z;
		} else { // 背面
			sidePos.z -= size.z;
		}

		if (_OnTouchSideHandler != null) {
			_OnTouchSideHandler (this, sidePos);
		}
		
		return true; 
	}
}

