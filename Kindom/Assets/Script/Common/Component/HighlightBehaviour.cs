using UnityEngine;
using System.Collections;

/// <summary>
/// 高光组件
/// </summary>
public class HighlightBehaviour : HighlightableObject
{
	/// <summary>
	/// 外发光颜色
	/// </summary>
	public Color OuterGlowColor = Color.red;

	/// <summary>
	/// 播放高光
	/// </summary>
	public void PlayHighlight() {
		HighlightableObject ho = GetComponent<HighlightableObject> ();
		if (ho != null) {
			ho.ConstantOn (OuterGlowColor);
		}
	}

	/// <summary>
	/// 取消高光
	/// </summary>
	public void CancelHighlight() {
		HighlightableObject ho = GetComponent<HighlightableObject> ();
		if (ho != null) {
			ho.ConstantOff ();
		}
	}
}

