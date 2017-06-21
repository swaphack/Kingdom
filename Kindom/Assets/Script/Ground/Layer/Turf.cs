using UnityEngine;
using System.Collections;

/// <summary>
/// 草皮
/// </summary>
public class Turf : GroundTile
{	
	/// <summary>
	/// 点击到当前块
	/// </summary>
	/// <param name="touchPosition">Touch position.</param>
	public override bool OnTouchModel (Vector3 touchPosition) {
		if (!IsTouched) {
			PlayHighlight ();
		} else {
			CancelHighlight ();
		}
		IsTouched = !IsTouched;

		return true;
	}
}

