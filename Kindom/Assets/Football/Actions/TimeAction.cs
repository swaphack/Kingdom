using System;

namespace Football.Actions
{
	/// <summary>
	/// 含有事件的动作
	/// </summary>
	public class TimeAction : Action
	{
		/// <summary>
		/// 总时长
		/// </summary>
		private float _TotalTime;
		/// <summary>
		/// 时间
		/// </summary>
		protected float _Time;

		/// <summary>
		/// 总时间
		/// </summary>
		/// <value>The time.</value>
		public float TotalTime {
			get { 
				return _TotalTime;
			}
			protected set { 
				_TotalTime = value;
			}
		}

		public TimeAction ()
		{
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public override void Init() {
			_Time = 0;
		}
	}
}

