using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 布局方向
/// </summary>
public enum UILayoutDirection
{
	/// <summary>
	/// 从左到右
	/// </summary>
	HORIZONTAL_LEFT,
	/// <summary>
	/// 从右到左
	/// </summary>
	HORIZONTAL_RIGHT,
	/// <summary>
	/// 从上到下
	/// </summary>
	VERTICAL_TOP,
	/// <summary>
	/// 从下到上
	/// </summary>
	VERTICAL_BOTTOM,
}

public class UIScrollView : UIControl
{
	/// <summary>
	/// 显示区域
	/// </summary>
	private ScrollRect _ScrollRect;
	/// <summary>
	/// 内容
	/// </summary>
	private RectTransform _Content;
	/// <summary>
	/// 每个元素大小
	/// </summary>
	private Vector2 _ItemSize;
	/// <summary>
	/// 当前布局方向
	/// </summary>
	private UILayoutDirection _LayoutDirection;
	/// <summary>
	/// 项与项之间的间距
	/// </summary>
	private float _Spacing;
	/// <summary>
	/// 项内部的填充
	/// </summary>
	private RectOffset _Padding;
	
	protected override void InitControl() {
		_ScrollRect = this.GetComponent<ScrollRect> ();
		_Content = FindControlByName<RectTransform> (_ScrollRect, "Viewport.Content");
		_Spacing = 20;
		_Padding = new RectOffset (0,0,0,0);
		Dirty = true;
		_LayoutDirection = UILayoutDirection.HORIZONTAL_LEFT;
	}

	/// <summary>
	/// 计算滚动栏显示区域大小
	/// </summary>
	private void CalScrollViewSize()
	{
		if (_Content == null) {
			return;
		}

		int childCount = _Content.childCount;

		for (int i = 0; i < childCount; i++) {
			RectTransform child = (RectTransform)_Content.GetChild (i);
			child.sizeDelta = _ItemSize;
		}

		Vector2 size = new Vector2 ();

		if (_LayoutDirection == UILayoutDirection.HORIZONTAL_LEFT
			|| _LayoutDirection == UILayoutDirection.HORIZONTAL_RIGHT) {
			size.x = childCount * _ItemSize.x + (childCount - 1) * _Spacing;
			size.y = _ItemSize.y;
		} else {
			size.x = _ItemSize.x;
			size.y = childCount * _ItemSize.y + (childCount - 1) * _Spacing;
		}

		if (_LayoutDirection == UILayoutDirection.HORIZONTAL_LEFT) {
			_Content.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Left, 0, size.x);
		} else if (_LayoutDirection == UILayoutDirection.HORIZONTAL_RIGHT) {
			_Content.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Right, 0, size.x);
		} else if (_LayoutDirection == UILayoutDirection.VERTICAL_TOP) {
			_Content.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, 0, size.y);
		} else if (_LayoutDirection == UILayoutDirection.VERTICAL_BOTTOM) {
			_Content.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Bottom, 0, size.y);
			_Content.anchorMin = new Vector2 (0, 0);
			_Content.anchorMax = new Vector2 (1, 0);
			_Content.pivot = new Vector2 (0, 0);
			_Content.offsetMin = new Vector2 (0, 0);
		}

		_Content.sizeDelta = size;
		_Content.pivot = Vector2.zero;
	}

	/// <summary>
	/// 计算滚动栏方向
	/// </summary>
	private void CalScrollViewDirection() {
		HorizontalOrVerticalLayoutGroup layoutGroup;
		if (_LayoutDirection == UILayoutDirection.HORIZONTAL_LEFT
			|| _LayoutDirection == UILayoutDirection.HORIZONTAL_RIGHT) {
			RemoveComponent<VerticalLayoutGroup> (_Content);
			layoutGroup = AppendComponent<HorizontalLayoutGroup> (_Content);
			_ScrollRect.vertical = false;
			_ScrollRect.horizontal = true;
		} else {
			RemoveComponent<HorizontalLayoutGroup> (_Content);
			layoutGroup = AppendComponent<VerticalLayoutGroup> (_Content);
			_ScrollRect.vertical = true;
			_ScrollRect.horizontal = false;
		}

		if (layoutGroup == null) {
			return;
		}

		layoutGroup.childAlignment = TextAnchor.MiddleCenter;
		layoutGroup.spacing = _Spacing;
		layoutGroup.padding = _Padding;
		layoutGroup.childForceExpandHeight = true;
		layoutGroup.childForceExpandWidth = true;
		layoutGroup.childControlHeight = false;
		layoutGroup.childControlWidth = false;
	}

	/// <summary>
	/// 刷新数据
	/// </summary>
	protected override void Flush() {
		CalScrollViewDirection ();
		CalScrollViewSize ();
	}

	/// <summary>
	/// 设置布局方向
	/// </summary>
	/// <param name="d">D.</param>
	public void SetLayout(UILayoutDirection d) {
		_LayoutDirection = d;
		Dirty = true;
	}

	/// <summary>
	/// 设置项与项之间的间距
	/// </summary>
	/// <param name="spacing">Spacing.</param>
	public void SetSpacing(float spacing){
		_Spacing = spacing;
		Dirty = true;
	}


	/// <summary>
	/// 设置项内部的填充大小
	/// </summary>
	/// <param name="padding">Padding.</param>
	public void SetPadding(RectOffset padding) {
		_Padding = padding;
		Dirty = true;
	}

	/// <summary>
	/// 移除所有节点
	/// </summary>
	public void RemoveAll() {
		RemoveAllChildren (_Content);
		Dirty = true;
	}

	/// <summary>
	/// 添加节点
	/// </summary>
	/// <param name="go">Go.</param>
	public void Add(Transform go) {
		AddChild (_Content, go);
		Dirty = true;
	}

	/// <summary>
	/// 获取子节点个数
	/// </summary>
	/// <value>The count.</value>
	public int Count {
		get { 
			return _Content.childCount;
		}
	}

	/// <summary>
	/// 获取子节点
	/// </summary>
	/// <param name="index">Index.</param>
	public Transform Find(int index) {
		if (index < 0 || index >= _Content.childCount) {
			return null;
		}

		return _Content.GetChild (index);
	}

	/// <summary>
	/// 移除子节点
	/// </summary>
	/// <param name="index">Index.</param>
	public void RemoveAt(int index) {
		if (index < 0 || index >= _Content.childCount) {
			return;
		}

		Transform t = Find (index);
		RemoveChild (_Content, t);
		Dirty = true;
	}

	/// <summary>
	/// 设置每个元素的大小
	/// </summary>
	/// <param name="size">Size.</param>
	public void SetItemSize(Vector2 size)
	{
		_ItemSize = size;
		Dirty = true;
	}
}

