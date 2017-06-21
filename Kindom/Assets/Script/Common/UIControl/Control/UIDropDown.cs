using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 含有下拉框的控件
/// </summary>
public class UIDropdown : UISelectable
{
	private Dropdown _DropDown;
	/// <summary>
	/// 背景图片
	/// </summary>
	private UIImage _Background;
	/// <summary>
	/// 显示文本
	/// </summary>
	private UIText _Label;
	/// <summary>
	/// 箭头
	/// </summary>
	private UIImage _Arrow;
	/// <summary>
	/// 模板
	/// </summary>
	private UIScrollList _Template;

	// Use this for initialization
	protected override void InitControl()
	{
		base.InitControl ();
		_DropDown = this.GetComponent<Dropdown> ();
		_Background = AppendControl<UIImage> (this);
		_Label = this.FindControlByName<UIText> ("Label");
		_Arrow = this.FindControlByName<UIImage> ("Arrow");
		_Template = this.FindControlByName<UIScrollList> ("Template");
	}

	/// <summary>
	/// 背景
	/// </summary>
	/// <value>The background.</value>
	public UIImage Background {
		get { 
			return _Background;
		}
	}

	/// <summary>
	/// 文本
	/// </summary>
	/// <value>The label.</value>
	public UIText Label {
		get { 
			return _Label;
		}
	}

	/// <summary>
	/// 箭头图片
	/// </summary>
	/// <value>The arrow.</value>
	public UIImage Arrow {
		get { 
			return _Arrow;
		}
	}

	/// <summary>
	/// 模板项
	/// </summary>
	/// <value>The template item.</value>
	public Component TemplateItem {
		get { 
			return _Template.FirstItem;
		}
		set { 
			_Template.RemoveAt (0);
			_Template.AddItem (value);
		}
	}

	/// <summary>
	/// 头个文本
	/// </summary>
	/// <value>The caption text.</value>
	public Text CaptionText {
		get { 
			return _DropDown.captionText;
		}
		set { 
			_DropDown.captionText = value;
		}
	}

	public Image ItemImage {
		get { 
			return _DropDown.itemImage;
		}
		set { 
			_DropDown.itemImage = value;
		}
	}

	public Text ItemText {
		get { 
			return _DropDown.itemText;
		}
		set { 
			_DropDown.itemText = value;
		}
	}

	/// <summary>
	/// 头个图片
	/// </summary>
	/// <value>The caption image.</value>
	public Image CaptionImage {
		get { 
			return _DropDown.captionImage;
		}
		set { 
			_DropDown.captionImage = value;
		}
	}

	/// <summary>
	/// 添加项
	/// </summary>
	/// <param name="text">Text.</param>
	public void AddOption(string text)
	{
		Dropdown.OptionData data = new Dropdown.OptionData ();
		data.text = text;
		data.image = null;
		_DropDown.options.Add (data);
	}

	/// <summary>
	/// 添加项
	/// </summary>
	/// <param name="text">Text.</param>
	public void RemoveOption(string text)
	{
		for (int i = 0; i < _DropDown.options.Count; i++) {
			if (_DropDown.options [i].text == text) {
				_DropDown.options.RemoveAt (i);
				return;
			}
		}
	}

	/// <summary>
	/// 添加项
	/// </summary>
	/// <param name="text">Text.</param>
	public void RemoveAllOptions()
	{
		_DropDown.ClearOptions();
	}

	/// <summary>
	/// 设置选中的选项
	/// </summary>
	/// <param name="index">Index.</param>
	public void EnableOption(int index)
	{
		if (index < 0 || index >= _DropDown.options.Count) {
			return;
		}

		_DropDown.value = index;
		if (_DropDown.captionText != null) {
			_DropDown.captionText.text = _DropDown.options [index].text;
		}
		if (_DropDown.captionImage != null) {
			_DropDown.captionImage.sprite = _DropDown.options [index].image;
		}
	}

	/// <summary>
	/// 当Dropdown的值被改变时的回调
	/// </summary>
	/// <value>The on value changed.</value>
	public Dropdown.DropdownEvent OnValueChanged {
		get { 
			return _DropDown.onValueChanged;
		}
	}
}

