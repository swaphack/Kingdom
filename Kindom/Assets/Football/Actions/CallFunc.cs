using System;

namespace Football.Actions
{
	public delegate void CallFuncDelegate();
	/// <summary>
	/// 不带参数回调
	/// </summary>
	public class CallFunc : Action
	{
		private CallFuncDelegate _Handler;

		public CallFunc(CallFuncDelegate handler) {
			_Handler = handler;
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		protected override void RunAction(float dt) {
			if (_Handler != null) {
				_Handler ();
			}
			Finish = true;
		}
	}

	public delegate void CallFuncWithParamterDelegate(object param);

	/// <summary>
	/// 带参数回调
	/// </summary>
	public class CallFunc2 : Action
	{
		private CallFuncWithParamterDelegate _Handler;
		private object _Parameter;

		public CallFunc2(CallFuncWithParamterDelegate handler, object param) {
			_Handler = handler;
			_Parameter = param;
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		protected override void RunAction(float dt) {
			if (_Handler == null) {
				return;
			}
			_Handler (_Parameter);
			Finish = true;
		}
	}
}

