using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRawImage : UIControl
{
	private RawImage _Image;

	// Use this for initialization
	protected override void InitControl() 
	{
		_Image = this.GetComponent<RawImage>();
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
			color.a = Alpha;
			_Image.color = color;
		}
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
	/// 图片地址
	/// </summary>
	/// <value>The URL.</value>
	public string Url {
		set { 
			if (string.IsNullOrEmpty (value)) {
				return;
			}
			Texture texture = UIBase.GetTexture (value);
			if (texture == null) {
				return;
			}
			_Image.texture = texture;
		}
	}

	/// <summary>
	/// 纹理uv
	/// </summary>
	/// <value>The U.</value>
	public Rect UV {
		get { 
			return _Image.uvRect;
		}
		set { 
			_Image.uvRect = value;
		}
	}
}

