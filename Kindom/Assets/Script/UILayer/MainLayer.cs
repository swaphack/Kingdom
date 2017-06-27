using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Geography.Ground.Sample;

public class MainLayer : UILayer {

	/// <summary>
	/// 图片集合
	/// </summary>
	private Dictionary<string, string> _Images;
	/// <summary>
	/// 地图创造者
	/// </summary>
	public Builder Builder;

	private UIImage _Image;
	private UIButton _LastTouchUI;

	public MainLayer()
	{
		_Images = new Dictionary<string, string> ();
		_Images.Add ("Textures/UI/Cliff (Grassy)", "Textures/Ground/Cliff (Grassy)");
		_Images.Add ("Textures/UI/Cliff (Layered Rock)", "Textures/Ground/Cliff (Layered Rock)");
		_Images.Add ("Textures/UI/Cliff (Sandstone)", "Textures/Ground/Cliff (Sandstone)");
		_Images.Add ("Textures/UI/Forest Floor", "Textures/Ground/Forest Floor");
		_Images.Add ("Textures/UI/GoodDirt", "Textures/Ground/GoodDirt");
		_Images.Add ("Textures/UI/Grass (Hill)", "Textures/Ground/Grass (Hill)");
	}

	private void SetImageUrl(string url) {
		if (Builder != null && Builder.Ground != null) {
			TurfLayer layer = Builder.Ground.GetLayer<TurfLayer> ();
			if (layer != null) {
				layer.ReplaceTextureUrl = url;
			}
		}
		_Image.Url = url;
	}

	private void SetEnableLayer(int index) {
		if (Builder != null && Builder.Ground != null) {
			Builder.Ground.SetEnableTouchLayer (index);
		}
	}

	void Start () {
		UIScrollList scrollList = FindControlByName<UIScrollList> ("Scroll View");
		scrollList.SetLayout (UIScrollList.UILayoutDirection.HORIZONTAL_LEFT);
		scrollList.ItemSize = new Vector2 (120, 120);
		scrollList.ContentSize = new Vector2 (180, 180);
		scrollList.SetPadding(new RectOffset(20,20,0,0));
		scrollList.SetSpacing (30);

		foreach(KeyValuePair<string, string> item in _Images) {
			scrollList.AddItem (CreateButton (item.Key, item.Key, (UIControl ui) => { 
				UIButton btn = (UIButton) ui;
				if (_LastTouchUI != null) {
					_LastTouchUI.Label.Color = Color.white;
					_LastTouchUI.Background.Alpha = 1;
				}

				btn.Label.Color = Color.red;
				btn.Background.Alpha = 0.5f;
				SetImageUrl(item.Value);
				_LastTouchUI = btn;
			}));
		}

		_Image = FindControlByName<UIImage> ("Image");

		KeyboardListener.Instance.AddDispatch (this.gameObject, KeyCode.Delete, (TouchPhase phase) => {
			if (phase != TouchPhase.Began) {
				return;
			}
			if (_LastTouchUI != null) {
				_LastTouchUI.Label.Color = Color.white;
				_LastTouchUI.Background.Alpha = 1;
			}


			SetImageUrl("");

			_LastTouchUI = null;
		});

		SetImageUrl("");

		UIDropdown dropDown = FindControlByName<UIDropdown> ("Dropdown");
		dropDown.RemoveAllOptions ();
		dropDown.AddOption ("地块层");
		dropDown.AddOption ("建筑层");
		dropDown.AddOption ("角色层");

		dropDown.OnValueChanged.AddListener((int arg0) => {
			SetEnableLayer(arg0);
		});
	}

	void OnDestory() {
		KeyboardListener.Instance.RemoveDispatch (this.gameObject, KeyCode.Delete);
	}
}
