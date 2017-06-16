using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBase
{
	/// <summary>
	/// 创建纹理
	/// </summary>
	/// <returns>The texture.</returns>
	/// <param name="url">URL.</param>
	public static Texture GetTexture(string url) {
		return ResourceManger.Instance.Get<Texture> (url);
	}

	/// <summary>
	/// 创建用于2d显示的纹理
	/// </summary>
	/// <returns>The texture2 d.</returns>
	/// <param name="url">URL.</param>
	public static Texture2D GetTexture2D(string url) {
		return ResourceManger.Instance.Get<Texture2D> (url);
	}

	/// <summary>
	/// 创建字体
	/// </summary>
	/// <returns>The font.</returns>
	/// <param name="name">Name.</param>
	/// <param name="size">Size.</param>
	public static Font GetFont(string name, int size = 16) {
		Font font = Font.CreateDynamicFontFromOSFont (name, size);
		return font;
	}
}

