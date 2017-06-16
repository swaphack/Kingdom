using UnityEngine;
using System.Collections;

public abstract class UIBox : UIControl
{
	private RectTransform _RectBox;

	protected RectTransform RectBox {
		get { 
			return _RectBox;
		}
	}

	protected override void InitControl() {
		_RectBox = GetComponent<RectTransform> ();
		if (_RectBox == null) {
			return;
		}

		this.InitRectBox ();
	}

	protected virtual void InitRectBox() {
		
	}

	/// <summary>
	/// 设置距离左边的位置
	/// </summary>
	/// <param name="offset">Offset.</param>
	public abstract void SetLeft(float offset);

	/// <summary>
	/// 设置距离右边的位置
	/// </summary>
	/// <param name="offset">Offset.</param>
	public abstract void SetRight(float offset);

	/// <summary>
	/// 设置距离底部的距离
	/// </summary>
	/// <param name="offset">Offset.</param>
	public abstract void SetTop(float offset);
	/// <summary>
	/// 设置距离底部的距离
	/// </summary>
	/// <param name="offset">Offset.</param>
	public abstract void SetBottom(float offset);
}

