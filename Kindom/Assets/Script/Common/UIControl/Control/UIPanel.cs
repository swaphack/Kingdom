using UnityEngine;
using System.Collections;

public class UIPanel : UIControl
{
	/// <summary>
	/// 背景图片
	/// </summary>
	private UIImage _Background;

	// Use this for initialization
	protected override void InitControl()
	{
		_Background = AppendControl<UIImage> (this);
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
}

