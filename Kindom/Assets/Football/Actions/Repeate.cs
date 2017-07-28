using System;

namespace Football.Actions
{
	/// <summary>
	/// 重复动作
	/// </summary>
	public class Repeate : Action
	{
		/// <summary>
		/// 重复次数
		/// </summary>
		private int _Count;
		/// <summary>
		/// 指定动作
		/// </summary>
		private Action _Action;


		public Repeate (int count, Action action)
		{
			_Count = count;
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

			_Count--;
			if (_Action != null) {
				_Action.Finish = false;
			}
			if (_Count == 0) {
				Finish = true;
			}
		}
	}
}

