using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScrollbar : UISelectable
{
	/// <summary>
	/// 背景图片
	/// </summary>
	private UIImage _Background;
	/// <summary>
	/// 滑动栏
	/// </summary>
	private Scrollbar _Scrollbar;
	/// <summary>
	/// 滑动处理图片
	/// </summary>
	private UIImage _SlideHandle;

	// Use this for initialization
	protected override void InitControl() 
	{	
		base.InitControl ();
		_Scrollbar = this.GetComponent<Scrollbar> ();
		_Background = AppendControl<UIImage> (this);
		_SlideHandle = this.FindControlByName<UIImage> ("Sliding Area.Handle");
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
	/// 滑块处理图片
	/// </summary>
	/// <value>The slide handle.</value>
	public UIImage SlideHandle {
		get { 
			return _SlideHandle;
		}
	}

	/// <summary>
	/// 滑动方向
	/// </summary>
	/// <value>The direction.</value>
	public Scrollbar.Direction Direction {
		get { 
			return _Scrollbar.direction;
		}
		set { 
			_Scrollbar.direction = value;
		}
	}

	/// <summary>
	/// 当前值
	/// </summary>
	/// <value>The value.</value>
	public float Value {
		get { 
			return _Scrollbar.value;
		}
		set { 
			_Scrollbar.value = value;
		}
	}

	/// <summary>
	/// 滑动图片大小
	/// </summary>
	/// <value>The size.</value>
	public float Size {
		get { 
			return _Scrollbar.size;
		}
		set { 
			_Scrollbar.size = value;
		}
	}

	/// <summary>
	/// 步骤
	/// </summary>
	/// <value>The number of steps.</value>
	public int NumberOfSteps {
		get { 
			return _Scrollbar.numberOfSteps;
		}
		set { 
			_Scrollbar.numberOfSteps = value;
		}
	}

	/// <summary>
	/// 滑动事件
	/// </summary>
	/// <value>The on value changed.</value>
	public Scrollbar.ScrollEvent OnValueChanged {
		get { 
			return _Scrollbar.onValueChanged;
		}
	}
}

