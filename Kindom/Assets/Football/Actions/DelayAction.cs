using System;

namespace Football.Actions
{
	/// <summary>
	/// 延迟动作
	/// </summary>
	public class DelayAction : TimeAction
	{
		private CallFuncDelegate _Handler;

		public DelayAction (float time, CallFuncDelegate handler)
		{
			TotalTime = time;
			_Handler = handler;
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected override void RunAction(float dt) {
			if (_Time < TotalTime) {
				_Time += dt;
			} else {
				DoDelayEvent ();
				Finish = true;
			}
		}

		protected virtual void DoDelayEvent() {
			if (_Handler != null) {
				_Handler ();
			}
		}
	}
}

