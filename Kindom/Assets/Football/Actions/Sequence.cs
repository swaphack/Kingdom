using System.Collections.Generic;

namespace Football.Actions
{
	/// <summary>
	/// 顺序动作
	/// </summary>
	public class Sequence : Action
	{
		/// <summary>
		/// 动作队列
		/// </summary>
		private List<Action> _Actions;
		
		public Sequence ()
		{
			_Actions = new List<Action> ();
		}

		public Sequence(Action[] actions):this() {
			if (actions == null) {
				return;
			}

			for (int i = 0; i < actions.Length; i++) {
				this.AddAction (actions [i]);
			}
		}

		public Sequence(Action action0, Action action1) : this() {
			if (action0 == null || action1 == null) {
				return;
			}

			this.AddAction (action0);
			this.AddAction (action1);
		}

		/// <summary>
		/// 添加动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void AddAction(Action action) {
			if (action == null) {
				return;
			}

			_Actions.Add (action);
		}

		/// <summary>
		/// 移除所有动作
		/// </summary>
		public void RemoveAllActions() {
			_Actions.Clear ();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public override void Init() {
			base.Init ();

			if (_Actions.Count == 0) {
				Finish = true;
				return;
			}

			for (int i = 0; i < _Actions.Count; i++) {
				_Actions [i].Init();
			}
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected override void RunAction(float dt) {
			if (_Actions.Count == 0) {
				return;
			}

			int i = 0;
			for (i = 0; i < _Actions.Count; i++) {
				if (!_Actions [i].Finish) {
					_Actions[i].Entity = Entity;
					_Actions [i].Update (dt);
					break;
				}
			}

			if (i == _Actions.Count) {
				Finish = true;
			}
		}
	}
}

