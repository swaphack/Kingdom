using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScrollList : UIControl
{
	/// <summary>
	/// 滑动区域
	/// </summary>
	private ScrollRect _ScrollRect;
	/// <summary>
	/// 背景图片
	/// </summary>
	private UIImage _Background;
	/// <summary>
	/// 当前布局方向
	/// </summary>
	private UILayoutDirection _LayoutDirection;
	/// <summary>
	/// 内容
	/// </summary>
	private UIControl _Content;
	/// <summary>
	/// 项与项之间的间距
	/// </summary>
	private float _Spacing;
	/// <summary>
	/// 项内部的填充
	/// </summary>
	private RectOffset _Padding;
	/// <summary>
	/// 每个项的大小
	/// </summary>
	private Vector2 _ItemSize;

	/// <summary>
	/// 每个项的大小
	/// </summary>
	private Vector2 _ContentSize;

	/// <summary>
	/// 每个项的大小
	/// </summary>
	public Vector2 ItemSize {
		get { 
			return _ItemSize;
		}
		set { 
			_ItemSize = value;
		}
	}

	/// <summary>
	/// 滑动显示区域大小
	/// </summary>
	/// <value>The size of the content.</value>
	public Vector2 ContentSize {
		get { 
			return _ContentSize;
		}
		set {
			_ContentSize = value;
		}
	}

	// Use this for initialization
	protected override void InitControl() {
		_ScrollRect = this.GetComponent<ScrollRect> ();
		_Background = AppendControl<UIImage> (this);
		_Content = this.FindControlByName<UIControl> ("Viewport.Content");

		SetSpacing (0);
		SetPadding (new RectOffset (0, 0, 0, 0));
		SetLayout (UILayoutDirection.HORIZONTAL_LEFT);
	}

	/// <summary>
	/// 计算布局
	/// </summary>
	private void CalScrollListLayout() {
		
		HorizontalOrVerticalLayoutGroup layoutGroup = null;
		if (_LayoutDirection == UILayoutDirection.HORIZONTAL_LEFT
			|| _LayoutDirection == UILayoutDirection.HORIZONTAL_RIGHT) {
			RemoveComponent<VerticalLayoutGroup> (_Content);
			layoutGroup = AppendComponent<HorizontalLayoutGroup> (_Content);
			EnableVertical = false;
			EnableHorizontal = true;
			VerticalSrollBar.gameObject.SetActive (false);
			HorizontalSrollBar.gameObject.SetActive (true);

		} else {
			RemoveComponent<HorizontalLayoutGroup> (_Content);
			layoutGroup = AppendComponent<VerticalLayoutGroup> (_Content);
			EnableVertical = true;
			EnableHorizontal = false;
			VerticalSrollBar.gameObject.SetActive (true);
			HorizontalSrollBar.gameObject.SetActive (false);
		}

		if (layoutGroup == null) {
			return;
		}

		layoutGroup.childAlignment = TextAnchor.MiddleCenter;
		layoutGroup.spacing = _Spacing;
		layoutGroup.padding = _Padding;
		layoutGroup.childForceExpandHeight = false;
		layoutGroup.childForceExpandWidth = false;
		layoutGroup.childControlHeight = false;
		layoutGroup.childControlWidth = false;
	}

	/// <summary>
	/// 设置布局方向
	/// </summary>
	/// <param name="d">D.</param>
	public void SetLayout(UILayoutDirection d) {
		_LayoutDirection = d;
		IsDirty = true;
	}

	/// <summary>
	/// 设置项与项之间的间距
	/// </summary>
	/// <param name="spacing">Spacing.</param>
	public void SetSpacing(float spacing){
		_Spacing = spacing;
		IsDirty = true;
	}


	/// <summary>
	/// 设置项内部的填充大小
	/// </summary>
	/// <param name="padding">Padding.</param>
	public void SetPadding(RectOffset padding) {
		_Padding = padding;
		IsDirty = true;
	}

	/// <summary>
	/// 计算滚动栏显示区域大小
	/// </summary>
	private void CalScrollListSize()
	{
		if (_Content == null) {
			return;
		}

		RectTransform rect = _Content.GetComponent<RectTransform> ();
		Vector2 parentSize = _ScrollRect.GetComponent<RectTransform> ().sizeDelta;

		float x = 0;
		float y = 0;

		int childCount = rect.childCount;
		if (_LayoutDirection == UILayoutDirection.HORIZONTAL_LEFT
			|| _LayoutDirection == UILayoutDirection.HORIZONTAL_RIGHT) {
			for (int i = 0; i < childCount; i++) {
				RectTransform child = (RectTransform)rect.GetChild (i);
				x += child.sizeDelta.x;
			}

			x += (childCount - 1) * _Spacing;
			y = _ContentSize.y;
		} else {
			for (int i = 0; i < childCount; i++) {
				RectTransform child = (RectTransform)rect.GetChild (i);
				y += child.sizeDelta.y;
			}

			y += (childCount - 1) * _Spacing;
			x = _ContentSize.x;
		}

		UIAbsoluteBox box = _Content.GetBox<UIAbsoluteBox> ();

		if (_LayoutDirection == UILayoutDirection.HORIZONTAL_LEFT) {
			box.Pivot = new Vector2 (0, 0);
			box.Position = new Vector2(0, 0);
		} else if (_LayoutDirection == UILayoutDirection.HORIZONTAL_RIGHT) {
			box.Pivot = new Vector2 (1, 0);
			box.Position = new Vector2(parentSize.x - x, 0);
		} else if (_LayoutDirection == UILayoutDirection.VERTICAL_TOP) {
			box.Pivot = new Vector2 (0, 1);
			box.Position = new Vector2(0, parentSize.y - y);
		} else if (_LayoutDirection == UILayoutDirection.VERTICAL_BOTTOM) {
			box.Pivot = new Vector2 (0, 0);
			box.Position = new Vector2(0, 0);
		}

		box.Size = new Vector2 (x, y);
	}


	/// <summary>
	/// 背景图片
	/// </summary>
	/// <value>The background.</value>
	public UIImage Background {
		get { 
			return _Background;
		}
	}
		
	/// <summary>
	/// 是否允许横向滑动
	/// </summary>
	/// <value><c>true</c> if enable horizontal; otherwise, <c>false</c>.</value>
	public bool EnableHorizontal {
		get { 
			return _ScrollRect.horizontal;
		}
		set { 
			_ScrollRect.horizontal = value;
		}
	}

	/// <summary>
	/// 是否允许纵向滑动
	/// </summary>
	/// <value><c>true</c> if enable vertical; otherwise, <c>false</c>.</value>
	public bool EnableVertical {
		get { 
			return _ScrollRect.vertical;
		}
		set { 
			_ScrollRect.vertical = value;
		}
	}

	/// <summary>
	/// 运动类型
	/// </summary>
	/// <value>The type of the movement.</value>
	public ScrollRect.MovementType MovementType {
		get { 
			return _ScrollRect.movementType;
		}
		set { 
			_ScrollRect.movementType = value;
		}
	}

	/// <summary>
	/// 只有在movemonetType为Elastic有意义 , 这是ScrollRect越过边界后弹回速度的量
	/// </summary>
	/// <value>The elasticity.</value>
	public float Elasticity {
		get { 
			return _ScrollRect.elasticity;
		}
		set { 
			_ScrollRect.elasticity = value;
		}
	}

	/// <summary>
	/// 滑动结束时是否拥有惯性移动,为ture时会以DecelerationRate的值作为惯性的量
	/// </summary>
	/// <value><c>true</c> if inertia; otherwise, <c>false</c>.</value>
	public bool Inertia {
		get { 
			return _ScrollRect.inertia;
		}
		set { 
			_ScrollRect.inertia = value;
		}
	}

	/// <summary>
	/// DecelerationRate的正常值为0 – 1 , 该值大于等于1时则永远不会减速,除非到达边界
	/// </summary>
	/// <value>The deceleration rate.</value>
	public float DecelerationRate {
		get { 
			return _ScrollRect.decelerationRate;
		}
		set { 
			_ScrollRect.decelerationRate = value;
		}
	}

	/// <summary>
	/// 对于鼠标滚动轮或触控板的敏感度,该值越大,对鼠标滑轮的滚动反应越大,可以自行测试,对手指滑动和鼠标拖动影响不大
	/// </summary>
	/// <value>The scroll sensitivity.</value>
	public float ScrollSensitivity {
		get { 
			return _ScrollRect.scrollSensitivity;
		}
		set { 
			_ScrollRect.scrollSensitivity = value;
		}
	}

	/// <summary>
	/// 可见区域
	/// </summary>
	/// <value>The viewport.</value>
	public RectTransform Viewport {
		get { 
			return _ScrollRect.viewport;
		}
		set { 
			_ScrollRect.viewport = value;
		}
	}

	/// <summary>
	/// 水平滚动栏
	/// </summary>
	/// <value>The vertical sroll bar.</value>
	public Scrollbar HorizontalSrollBar {
		get { 
			return _ScrollRect.horizontalScrollbar;
		}
		set { 
			_ScrollRect.horizontalScrollbar = value;
		}
	}

	/// <summary>
	/// 水平可见方式
	/// </summary>
	/// <value>The vertical visibility.</value>
	public ScrollRect.ScrollbarVisibility HorizontalVisibility {
		get { 
			return _ScrollRect.horizontalScrollbarVisibility;
		}
		set {
			_ScrollRect.horizontalScrollbarVisibility = value;
		}
	}

	/// <summary>
	/// 水平间距
	/// </summary>
	/// <value>The vertical spacing.</value>
	public float HorizontalSpacing {
		get { 
			return _ScrollRect.horizontalScrollbarSpacing;
		}
		set {
			_ScrollRect.horizontalScrollbarSpacing = value;
		}
	}

	/// <summary>
	/// 垂直滚动栏
	/// </summary>
	/// <value>The vertical sroll bar.</value>
	public Scrollbar VerticalSrollBar {
		get { 
			return _ScrollRect.verticalScrollbar;
		}
		set {
			_ScrollRect.verticalScrollbar = value;
		}
	}

	/// <summary>
	/// 垂直可见方式
	/// </summary>
	/// <value>The vertical visibility.</value>
	public ScrollRect.ScrollbarVisibility VerticalVisibility {
		get { 
			return _ScrollRect.verticalScrollbarVisibility;
		}
		set {
			_ScrollRect.verticalScrollbarVisibility = value;
		}
	}

	/// <summary>
	/// 垂直间距
	/// </summary>
	/// <value>The vertical spacing.</value>
	public float VerticalSpacing {
		get { 
			return _ScrollRect.verticalScrollbarSpacing;
		}
		set {
			_ScrollRect.verticalScrollbarSpacing = value;
		}
	}

	/// <summary>
	///  当ScrollRect的值被改变时的回调 , 参数的Vector2是以当前ScrollRect内容的位置在父容器的百分位置.Vector2.x对应的是在横向的比例 , Vector2.y对应的在纵向的比例
	/// </summary>
	/// <value>The on value changed.</value>
	public ScrollRect.ScrollRectEvent OnValueChanged {
		get { 
			return _ScrollRect.onValueChanged;
		}
	}

	protected override void Flush ()
	{
		this.CalScrollListLayout ();
		this.CalScrollListSize ();
	}

	/// <summary>
	/// 移除所有节点
	/// </summary>
	public void RemoveAll() {
		RemoveAllChildren (_Content);
		IsDirty = true;
	}

	/// <summary>
	/// 添加节点
	/// </summary>
	/// <param name="go">Go.</param>
	public void AddItem(Component go) {
		if (go == null) {
			return;
		}
		if (go.GetComponent<RectTransform> () != null) {
			go.GetComponent<RectTransform> ().sizeDelta = _ItemSize;
		}

		AddItemWithDefaultSize (go);
	}

	/// <summary>
	/// 添加节点,原始大小
	/// </summary>
	/// <param name="go">Go.</param>
	public void AddItemWithDefaultSize(Component go) {
		if (go == null) {
			return;
		}
		AddChild (_Content, go);
		IsDirty = true;
	}

	/// <summary>
	/// 移除节点
	/// </summary>
	/// <param name="go">Go.</param>
	public void RemoveItem(Component go) {
		if (go == null) {
			return;
		}
		RemoveChild (_Content, go);
		IsDirty = true;
	}

	/// <summary>
	/// 获取子节点个数
	/// </summary>
	/// <value>The count.</value>
	public int Count {
		get { 
			return _Content.GetComponent<Transform>().childCount;
		}
	}

	/// <summary>
	/// 获取子节点
	/// </summary>
	/// <param name="index">Index.</param>
	public Component FindItem(int index) {
		int count = Count;
		if (index < 0 || index >= count) {
			return null;
		}

		return (Component)_Content.GetComponent<Transform>().GetChild (index);
	}

	/// <summary>
	/// 移除子节点
	/// </summary>
	/// <param name="index">Index.</param>
	public void RemoveAt(int index) {
		int count = Count;
		if (index < 0 || index >= count) {
			return;
		}

		Component t = FindItem (index);
		if (t == null) {
			return;
		}
		RemoveChild (_Content, t);
		IsDirty = true;
	}

	/// <summary>
	/// 第一项
	/// </summary>
	/// <value>The first item.</value>
	public Component FirstItem {
		get { 
			return FindItem (0);
		}
	}

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
}

