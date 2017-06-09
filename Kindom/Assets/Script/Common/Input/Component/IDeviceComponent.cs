using UnityEngine;
using System.Collections;

public interface IDeviceComponent
{
	/// <summary>
	/// 输入设备是否处于响应状态
	/// </summary>
	/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
	bool IsActive { get; }
	/// <summary>
	/// 更新
	/// </summary>
	void Update ();
}

