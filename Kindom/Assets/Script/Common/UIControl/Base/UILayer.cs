using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UILayer : UIControl
{
	public T CreateUI<T>() where T : UIBehaviour
	{
		string name = typeof(T).ToString ();
		string[] names = name.Split ('.');
		name = names [names.Length - 1];

		GameObject go = new GameObject ();
		go.name = name;
		return go.AddComponent<T> ();
	}

	/// <summary>
	/// 创建文本
	/// </summary>
	/// <returns>The text.</returns>
	/// <param name="text">Text.</param>
	/// <param name="fontSize">Font size.</param>
	public UIText CreateText(string text, int fontSize) 
	{
		Text go = CreateUI<Text> ();
		UIText ui = AppendControl<UIText> (go);
		ui.FontName = "Arial";
		ui.Alignment = TextAnchor.MiddleCenter;
		ui.ResizeTextForBestFit = true;
		ui.ResizeTextMinSize = fontSize;
		ui.ResizeTextMaxSize = 40;
		ui.Text = text;
		ui.FontSize = fontSize;
		return ui;
	}

	/// <summary>
	/// 创建图片
	/// </summary>
	/// <returns>The image.</returns>
	/// <param name="url">URL.</param>
	public UIImage CreateImage(string url) 
	{
		Image go = CreateUI<Image> ();
		UIImage ui = AppendControl<UIImage> (go);
		ui.Url = url;
		return ui;
	}

	/// <summary>
	/// 创建按钮
	/// </summary>
	/// <returns>The button.</returns>
	/// <param name="bgurl">Bgurl.</param>
	/// <param name="text">Text.</param>
	/// <param name="handler">Handler.</param>
	public UIButton CreateButton(string bgurl, string text, OnControlHandler handler) {
		Button go = CreateUI<Button> ();
		AppendComponent<Image> (go);
		AddChild (go, CreateText (text, 16));

		UIButton ui = AppendControl<UIButton> (go);
		ui.Background.Url = bgurl;
		ui.Label.Text = text;
		if (handler != null) {
			ui.OnClick.AddListener (()=>{
				handler(ui);
			});
		}
		return ui;
	}

	/// <summary>
	/// 创建输入文本
	/// </summary>
	/// <returns>The input field.</returns>
	/// <param name="imgurl">Imgurl.</param>
	/// <param name="tip">Tip.</param>
	public UIInputField CreateInputField(string imgurl, string tip)
	{
		InputField go = CreateUI<InputField> ();
		AppendComponent<Image> (go);

		UIText placeholder = CreateText (tip, 16);
		placeholder.name = "Placeholder";
		AddChild (go, placeholder);

		UIText text = CreateText ("", 16);
		text.SupportRichText = false;
		AddChild (go, text);

		UIInputField ui = AppendControl<UIInputField> (go);
		ui.TargetGraphic = go.image;
		ui.Background.Url = imgurl;
		ui.Placeholder = placeholder.GetComponent<Text>();
		ui.LabelComponent = text.GetComponent<Text>();
		ui.InputType = InputField.InputType.Standard;

		return ui;
	}
}

