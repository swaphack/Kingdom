using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLayer : UILayer {

	// Use this for initialization
	void Start () {
		UIScrollView scrollView = FindControlByName<UIScrollView> ("Scroll View");
		scrollView.SetLayout (UILayoutDirection.HORIZONTAL_LEFT);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
