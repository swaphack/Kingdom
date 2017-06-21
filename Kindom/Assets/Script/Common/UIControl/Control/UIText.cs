using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIText : UIControl
{
	/// <summary>
	/// 文本
	/// </summary>
	private Text _Text;

	// Use this for initialization
	protected override void InitControl() 
	{
		_Text = this.GetComponent<Text>();
	}

	/// <summary>
	/// 文本内容
	/// </summary>
	/// <value>The text.</value>
	public string Text {
		get { 
			return _Text.text;
		}
		set { 
			_Text.text = value;
		}
	}

	/// <summary>
	/// 文本颜色
	/// </summary>
	/// <value>The color.</value>
	public Color Color {
		get { 
			return _Text.color;
		}
		set { 
			_Text.color = value;
		}
	}

	/// <summary>
	/// 透明度
	/// </summary>
	/// <value>The alpha.</value>
	public float Alpha {
		get {
			return _Text.color.a;
		}
		set { 
			Color color = _Text.color;
			color.a = Alpha;
			_Text.color = color;
		}
	}

	/// <summary>
	/// 字体风格
	/// </summary>
	/// <value>The font style.</value>
	public FontStyle FontStyle {
		get {
			return _Text.fontStyle;
		}
		set { 
			_Text.fontStyle = value;
		}
	}

	/// <summary>
	/// 字体大小
	/// </summary>
	/// <value>The size of the font.</value>
	public int FontSize {
		get {
			return _Text.fontSize;
		}
		set { 
			_Text.fontSize = value;
		}
	}

	/// <summary>
	/// 行与行之间的间距
	/// </summary>
	/// <value>The line spacing.</value>
	public float LineSpacing {
		get {
			return _Text.lineSpacing;
		}
		set { 
			_Text.lineSpacing = value;
		}
	}

	/// <summary>
	/// 水平环绕方式
	/// </summary>
	/// <value>The horizontal wrap mode.</value>
	public HorizontalWrapMode HorizontalWrapMode {
		get {
			return _Text.horizontalOverflow;
		}
		set { 
			_Text.horizontalOverflow = value;
		}
	}

	/// <summary>
	/// 垂直环绕方式
	/// </summary>
	/// <value>The vertical overflow.</value>
	public VerticalWrapMode verticalOverflow {
		get {
			return _Text.verticalOverflow;
		}
		set { 
			_Text.verticalOverflow = value;
		}
	}

	/// <summary>
	/// 是否支持富文本
	/// </summary>
	/// <value><c>true</c> if support rich text; otherwise, <c>false</c>.</value>
	public bool SupportRichText {
		get {
			return _Text.supportRichText;
		}
		set { 
			_Text.supportRichText = value;
		}
	}

	/// <summary>
	/// 是否根据几何框排版
	/// </summary>
	/// <value><c>true</c> if align by geometry; otherwise, <c>false</c>.</value>
	public bool AlignByGeometry {
		get {
			return _Text.alignByGeometry;
		}
		set { 
			_Text.alignByGeometry = value;
		}
	}

	/// <summary>
	/// 是否重新设置文本大小，为了更好的效果
	/// </summary>
	/// <value><c>true</c> if resize text for best fit; otherwise, <c>false</c>.</value>
	public bool ResizeTextForBestFit {
		get {
			return _Text.resizeTextForBestFit;
		}
		set { 
			_Text.resizeTextForBestFit = value;
		}
	}
		
	/// <summary>
	/// 重新设置文本大小的最大值
	/// </summary>
	/// <value>The size of the resize text max.</value>
	public int ResizeTextMaxSize {
		get {
			return _Text.resizeTextMaxSize;
		}
		set { 
			_Text.resizeTextMaxSize = value;
		}
	}

	/// <summary>
	/// 重新设置文本大小的最小值
	/// </summary>
	/// <value>The size of the resize text minimum.</value>
	public int ResizeTextMinSize {
		get {
			return _Text.resizeTextMinSize;
		}
		set { 
			_Text.resizeTextMinSize = value;
		}
	}

	/// <summary>
	/// 对齐方式
	/// </summary>
	/// <value>The alignment.</value>
	public TextAnchor Alignment {
		get  {
			return _Text.alignment;
		}
		set { 
			_Text.alignment = value;
		}
	}

	/// <summary>
	/// 字体名称
	/// </summary>
	/// <value>The name of the font.</value>
	public string FontName {
		get {
			return _Text.font.fontNames[0];
		}
		set { 
			_Text.font = UIBase.GetFont(value);
		}
	}
}

