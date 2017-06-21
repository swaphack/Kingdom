using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 滑动
/// </summary>
public class UISlider : UISelectable
{
	/// <summary>
	/// 背景图片
	/// </summary>
	private UIImage _Background;
	/// <summary>
	/// 填充图片
	/// </summary>
	private UIImage _Fill;
	/// <summary>
	/// 滑块
	/// </summary>
	private Slider _Slider;
	/// <summary>
	/// 滑块处理图片
	/// </summary>
	private UIImage _SlideHandle;

	// Use this for initialization
	protected override void InitControl() 
	{
		base.InitControl ();

		_Slider = this.GetComponent<Slider>();
		_Background = this.FindControlByName<UIImage> ("Background");
		_Fill = this.FindControlByName<UIImage> ("Fill Area.Fill");
		_SlideHandle = this.FindControlByName<UIImage> ("Handle Slide Area.Handle");
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
	/// 填充图片
	/// </summary>
	/// <value>The fill.</value>
	public UIImage FillUrl {
		get { 
			return _Fill;
		}
	}

	/// <summary>
	/// 滑块处理图片
	/// </summary>
	/// <value>The fill.</value>
	public UIImage SlideHandle {
		get { 
			return _SlideHandle;
		}
	}

	/// <summary>
	/// 是否显示滑块处理图片
	/// </summary>
	/// <value><c>true</c> if text visible; otherwise, <c>false</c>.</value>
	public bool SliderHandleVisible {
		get { 
			return _SlideHandle.gameObject.activeSelf;
		}
		set { 
			_SlideHandle.gameObject.SetActive (value);
		}
	}

	/// <summary>
	/// 最小值
	/// </summary>
	/// <value>The minimum value.</value>
	public float MinValue {
		get { 
			return _Slider.minValue;
		}
		set { 
			_Slider.minValue = value;
		}
	}

	/// <summary>
	/// 最大值
	/// </summary>
	/// <value>The max value.</value>
	public float MaxValue {
		get { 
			return _Slider.maxValue;
		}
		set { 
			_Slider.maxValue = value;
		}
	}

	/// <summary>
	/// 当前值
	/// </summary>
	/// <value>The value.</value>
	public float Value {
		get { 
			return _Slider.value;
		}
		set { 
			_Slider.value = value;
		}
	}

	/// <summary>
	/// 滑动方向
	/// </summary>
	/// <value>The direction.</value>
	public Slider.Direction Direction {
		get { 
			return _Slider.direction;
		}
		set { 
			_Slider.direction = value;
		}
	}

	/// <summary>
	/// 滑动事件
	/// </summary>
	/// <value>The on value changed.</value>
	public Slider.SliderEvent OnValueChanged {
		get { 
			return _Slider.onValueChanged;
		}
	}
}

