using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInputField : UISelectable
{
	/// <summary>
	/// 背景图片
	/// </summary>
	private UIImage _Background;
	/// <summary>
	/// 输入区域
	/// </summary>
	private InputField _InputField;

	// Use this for initialization
	protected override void InitControl()
	{
		base.InitControl ();
		_InputField = this.GetComponent<InputField> ();
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

	/// <summary>
	/// 提示文本
	/// </summary>
	/// <value>The Placeholder.</value>
	public UIText TipLabel {
		get { 
			return AppendControl<UIText>(Placeholder);
		}
	}

	/// <summary>
	/// 文本组件
	/// </summary>
	/// <value>The label.</value>
	public UIText Label {
		get { 
			return AppendControl<UIText>(LabelComponent);
		}
	}

	/// <summary>
	/// 输入文本
	/// </summary>
	/// <value>The input text.</value>
	public string InputText {
		get { 
			return _InputField.text;
		}
		set { 
			_InputField.text = value;
		}
	}

	/// <summary>
	/// 输入单词限制
	/// </summary>
	/// <value>The character limit.</value>
	public int CharacterLimit {
		get { 
			return _InputField.characterLimit;
		}
		set { 
			_InputField.characterLimit = value;
		}
	}

	/// <summary>
	/// 显示文本组件
	/// </summary>
	/// <value>The label component.</value>
	public Text LabelComponent {
		get { 
			return _InputField.textComponent;
		}
		set { 
			_InputField.textComponent = value;
		}
	}

	/// <summary>
	/// 提示组件
	/// </summary>
	/// <value>The placeholder.</value>
	public Graphic Placeholder {
		get { 
			return _InputField.placeholder;
		}
		set { 
			_InputField.placeholder = value;
		}
	}

	/// <summary>
	/// 光标闪烁频率
	/// </summary>
	/// <value>The caret blink rate.</value>
	public float CaretBlinkRate {
		get { 
			return _InputField.caretBlinkRate;
		}
		set { 
			_InputField.caretBlinkRate = value;
		}
	}

	/// <summary>
	/// 光标宽度
	/// </summary>
	/// <value>The width of the caret.</value>
	public int CaretWidth {
		get { 
			return _InputField.caretWidth;
		}
		set { 
			_InputField.caretWidth = value;
		}
	}

	/// <summary>
	/// 光标颜色
	/// </summary>
	/// <value>The color of the caret.</value>
	public Color CaretColor {
		get { 
			return _InputField.caretColor;
		}
		set { 
			_InputField.caretColor = value;
		}
	}
	/// <summary>
	/// 是否使用自定义颜色
	/// </summary>
	/// <value><c>true</c> if custom caret color; otherwise, <c>false</c>.</value>
	public bool CustomCaretColor {
		get { 
			return _InputField.customCaretColor;
		}
		set { 
			_InputField.customCaretColor = value;
		}
	}

	/// <summary>
	/// 选中颜色
	/// </summary>
	/// <value>The color of the selection.</value>
	public Color SelectionColor {
		get { 
			return _InputField.selectionColor;
		}
		set { 
			_InputField.selectionColor = value;
		}
	}

	/// <summary>
	/// 在移动端是否隐藏输入
	/// </summary>
	/// <value><c>true</c> if hide mobile input; otherwise, <c>false</c>.</value>
	public bool HideMobileInput {
		get { 
			return _InputField.shouldHideMobileInput;
		}
		set { 
			_InputField.shouldHideMobileInput = value;
		}
	}

	/// <summary>
	/// 是否只读
	/// </summary>
	/// <value><c>true</c> if read only; otherwise, <c>false</c>.</value>
	public bool ReadOnly {
		get { 
			return _InputField.readOnly;
		}
		set { 
			_InputField.readOnly = value;
		}
	}

	/// <summary>
	/// 验证类型
	/// </summary>
	/// <value>The character validation.</value>
	public InputField.CharacterValidation CharacterValidation {
		get { 
			return _InputField.characterValidation;
		}
		set { 
			_InputField.characterValidation = value;
		}
	}

	/// <summary>
	/// 输入类型
	/// </summary>
	/// <value>The type of the input.</value>
	public InputField.InputType InputType {
		get { 
			return _InputField.inputType;
		}
		set { 
			_InputField.inputType = value;
		}
	}

	/// <summary>
	/// 文本类型
	/// </summary>
	/// <value>The type of the content.</value>
	public InputField.ContentType ContentType {
		get { 
			return _InputField.contentType;
		}
		set { 
			_InputField.contentType = value;
		}
	}

	/// <summary>
	/// 行类型
	/// </summary>
	/// <value>The type of the line.</value>
	public InputField.LineType LineType {
		get { 
			return _InputField.lineType;
		}
		set { 
			_InputField.lineType = value;
		}
	}

	/// <summary>
	/// 值改变时处理
	/// </summary>
	/// <value>The on value changed.</value>
	public InputField.OnChangeEvent OnValueChanged { 
		get { 
			return _InputField.onValueChanged;
		}
	}

	/// <summary>
	/// 输入完毕
	/// </summary>
	/// <value>The on end edit.</value>
	public InputField.SubmitEvent OnEndEdit  { 
		get { 
			return _InputField.onEndEdit;
		}
	}

}

