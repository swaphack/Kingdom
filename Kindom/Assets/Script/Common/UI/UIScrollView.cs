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
	
	protected override void InitControl() {
		_ScrollRect = this.GetComponent<ScrollRect> ();
		_Content = FindControlByName<RectTransform> (_ScrollRect, "Viewport.Content");
	}

	/// <summary>
	/// 设置布局方向
	/// </summary>
	/// <param name="d">D.</param>
	public void SetLayout(UILayoutDirection d) {
		if (d == UILayoutDirection.HORIZONTAL_LEFT
		    || d == UILayoutDirection.HORIZONTAL_RIGHT) {
			RemoveComponent<VerticalLayoutGroup> (_Content);
			AddComponent<HorizontalLayoutGroup> (_Content);
			_ScrollRect.vertical = false;
			_ScrollRect.horizontal = true;
		} else {
			RemoveComponent<HorizontalLayoutGroup> (_Content);
			AddComponent<VerticalLayoutGroup> (_Content);
			_ScrollRect.vertical = true;
			_ScrollRect.horizontal = false;
		}

		HorizontalOrVerticalLayoutGroup layoutGroup = _Content.GetComponent<HorizontalOrVerticalLayoutGroup> ();
		layoutGroup.childAlignment = TextAnchor.MiddleCenter;
		layoutGroup.spacing = 20;

		if (_Content == null || _Content.childCount == 0) {
			return;
		}

		RectTransform firstChild = _Content.GetComponentInChildren<RectTransform> ();
		Vector2 size = firstChild.rect.size;

		if (d == UILayoutDirection.HORIZONTAL_LEFT
			|| d == UILayoutDirection.HORIZONTAL_RIGHT) {
			//_Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal
		} else {
		}
	}

	/// <summary>
	/// 移除所有节点
	/// </summary>
	public void RemoveAll() {
		RemoveAllChildren (_Content);
	}

	/// <summary>
	/// 添加节点
	/// </summary>
	/// <param name="go">Go.</param>
	public void Add(Transform go) {
		AddChild (_Content, go);
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
	public void Remove(int index) {
		if (index < 0 || index >= _Content.childCount) {
			return;
		}

		Transform t = Find (index);
		RemoveChild (_Content, t);
	}
}

