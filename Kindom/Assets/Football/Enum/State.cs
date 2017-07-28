using System;

namespace Football
{
	/// <summary>
	/// 队伍状态
	/// </summary>
	public enum TeamState 
	{
		/// <summary>
		/// 攻击
		/// </summary>
		ETS_ATTCK = 0,
		/// <summary>
		/// 平衡
		/// </summary>
		EST_BLANECE = 1,
		/// <summary>
		/// 防守
		/// </summary>
		EST_DEFEND = 2,
	}

	/// <summary>
	/// 球员状态
	/// </summary>
	public enum PlayerState
	{
		/// <summary>
		/// 等待
		/// </summary>
		EPS_WAIT = 0,
		/// <summary>
		/// 奔跑
		/// </summary>
		EPS_MOVE = 1,
		/// <summary>
		/// 踢球
		/// </summary>
		EPS_KICK = 2,
	}

	/// <summary>
	/// 球状态
	/// </summary>
	public enum BallState
	{
		/// <summary>
		/// 球在球员控制下
		/// </summary>
		EST_IN_CONTROL = 0,
		/// <summary>
		/// 球不在球员控制下
		/// </summary>
		EST_OUT_OF_CONTROL = 1,
	}
}

