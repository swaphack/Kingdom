using System;

namespace Football
{
	/// <summary>
	/// 常量
	/// </summary>
	public class Constants
	{
		/// <summary>
		/// 动作
		/// </summary>
		public static string[] ActionName = {
			// 普通移动
			"Move",
			// 加速移动
			"Dash", 
			// 踢球
			"Kick",
			// 拦截
			"Intercept"
		};

		/// <summary>
		/// 慢速带球系数
		/// </summary>
		public const float SLOW_DRIBBLE_RATIO = 0.5f;
		/// <summary>
		/// 正常带球系数
		/// </summary>
		public const float NORMAL_DRIBBLE_RATIO = 0.8f;
		/// <summary>
		/// 快速带球系数
		/// </summary>
		public const float FAST_DRIBBLE_RATIO = 1.0f;
	}
}

