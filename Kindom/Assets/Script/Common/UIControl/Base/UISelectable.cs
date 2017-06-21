using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISelectable : UIControl
{
	private Selectable _Selectable;

	// Use this for initialization
	protected override void InitControl()
	{
		_Selectable = this.GetComponent<Selectable> ();
	}

	/// <summary>
	/// 目标图像
	/// </summary>
	/// <value>The target graphic.</value>
	public Graphic TargetGraphic {
		get { 
			return _Selectable.targetGraphic;
		}
		set { 
			_Selectable.targetGraphic = value;
		}
	}

	/// <summary>
	/// 不同状态的表现方式
	/// </summary>
	/// <value>The transition.</value>
	public Selectable.Transition Transition {
		get { 
			return _Selectable.transition;
		}
		set { 
			_Selectable.transition = value;
		}
	}

	/// <summary>
	/// 用动作区别按钮不同状态
	/// </summary>
	/// <value>The animation triggers.</value>
	public AnimationTriggers AnimationTriggers {
		get { 
			return _Selectable.animationTriggers;
		}
		set { 
			_Selectable.animationTriggers = value;
		}
	}

	/// <summary>
	/// 用图片区别按钮不同状态
	/// </summary>
	/// <value>The state of the sprite.</value>
	public SpriteState SpriteState {
		get { 
			return _Selectable.spriteState;
		}
		set { 
			_Selectable.spriteState = value;
		}
	}

	/// <summary>
	/// 用颜色区别按钮不同状态
	/// </summary>
	/// <value>The color block.</value>
	public ColorBlock ColorBlock {
		get { 
			return _Selectable.colors;
		}
		set { 
			_Selectable.colors = value;
		}
	}
}

