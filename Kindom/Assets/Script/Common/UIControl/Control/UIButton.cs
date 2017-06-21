using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButton : UIControl
{
	/// <summary>
	/// 按钮
	/// </summary>
	private Button _Button;
	/// <summary>
	/// 背景图片
	/// </summary>
	private UIImage _Background;
	/// <summary>
	/// 文本
	/// </summary>
	private UIText _Label;

	// Use this for initialization
	protected override void InitControl() 
	{
		_Button = this.GetComponent<Button>();
		_Background = AppendControl<UIImage> (this);
		_Label = this.FindControlByName<UIText> ("Text");
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
	/// 是否显示文本
	/// </summary>
	/// <value><c>true</c> if text visible; otherwise, <c>false</c>.</value>
	public bool TextVisible {
		get { 
			return _Label.gameObject.activeSelf;
		}
		set { 
			_Label.gameObject.SetActive (value);
		}
	}

	/// <summary>
	/// 按钮事件
	/// </summary>
	/// <value>The click.</value>
	public Button.ButtonClickedEvent OnClick {
		get { 
			return _Button.onClick;
		}
	}
}

