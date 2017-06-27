using UnityEngine;
using System.Collections;

/// <summary>
/// 步骤接口
/// </summary>
public interface IStep
{
	/// <summary>
	/// 是否结束
	/// </summary>
	bool Finish { get; }
	/// <summary>
	/// 执行事件
	/// </summary>
	void DoEvent();
}

