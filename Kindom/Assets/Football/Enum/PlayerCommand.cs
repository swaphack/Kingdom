using System;

namespace Football
{
	/// <summary>
	/// 球员命令
	/// </summary>
	public enum PlayerCommand
	{
		/// <summary>
		/// 转身
		/// </summary>
		EPC_TURN,
		/// <summary>
		/// 移动
		/// </summary>
		EPC_MOVE,
		/// <summary>
		/// 冲刺
		/// </summary>
		EPC_DASH,
		/// <summary>
		/// 踢球
		/// </summary>
		EPC_KICK,
		/// <summary>
		/// 拦截
		/// </summary>
		EPC_INTERCEPT,
		/// <summary>
		/// 说话
		/// </summary>
		EPC_SAY,
	}
}

