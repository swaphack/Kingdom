using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIImage : UIControl
{
	/// <summary>
	/// 图片
	/// </summary>
	private Image _Image;
	/// <summary>
	/// 图片地址
	/// </summary>
	private string _ImageUrl;

	// Use this for initialization
	protected override void InitControl() 
	{
		_Image = this.GetComponent<Image>();
	}

	/// <summary>
	/// 颜色
	/// </summary>
	/// <value>The color.</value>
	public Color Color {
		get { 
			return _Image.color;
		}
		set { 
			_Image.color = value;
		}
	}

	/// <summary>
	/// 透明度
	/// </summary>
	/// <value>The alpha.</value>
	public float Alpha {
		get {
			return _Image.color.a;
		}
		set { 
			Color color = _Image.color;
			color.a = value;
			_Image.color = color;
		}
	}

	/// <summary>
	/// 图片地址
	/// </summary>
	/// <value>The URL.</value>
	public string Url {
		set { 
			if (string.IsNullOrEmpty (value)) {
				_Image.sprite = null;
				return;
			}
			Texture2D texture = UIBase.GetTexture2D (value);
			if (texture == null) {
				_Image.sprite = null;
				return;
			}
			Vector2 size = new Vector2 (texture.width, texture.height);
			Rect rect = new Rect (Vector2.zero, size);
			_Image.sprite = Sprite.Create (texture, rect, size);
			_ImageUrl = value;
		}
		get { 
			return _ImageUrl;
		}
	}
}

