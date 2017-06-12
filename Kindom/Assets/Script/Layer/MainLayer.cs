using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLayer : UILayer {

	// Use this for initialization
	void Start () {
		UIScrollView scrollView = FindControlByName<UIScrollView> ("Scroll View");
		scrollView.SetLayout (UILayoutDirection.VERTICAL_BOTTOM);
		scrollView.SetItemSize (new Vector2 (100, 100));
		scrollView.SetSpacing (10);
		scrollView.RemoveAt (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
