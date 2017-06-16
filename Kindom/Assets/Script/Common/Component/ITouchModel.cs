using System;
using UnityEngine;

/// <summary>
/// 模型点击事件
/// </summary>
public interface ITouchModel
{
	/// <summary>
	/// 点击到地皮
	/// </summary>
	/// <param name="touchPosition">Touch position.</param>
	bool OnTouchModel (Vector3 touchPosition);
	/// <summary>
	/// 是否已点击过
	/// </summary>
	/// <value><c>true</c> if this instance is touched; otherwise, <c>false</c>.</value>
	bool IsTouched { get; set; }
	/// <summary>
	/// 是否能点击
	/// </summary>
	/// <value><c>true</c> if touch enable; otherwise, <c>false</c>.</value>
	bool EnableTouch { get; set; }
}

