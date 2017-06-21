using UnityEngine;
using System.Collections;

/// <summary>
/// 绝对位置盒子，父节点大小改变，位置不变
/// </summary>
public class UIAbsoluteBox : UIBox
{
	/// <summary>
	/// 初始化UI框
	/// </summary>
	protected override void InitRectBox() {
		if (RectBox == null) {
			return;
		}

		RectBox.anchorMin = Vector2.zero;
		RectBox.anchorMax = Vector2.zero;
		RectBox.offsetMax = Vector2.zero;
		RectBox.offsetMax = Vector2.zero;
		this.Position = Vector2.zero;
	}

	/// <summary>
	/// 自身的锚点
	/// </summary>
	public Vector2 Pivot {
		get { 
			return RectBox.pivot;
		}
		set { 
			RectBox.pivot = value;
		}
	}

	/// <summary>
	/// 设置大小
	/// </summary>
	public Vector2 Size {
		get { 
			return RectBox.sizeDelta;
		}
		set { 
			RectBox.sizeDelta = value;
		}
	}

	/// <summary>
	/// 设置苗店偏离位置
	/// </summary>
	public Vector2 Position {
		get { 
			return RectBox.anchoredPosition;
		}
		set { 
			RectBox.anchoredPosition = value;
		}
	}

	/// <summary>
	/// 设置距离左边的位置
	/// </summary>
	/// <param name="offset">Offset.</param>
	public override void SetLeft(float offset) {
		this.Position = new Vector2(offset, Position.y);
	}

	/// <summary>
	/// 设置距离右边的位置
	/// </summary>
	/// <param name="offset">Offset.</param>
	public override void SetRight(float offset) {
		Vector2 parentSize = RectBox.parent.GetComponent<RectTransform> ().sizeDelta;
		this.Position = new Vector2 (parentSize.x - offset, this.Position.y);
	}

	/// <summary>
	/// 设置距离底部的距离
	/// </summary>
	/// <param name="offset">Offset.</param>
	public override void SetTop(float offset) {
		Vector2 parentSize = RectBox.parent.GetComponent<RectTransform> ().sizeDelta;
		this.Position = new Vector2 (this.Position.x, parentSize.y - offset);
	}

	/// <summary>
	/// 设置距离底部的距离
	/// </summary>
	/// <param name="offset">Offset.</param>
	public override void SetBottom(float offset) {
		this.Position = new Vector2 (this.Position.x, offset);
	}
}

