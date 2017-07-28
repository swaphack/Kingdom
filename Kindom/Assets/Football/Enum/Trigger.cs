using System;

namespace Football
{
	/// <summary>
	/// 队伍状态触发条件
	/// </summary>
	public enum TeamStateTrigger
	{
		/// <summary>
		/// 球在对方脚下
		/// </summary>
		ETST_BALL_IN_OTHER_TEAM = 0,
		/// <summary>
		/// 球在自己队员脚下
		/// </summary>
		ETST_BALL_IN_SELF_TEAM = 1,
		/// <summary>
		/// 球不受控制
		/// </summary>
		ETST_BALL_OUTCONTROL = 2,
	}

	/// <summary>
	/// 球员状态触发条件
	/// </summary>
	public enum PlayerStateTrigger
	{
		/// <summary>
		/// 无人控制球
		/// </summary>
		EPST_BALL_OUTOFCONTROL = 0,
		/// <summary>
		/// 球被控制
		/// </summary>
		EPST_BALL_INCONTROL = 1,

		/// <summary>
		/// 球在对方队员脚下
		/// </summary>
		EPST_BALL_INCONTROLOF_OTHERTEAM = 2,
		/// <summary>
		/// 球在自己队员脚下
		/// </summary>
		EPST_BALL_INCONTROLOF_SELFTEAM = 3,

		/// <summary>
		/// 球在自己脚下
		/// </summary>
		EPST_BALL_INCONTROLOF_SELF = 4,
		/// <summary>
		/// 球不在自己脚下
		/// </summary>
		EPST_BALL_OUTCONTROLOF_SELF = 5,

		/// <summary>
		/// 球在自己范围
		/// </summary>
		EPST_BALL_INRANGEOF_SELF = 6,
		/// <summary>
		/// 球不在自己范围
		/// </summary>
		EPST_BALL_OUTRANGEOF_SELF = 7,

		/// <summary>
		/// 身边有队员
		/// </summary>
		EPST_TEAMATE_INRANGE = 8,
		/// <summary>
		/// 身边无队员
		/// </summary>
		EPST_TEAMATE_OUTRANGE = 9,

		/// <summary>
		/// 身边有其他队伍队员
		/// </summary>
		EPST_OTHERTEAM_PLAYER_INRANGE = 10,
		/// <summary>
		/// 身边无其他队伍队员
		/// </summary>
		EPST_OTHERTEAM_PLAYER_OUTRANGE = 12,

		/// <summary>
		/// 在己方禁区
		/// </summary>
		EPST_IN_SELF_GOALAREA = 13,
		/// <summary>
		/// 在对方禁区
		/// </summary>
		EPST_IN_OTHER_GOALAREA = 14,

		/// <summary>
		/// 在己方半场
		/// </summary>
		EPST_IN_SELF_FIELD = 15,
		/// <summary>
		/// 在对方半场
		/// </summary>
		EPST_IN_OTHER_FIELD = 16,
	}

	/// <summary>
	/// 球员动作
	/// </summary>
	public enum PlayerAction
	{
		/// <summary>
		/// 等球
		/// </summary>
		EPA_WAITFOR_BALL,
		/// <summary>
		/// 等待队员
		/// </summary>
		EPA_WAITFOR_TEAMMATE,
		/// <summary>
		/// 等待其他队伍球员
		/// </summary>
		EPA_WAITFOR_OTHERTEAM_PLAYER,

		/// <summary>
		/// 跑向球
		/// </summary>
		EPA_MOVE_TO_BALL,
		/// <summary>
		/// 跑向队友
		/// </summary>
		EPA_MOVE_TO_TEAMMATE,
		/// <summary>
		/// 跑向其他队伍球员
		/// </summary>
		EPA_MOVE_TO_OTHERTEAM_PLAYER,
		/// <summary>
		/// 跑向空地
		/// </summary>
		EPA_MOVE_TO_SPACE,

		/// <summary>
		/// 传球-附近球员
		/// </summary>
		EPA_PASS_TO_NEAR_PLAYER,
		/// <summary>
		/// 传球-远处球员
		/// </summary>
		EPA_PASS_TO_FAR_PLAYER,

		/// <summary>
		/// 带球
		/// </summary>
		EPA_DRIBBLE,
		/// <summary>
		/// 带球过人
		/// </summary>
		EPA_PASS_OTHERTEAM_PLAYER,

		/// <summary>
		/// 拦截球
		/// </summary>
		EPA_INTERCEPT_BALL,
		/// <summary>
		/// 拦截队员
		/// </summary>
		EPA_INTERCEPT_TEAMMATE,
		/// <summary>
		/// 拦截其他队伍队员
		/// </summary>
		EPA_INTERCEPT_OTHERTEAM_PLAYER,

		/// <summary>
		/// 射门
		/// </summary>
		EPA_SHOOT_AT_GOAL,
	}
}

