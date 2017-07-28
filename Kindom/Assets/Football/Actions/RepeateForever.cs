using System;

namespace Football.Actions
{
	/// <summary>
	/// 永远执行下去
	/// </summary>
	public class RepeateForever : Action
	{
		/// <summary>
		/// 指定动作
		/// </summary>
		private Action _Action;

		public RepeateForever (Action action)
		{
			_Action = action;
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public override void Init() {
			base.Init ();

			if (_Action == null) {
				Finish = true;
				return;
			}
			_Action.Entity = Entity;
			_Action.Init ();
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected override void RunAction(float dt) {
			if (_Action != null && !_Action.Finish) {
				_Action.Update (dt);
				return;
			}
			if (_Action != null) {
				_Action.Init();
			}
		}
	}
}

