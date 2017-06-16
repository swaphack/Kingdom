using UnityEngine;
using System.Collections;

/// <summary>
/// 相对于父节点的盒子，可根据父节点改变位置
/// </summary>
public class UIRelativeBox : UIBox
{
	/// <summary>
	/// 初始化UI框
	/// </summary>
	protected override void InitRectBox() {
		if (RectBox == null) {
			return;
		}

		SetAnchorRect (1, 1, 0, 0);
		SetOffsetRect (0, 0, 0, 0);
	}

	/// <summary>
	/// 设置在父节点的矩形框比例,值范围[0,1]
	/// </summary>
	/// <param name="top">Top.</param>
	/// <param name="right">Right.</param>
	/// <param name="bottom">Bottom.</param>
	/// <param name="left">Left.</param>
	public void SetAnchorRect(float top, float right, float bottom, float left) {
		if (RectBox == null) {
			return;
		}

		RectBox.anchorMax = new Vector2 (right, top);
		RectBox.anchorMin = new Vector2 (left, bottom);
	}

	/// <summary>
	/// 设置在父节点的矩形框偏移量
	/// </summary>
	/// <param name="top">Top.</param>
	/// <param name="right">Right.</param>
	/// <param name="bottom">Bottom.</param>
	/// <param name="left">Left.</param>
	public void SetOffsetRect(float top, float right, float bottom, float left) {
		if (RectBox == null) {
			return;
		}
		RectBox.offsetMax = new Vector2 (right, top);
		RectBox.offsetMin = new Vector2 (left, bottom);
	}

	/// <summary>
	/// 设置距离左边的位置
	/// </summary>
	/// <param name="offset">Offset.</param>
	public override void SetLeft(float offset) {
		if (RectBox == null) {
			return;
		}
		RectBox.offsetMin = new Vector2(offset, RectBox.offsetMin.y);
	}

	/// <summary>
	/// 设置距离右边的位置
	/// </summary>
	/// <param name="offset">Offset.</param>
	public override void SetRight(float offset) {
		if (RectBox == null) {
			return;
		}
		RectBox.offsetMax = new Vector2(-offset, RectBox.offsetMax.y);
	}

	/// <summary>
	/// 设置距离底部的距离
	/// </summary>
	/// <param name="offset">Offset.</param>
	public override void SetTop(float offset) {
		if (RectBox == null) {
			return;
		}
		RectBox.offsetMax = new Vector2 (RectBox.offsetMax.x, -offset);
	}

	/// <summary>
	/// 设置距离底部的距离
	/// </summary>
	/// <param name="offset">Offset.</param>
	public override void SetBottom(float offset) {
		if (RectBox == null) {
			return;
		}
		RectBox.offsetMin = new Vector2 (RectBox.offsetMin.x, offset);
	}
}

