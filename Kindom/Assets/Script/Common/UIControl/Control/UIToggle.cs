using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIToggle : UISelectable
{
	private Toggle _Toggle;

	/// <summary>
	/// 背景图片
	/// </summary>
	private UIImage _Background;

	/// <summary>
	/// 选中时图片
	/// </summary>
	private UIImage _Checkmark;
	/// <summary>
	/// 显示文本
	/// </summary>
	private UIText _Label;
	
	// Use this for initialization
	protected override void InitControl()
	{
		base.InitControl ();
		_Toggle = this.GetComponent<Toggle> ();
		_Background = this.FindControlByName<UIImage> ("Background");
		_Checkmark = this.FindControlByName<UIImage> ("Background.Checkmark");
		_Label = this.FindControlByName<UIText> ("Label");
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
	/// 显示文本
	/// </summary>
	/// <value>The checkmark.</value>
	public UIImage CheckmarkUrl {
		get { 
			return _Checkmark;
		}
	}

	/// <summary>
	/// 文本内容
	/// </summary>
	/// <value>The label.</value>
	public UIText Label {
		get { 
			return _Label;
		}
	}

	/// <summary>
	/// 是否选中
	/// </summary>
	/// <value><c>true</c> if this instance is on; otherwise, <c>false</c>.</value>
	public bool IsOn {
		get {
			return _Toggle.isOn;
		}
		set { 
			_Toggle.isOn = value;
		}
	}

	/// <summary>
	/// 所属分组
	/// </summary>
	/// <value>The group.</value>
	public ToggleGroup Group {
		get { 
			return _Toggle.group;
		}
		set { 
			_Toggle.group = value;
		}
	}

	public Toggle.ToggleTransition ToggleTransition {
		get { 
			return _Toggle.toggleTransition;
		}
		set { 
			_Toggle.toggleTransition = value;
		}
	}

	public Toggle.ToggleEvent OnValueChanged {
		get { 
			return _Toggle.onValueChanged;
		}
	}
}

