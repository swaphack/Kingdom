using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIText : UIControl
{
	protected Text Text;

	// Use this for initialization
	void Awake ()
	{
		Text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Start ()
	{
	}
}

